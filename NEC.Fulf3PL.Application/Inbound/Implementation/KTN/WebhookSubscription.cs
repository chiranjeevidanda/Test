using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using NEC.Fulf3PL.Application.Common.KTN.DTO;
using NEC.Fulf3PL.Application.Common.KTN;
using NEC.Fulf3PL.Application.Common.KTN.Interface;
using NEC.Fulf3PL.Application.Inbound.Implementation.KTN.Model;
using NEC.Fulf3PL.Core.Common;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using NEC.Fulf3PL.Core.Interface;
using NEC.Fulf3PL.Application.Inbound.Implementation.KTN.Common;
using System.Net;

namespace NEC.Fulf3PL.Application.Inbound.Implementation.KTN
{
    public abstract class WebhookSubscription
    {
        private readonly ILogger<WebhookSubscription> logger;
        private readonly IConfiguration configuration;
        private readonly ITokenGenerationService<GenerateTokenDTO> iTokenGenerationService;
        private readonly IRequestHandler requestHandler;

        public WebhookSubscription(ILogger<WebhookSubscription> logger, IConfiguration configuration, ITokenGenerationService<GenerateTokenDTO> iTokenGenerationService, IRequestHandler requestHandler)
        {
            this.logger = logger;
            this.configuration = configuration;
            this.iTokenGenerationService = iTokenGenerationService;
            this.requestHandler = requestHandler;
            //
        }
        public abstract Task<ExecuteResult<WebhookSubsciptionsPatchResponse>> ExtendSubscription(WebhookSubscriptionData subscription, Enums.WebhookSubscriptionType key, string customer = Constants.NEWERAB2B);
        public async Task<ExecuteResult<WebhookSubsciptionsGetResponse>> GetWebhookSubscription(Enums.WebhookSubscriptionType key, string companyId)
        {
            var result = new ExecuteResult<WebhookSubsciptionsGetResponse>();
            var response = new ExecuteResult<string>();
            try
            {
                var externalRequest = await CreateGetAPIRequest(key, companyId);
                response = await requestHandler.GetAsync(externalRequest);
            }
            catch (Exception ex)
            {
                //log in db
                response.Success = false;
               // await LogPostProcess(convertedData.ProcessId, convertedData.Endpoint.ToString(), externalRequest, response, $"Error - {ex.Message}");

                throw;
            }
            if (response.Success && response.Result != null)
            {
                result.Success = response.Success;
                result.Result = JsonConvert.DeserializeObject<WebhookSubsciptionsGetResponse>(response.Result);

                //log in db
                //await LogPostProcess(convertedData.ProcessId, convertedData.Endpoint.ToString(), externalRequest, response, "Success");
            }
            else if (!response.Success)
            {
                var httpStatusCode = response.Messages.FirstOrDefault().HttpCode;
                //log post process
                //log in db
                result.Success = response.Success;

               // await LogPostProcess(convertedData.ProcessId, convertedData.Endpoint.ToString(), externalRequest, response, "Failed while getting data from KTN API");

                if (httpStatusCode == HttpStatusCode.BadRequest)
                {
                    result.Success = response.Success;
                }
                else if (httpStatusCode == HttpStatusCode.Gone)
                {
                    throw new InternalServierException(Constants.ErrorWebhookGone);
                }
                else if (httpStatusCode == HttpStatusCode.InternalServerError)
                {
                    throw new InternalServierException(httpStatusCode.ToString());
                }
                else
                {
                    throw new Exception(httpStatusCode.ToString());
                }
            }
            else
            {
                result.Success = false;
                //log in db
                //await LogPostProcess(convertedData.ProcessId, convertedData.Endpoint.ToString(), externalRequest, response, Constants.ErrorNoDataFound);
                logger.LogError(Constants.ErrorNoDataFound);

                result.Messages = new List<ExecuteMessage>()
                    {
                        new ExecuteMessage() { Code = Enums.StatusCode.NoRecordFound, Description = Constants.ErrorNoDataFound },
                    };
            }


            return result;
        }
        private async Task<ExternalRequest> CreateGetAPIRequest(Enums.WebhookSubscriptionType endpointType, string companyId, string customer = Constants.NEWERAB2B)
        {
            var externalRequest = new ExternalRequest();

            try
            {
                var token = GetToken();
                string baseUrl = configuration["KTNBaseUrl"];
                string endpointURL = $"https://{baseUrl}/public/cgi/{endpointType.GetDescription()}/v2/subscriptions?company={companyId}";

                var headerData = new Dictionary<string, string>();
                var headerParamsAll = DeliveryProcessHelper.GetWebSubscriptionExtensionHeader(customer);
                var headerParams = new List<(string name, string value)>();
                if (headerParamsAll.TryGetValue(endpointType.ToString(), out headerParams))
                {
                    foreach (var item in headerParams)
                    {
                        headerData.Add(item.name, item.value);
                    }
                }

                externalRequest = new ExternalRequest()
                {
                    URL = endpointURL,
                    Header = headerData,
                    BearerToken = await GetToken()
                };
            }
            catch (Exception ex)
            {
                throw;
            }
            return externalRequest;
        }
        protected async Task<string> GetToken()
        {
            var result = new ExecuteResult<string>();
            var token = iTokenGenerationService.GenerateToken().Result;
            result.Result = token.Results.FirstOrDefault().Token;
            return result.Result;
        }
        protected async Task<Dictionary<string, string>> GetHeader(WebhookExtensionModel data, Enums.WebhookSubscriptionType key)
        {
            var result = new Dictionary<string, string>();
            var headerParamsAll = DeliveryProcessHelper.GetWebSubscriptionExtensionHeader(customer: data.Customer, subscriptionEtag: data.SubscriptionEtag);
            var headerParams = new List<(string name, string value)>();
            if (headerParamsAll.TryGetValue(key.ToString(), out headerParams))
            {
                foreach (var item in headerParams)
                {
                    result.Add(item.name, item.value);
                }
            }

            return result;
        }
        protected async Task<ExternalRequest> CreateAPIRequest(WebhookSubscriptionData subscription, Enums.WebhookSubscriptionType key, string customer = Constants.NEWERAB2B)
        {
            var token = await GetToken();
            var data = SetSubscriptionData(subscription, key, customer);
            var requestData = JsonConvert.SerializeObject(data.Payload);
            string baseUrl = configuration["KTNBaseUrl"];
            string endpointURL = $"https://{baseUrl}public/cgi/{key.GetDescription()}/v2/subscriptions('{data.SubscriptionId}')";

            var externalRequest = new ExternalRequest()
            {
                Body = requestData,
                URL = endpointURL,
                Header = await GetHeader(data, key),
                BearerToken = token
            };
            return externalRequest;
        }
        private WebhookExtensionModel SetSubscriptionData(WebhookSubscriptionData subscription, Enums.WebhookSubscriptionType key, string customer = Constants.NEWERAB2B)
        {
            var webhookData = new WebhookExtensionModel()
            {
                SubscriptionId = subscription.SubscriptionId,
                Payload = new WebhookExtensionPayload()
                {
                    ClientState = subscription.ClientState
                },
                Type = key,
                SubscriptionEtag = subscription.ETag,
                SubscriptionKey = configuration["KtnSubscriptionKey"],
                Customer = customer,
            };
            return webhookData;
        }
        protected async Task<ExecuteResult<WebhookSubsciptionsPatchResponse>> InvokeExtension(WebhookSubscriptionData subscription, Enums.WebhookSubscriptionType key, string customer = Constants.NEWERAB2B)
        {
            var result = new ExecuteResult<WebhookSubsciptionsPatchResponse>();
            var response = new ExecuteResult<string>();
            try
            {
                var externalRequest = await CreateAPIRequest(subscription, key, customer);
                response = await requestHandler.PatchAsync(externalRequest);
            }
            catch (Exception ex)
            {
                //log in db
                response.Success = false;
                // await LogPostProcess(convertedData.ProcessId, convertedData.Endpoint.ToString(), externalRequest, response, $"Error - {ex.Message}");

                throw;
            }
            if (response.Success && response.Result != null)
            {
                result.Success = response.Success;
                result.Result = JsonConvert.DeserializeObject<WebhookSubsciptionsPatchResponse>(response.Result);

                //log in db
                //await LogPostProcess(convertedData.ProcessId, convertedData.Endpoint.ToString(), externalRequest, response, "Success");
            }
            else if (!response.Success)
            {
                var httpStatusCode = response.Messages.FirstOrDefault().HttpCode;
                //log post process
                //log in db
                result.Success = response.Success;

                // await LogPostProcess(convertedData.ProcessId, convertedData.Endpoint.ToString(), externalRequest, response, "Failed while getting data from KTN API");

                if (httpStatusCode == HttpStatusCode.BadRequest)
                {
                    result.Success = response.Success;
                }
                else if (httpStatusCode == HttpStatusCode.Gone)
                {
                    throw new InternalServierException(Constants.ErrorWebhookGone);
                }
                else if (httpStatusCode == HttpStatusCode.InternalServerError)
                {
                    throw new InternalServierException(httpStatusCode.ToString());
                }
                else
                {
                    throw new Exception(httpStatusCode.ToString());
                }
            }
            else
            {
                result.Success = false;
                //log in db
                //await LogPostProcess(convertedData.ProcessId, convertedData.Endpoint.ToString(), externalRequest, response, Constants.ErrorNoDataFound);
                logger.LogError(Constants.ErrorNoDataFound);

                result.Messages = new List<ExecuteMessage>()
                    {
                        new ExecuteMessage() { Code = Enums.StatusCode.NoRecordFound, Description = Constants.ErrorNoDataFound },
                    };
            }


            return result;
        }

    }
}
