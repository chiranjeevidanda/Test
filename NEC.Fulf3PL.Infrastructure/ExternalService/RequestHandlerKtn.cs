using Newtonsoft.Json;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using NEC.Fulf3PL.Core.Common;
using System.Net;
using System.Net.Http.Headers;
using System.Text;
using NEC.Fulf3PL.Core.Interface;
using System;

namespace NEC.Fulf3PL.Infrastructure.ExternalService
{
    public class RequestHandlerKtn : IRequestHandler
    {
        private readonly IHttpClientFactory httpClientFactory;
        private readonly ILogger<RequestHandlerKtn> logger;
        private readonly IConfiguration configuration;

        public RequestHandlerKtn(IHttpClientFactory httpClientFactory, ILogger<RequestHandlerKtn> logger, IConfiguration configuration)
        {
            this.httpClientFactory = httpClientFactory;
            this.logger = logger;
            this.configuration = configuration;
        }

        public async Task<ExecuteResult<string>> PostAsync(ExternalRequest requestData)
        {
            var result = new ExecuteResult<string>();
            if ((requestData.ContentType != "application/x-www-form-urlencoded") && (requestData == null || string.IsNullOrEmpty(requestData.Body)))
            {
                throw new ArgumentNullException(nameof(requestData));
            }

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
                using (var cts = new CancellationTokenSource(TimeSpan.FromSeconds(Convert.ToInt64(configuration["APIRequestTimeSeconds"]))))
                {
                    if (requestData.ContentType == "application/x-www-form-urlencoded")
                    {
                        var formData = new List<KeyValuePair<string, string>>();
                        foreach (var pair in requestData.FormData)
                        {
                            formData.Add(new KeyValuePair<string, string>(pair.Key, pair.Value));
                        }
                        var content = new FormUrlEncodedContent(formData);
                        response = await client.PostAsync(requestData.URL, content, cts.Token).ConfigureAwait(false);
                    }
                    else // Assume JSON content type by default
                    {
                        var payload = new StringContent(requestData.Body, Encoding.UTF8, "application/json");
                        response = await client.PostAsync(requestData.URL, payload, cts.Token).ConfigureAwait(false);
                    }
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, Constants.ErrorTimeOut);
                throw;
            }
            // Check success or not
            // HttpResponseMessage customResponse = CheckStatus (HttpResponseMessage response, requestData)
            // 
            response = await ValidateResponseCodes(requestData, result, response);

            return result;
        }

        public async Task<ExecuteResult<string>> PutAsync(ExternalRequest requestData)
        {
            var result = new ExecuteResult<string>();
            if ((requestData.ContentType != "application/x-www-form-urlencoded") && (requestData == null || string.IsNullOrEmpty(requestData.Body)))
            {
                throw new ArgumentNullException(nameof(requestData));
            }

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
                using (var cts = new CancellationTokenSource(TimeSpan.FromSeconds(Convert.ToInt64(configuration["APIRequestTimeSeconds"]))))
                {
                    if (requestData.ContentType == "application/x-www-form-urlencoded")
                    {
                        var formData = new List<KeyValuePair<string, string>>();
                        foreach (var pair in requestData.FormData)
                        {
                            formData.Add(new KeyValuePair<string, string>(pair.Key, pair.Value));
                        }
                        var content = new FormUrlEncodedContent(formData);
                        response = await client.PutAsync(requestData.URL, content, cts.Token).ConfigureAwait(false);
                    }
                    else // Assume JSON content type by default
                    {
                        var payload = new StringContent(requestData.Body, Encoding.UTF8, "application/json");
                        response = await client.PutAsync(requestData.URL, payload, cts.Token).ConfigureAwait(false);
                    }
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, Constants.ErrorTimeOut);
                throw;
            }
            // Check success or not
            // HttpResponseMessage customResponse = CheckStatus (HttpResponseMessage response, requestData)
            // 
            response = await ValidateResponseCodes(requestData, result, response);

            return result;
        }
        public async Task<ExecuteResult<string>> PatchAsync(ExternalRequest requestData)
        {
            var result = new ExecuteResult<string>();
            if ((requestData.ContentType != "application/x-www-form-urlencoded") && (requestData == null || string.IsNullOrEmpty(requestData.Body)))
            {
                throw new ArgumentNullException(nameof(requestData));
            }

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
                using (var cts = new CancellationTokenSource(TimeSpan.FromSeconds(Convert.ToInt64(configuration["APIRequestTimeSeconds"]))))
                {
                    if (requestData.ContentType == "application/x-www-form-urlencoded")
                    {
                        var formData = new List<KeyValuePair<string, string>>();
                        foreach (var pair in requestData.FormData)
                        {
                            formData.Add(new KeyValuePair<string, string>(pair.Key, pair.Value));
                        }
                        var content = new FormUrlEncodedContent(formData);
                        response = await client.PatchAsync(requestData.URL, content, cts.Token).ConfigureAwait(false);
                    }
                    else // Assume JSON content type by default
                    {
                        var payload = new StringContent(requestData.Body, Encoding.UTF8, "application/json");
                        response = await client.PatchAsync(requestData.URL, payload, cts.Token).ConfigureAwait(false);
                    }
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, Constants.ErrorTimeOut);
                throw;
            }
            // Check success or not
            // HttpResponseMessage customResponse = CheckStatus (HttpResponseMessage response, requestData)
            // 
            response = await ValidateResponseCodes(requestData, result, response);

            return result;
        }

        private async Task<HttpResponseMessage?> ValidateResponseCodes(ExternalRequest requestData, ExecuteResult<string> result, HttpResponseMessage? response)
        {
            Enums.OutboundAPIEndpoint endpointType;
            if (Enum.TryParse<Enums.OutboundAPIEndpoint>(requestData.AdditionalInfo, true, out endpointType))
            {
                response = await CustomStatus(response);
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
                result.Messages = new List<ExecuteMessage>()
                    {
                        new ExecuteMessage() {  HttpCode = response.StatusCode, Code = Enums.StatusCode.Error, Description = Constants.ErrorWebhookSend },
                    };
            }

            return response;
        }

        private async Task<HttpResponseMessage> CustomStatus(HttpResponseMessage response)
        {
            string responseData = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
                if (string.IsNullOrEmpty(responseData))
                {
                    response.StatusCode = HttpStatusCode.OK;
                }
                else if (responseData != null)
                {
                    if (responseData.Contains("ErrorSchemaElement"))
                    {
                        response.StatusCode = HttpStatusCode.BadRequest;
                    }
                    else if (responseData.Contains("StatusResponse"))
                    {
                        CheckStatusResponse responseStatusCode = JsonConvert.DeserializeObject<CheckStatusResponse>(responseData);
                        if (responseStatusCode?.StatusResponses?.StatusCode == Convert.ToInt32(HttpStatusCode.BadRequest))
                        {
                            response.StatusCode = HttpStatusCode.BadRequest;
                        }
                        else
                        {
                            response.StatusCode = HttpStatusCode.OK;
                        }
                    }
                }
            }
            else
            {
                response.StatusCode = HttpStatusCode.BadRequest;
            }
            return response;
        }

        public async Task<ExecuteResult<string>> GetAsync(ExternalRequest requestData)
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
            //using (var cts = new CancellationTokenSource(TimeSpan.FromSeconds(Convert.ToInt64(60))))
            //    {
            //        try
            //        {

            //            var vv = await client.GetAsync("https://api.emea.uat.katoennatie.com//public/cgi/inbound/v2/subscriptions?company=c19f151d-33e5-ee11-84ab-005056be8bbc", cts.Token).ConfigureAwait(false);
            //        }
            //        catch (Exception ex)
            //        {
            //            logger.LogError(ex, Constants.ErrorTimeOut);
            //            throw;
            //        }
            //    }
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
                HttpResponseMessage test = await client.GetAsync("https://api.emea.uat.katoennatie.com//public/cgi/inbound/v2/subscriptions?company=c19f151d-33e5-ee11-84ab-005056be8bbc");
                using (var cts = new CancellationTokenSource(TimeSpan.FromSeconds(Convert.ToInt64(configuration["APIRequestTimeSeconds"]))))
                {
                    try
                    {
                       
                        response = await client.GetAsync(requestData.URL, cts.Token).ConfigureAwait(false);
                    }
                    catch (Exception ex)
                    {
                        logger.LogError(ex, Constants.ErrorTimeOut);
                        throw;
                    }
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

        public class CheckStatusResponse
        {
            [JsonProperty("StatusResponse")]
            public StatusResponse StatusResponses { get; set; }


            public class StatusResponse
            {
                [JsonProperty("StatusCode")]
                public int StatusCode { get; set; }

                [JsonProperty("Status")]
                public string Status { get; set; }

            }

        }
    }
}
