using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using NEC.Fulf3PL.Application.Common.DurableContext;
using NEC.Fulf3PL.Core.Common;
using NEC.Fulf3PL.Core.Interface;
using System.Net;
using System.Net.Http.Headers;
using System.Text;

namespace NEC.Fulf3PL.Infrastructure.ExternalService
{
    public class RequestBaseHandler : IRequestHandler
    {
        private readonly IHttpClientFactory httpClientFactory;
        private readonly ILogger<RequestBaseHandler> logger;
        private readonly IConfiguration configuration;
        private readonly IDurableContextManager _durableContextManager;
        public RequestBaseHandler(IHttpClientFactory httpClientFactory, ILogger<RequestBaseHandler> logger, IConfiguration configuration, IDurableContextManager durableContextManager)
        {
            this.httpClientFactory = httpClientFactory;
            this.logger = logger;
            this.configuration = configuration;
            _durableContextManager = durableContextManager;
        }

        public async virtual Task<ExecuteResult<string>> GetAsync(ExternalRequest requestData)
        {
            var result = new ExecuteResult<string>();
            if (requestData == null)
            {
                throw new ArgumentNullException(nameof(requestData));
            }

            var client = httpClientFactory.CreateClient();
            if (!string.IsNullOrEmpty(requestData.BearerToken))
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", requestData.BearerToken);
            }
            foreach (var header in requestData.Header)
            {
                if (!client.DefaultRequestHeaders.TryAddWithoutValidation(header.Key, header.Value))
                {
                    var message = string.Format("Could not add header field \'{0}\' to the request ", header.Key);
                    logger.LogError(message);
                }
            }
            HttpResponseMessage? response;
            try
            {
                using (var cts = new CancellationTokenSource(TimeSpan.FromSeconds(Convert.ToInt64(configuration["APIRequestTimeSeconds"]))))
                {
                    response = await client.GetAsync(requestData.URL, cts.Token).ConfigureAwait(false);
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, Constants.ErrorTimeOut);
                throw;
            }
            if (response.IsSuccessStatusCode)
            {
                string responseJson = await response.Content.ReadAsStringAsync();
                result.Success = response.IsSuccessStatusCode;
                result.Result = responseJson;
                result.Messages = new List<ExecuteMessage>()
                    {
                        new ExecuteMessage() { HttpCode = response.StatusCode, Code = Enums.StatusCode.Success, Description = response.StatusCode.ToString() },
                    };
            }
            else if (response.StatusCode == HttpStatusCode.Gone)
            {
                // If we get a 410 Gone then we are also done.
                //await OnWebHookGone();
                logger.LogError(Constants.ErrorWebhookGone);
                result.Messages = new List<ExecuteMessage>()
                    {
                        new ExecuteMessage() { HttpCode = response.StatusCode, Code = Enums.StatusCode.Error, Description = Constants.ErrorWebhookGone },
                    };
            }
            else
            {
                //await OnWebHookFailure(response.StatusCode);
                logger.LogError(Constants.ErrorWebhookSend);
                result.Messages = new List<ExecuteMessage>()
                    {
                        new ExecuteMessage() {  HttpCode = response.StatusCode, Code = Enums.StatusCode.Error, Description = Constants.ErrorWebhookSend },
                    };
            }

            return result;
        }

        public async virtual Task<ExecuteResult<string>> PostAsync(ExternalRequest requestData)
        {
            if (_durableContextManager !=null && _durableContextManager.Current != null && !string.IsNullOrEmpty(_durableContextManager.Current?.InstanceId))
            {
                var processId = _durableContextManager.Current.InstanceId;
                requestData.Header.Add("x-instance-id", processId);
            }

            var result = new ExecuteResult<string>();
            if (requestData == null || string.IsNullOrEmpty(requestData.Body))
            {
                throw new ArgumentNullException(nameof(requestData));
            }
            //string jsonString = JsonSerializer.Serialize(json);
            var payload = new StringContent(requestData.Body, Encoding.UTF8, "application/json");
            var client = httpClientFactory.CreateClient();
            if (!string.IsNullOrEmpty(requestData.BearerToken))
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", requestData.BearerToken);
            }
            if (!string.IsNullOrEmpty(requestData.BasicToken))
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", requestData.Base64Encoded);
            }
            foreach (var header in requestData.Header)
            {
                if (!client.DefaultRequestHeaders.TryAddWithoutValidation(header.Key, header.Value))
                {
                    var message = string.Format("Could not add header field \'{0}\' to the request ", header.Key);
                    logger.LogError(message);
                }
            }
            HttpResponseMessage? response;
            try
            {
                using (var cts = new CancellationTokenSource(TimeSpan.FromSeconds(Convert.ToInt64("60"))))
                {
                    response = await client.PostAsync(requestData.URL, payload, cts.Token).ConfigureAwait(false);
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, Constants.ErrorTimeOut);
                throw;
            }
            if (response.IsSuccessStatusCode)
            {
                string responseJson = await response.Content.ReadAsStringAsync();
                result.Success = response.IsSuccessStatusCode;
                result.Result = responseJson;
                result.Messages = new List<ExecuteMessage>()
                    {
                        new ExecuteMessage() { HttpCode = response.StatusCode, Code = Enums.StatusCode.Success, Description = response.StatusCode.ToString() },
                    };
            }
            else if (response.StatusCode == HttpStatusCode.Gone)
            {
                // If we get a 410 Gone then we are also done.
                //await OnWebHookGone();
                string responseJson = await response.Content.ReadAsStringAsync();
                result.Result = responseJson;
                logger.LogError(Constants.ErrorWebhookGone);
                result.Messages = new List<ExecuteMessage>()
                    {
                        new ExecuteMessage() { HttpCode = response.StatusCode, Code = Enums.StatusCode.Error, Description = Constants.ErrorWebhookGone },
                    };
            }
            else
            {
                string responseJson = await response.Content.ReadAsStringAsync();
                logger.LogError(Constants.ErrorWebhookSend);
                result.Result = responseJson;
                //await OnWebHookFailure(response.StatusCode);
                result.Messages = new List<ExecuteMessage>()
                    {
                        new ExecuteMessage() {  HttpCode = response.StatusCode, Code = Enums.StatusCode.Error, Description = Constants.ErrorWebhookSend },
                    };
            }

            return result;
        }
        private Task OnWebHookGone()
        {
            logger.LogError(Constants.ErrorWebhookGone);
            throw new InternalServierException(Constants.ErrorWebhookGone);
        }
        private Task OnWebHookFailure(HttpStatusCode statusCode)
        {
            logger.LogError(Constants.ErrorWebhookSend);
            if (statusCode == HttpStatusCode.InternalServerError)
            {
                throw new InternalServierException(statusCode.ToString());
            }
            else
            {
                throw new Exception(statusCode.ToString());
            }
        }

        public Task<ExecuteResult<string>> PutAsync(ExternalRequest requestData)
        {
            throw new NotImplementedException();
        }

        public Task<ExecuteResult<string>> PatchAsync(ExternalRequest requestData)
        {
            throw new NotImplementedException();
        }
    }
}
