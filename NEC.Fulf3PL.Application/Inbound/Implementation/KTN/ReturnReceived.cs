using Microsoft.Extensions.Logging;
using NEC.Fulf3PL.Core.Common;
using Newtonsoft.Json;
using Microsoft.Extensions.Configuration;
using NEC.Fulf3PL.Core.Interface;
using NEC.Fulf3PL.Application.Inbound.Implementation.KTN.Common;
using NEC.Fulf3PL.Core.Entities;
using static NEC.Fulf3PL.Application.Inbound.Implementation.KTN.Common.DeliveryProcessMapper;
using System.Net;
using NEC.Fulf3PL.Application.Inbound.Interface;
using NEC.Fulf3PL.Application.Inbound.Implementation.KTN.Model;
using NEC.Fulfillment.Application.Models;

namespace NEC.Fulf3PL.Application.Inbound.Implementation.KTN
{
    public class ReturnReceived : IReturnReceived<InboundConverterModel>
    {
        private readonly ILogger<ReturnReceived> logger;
        private readonly IConfiguration configuration;
        private readonly IRequestHandler requestHandler;
        private readonly IInboundTransactionRepository inboundTransactionRepository;

        public ReturnReceived(ILogger<ReturnReceived> logger, IConfiguration configuration, IRequestHandler requestHandler, IInboundTransactionRepository inboundTransactionRepository)
        {
            this.logger = logger;
            this.configuration = configuration;
            this.requestHandler = requestHandler;
            this.inboundTransactionRepository = inboundTransactionRepository;
        }

        public async Task<ExecuteResult<InboundConverterModel>> GetData(InboundConverterModel convertedData)
        {
            var result = new ExecuteResult<InboundConverterModel>();
            var providerResponse = new ExecuteResult<string>();

            string tokenMessage = string.Empty;

            var externalRequest = new ExternalRequest();
            externalRequest.ContentType = "application/json";

            Enums.InboundAPIEndpoint endpointType = convertedData.Endpoint;

            result.Result = new InboundConverterModel();
            result.Result.Endpoint = endpointType;
            result.Result.TotalRecords = 1;
            result.Result.ProcessId = convertedData.ProcessId;
            string companyId = convertedData.CompanyId;

            externalRequest = await CreateAPIRequest(endpointURL: convertedData.URL, endpointType: endpointType, convertedData: convertedData, companyId);

            // set end point name in request
            externalRequest.AdditionalInfo = endpointType.ToString();

            try
            {
                providerResponse = await requestHandler.GetAsync(externalRequest);
            }
            catch (Exception ex)
            {
                //log in db
                providerResponse.Success = false;
                await LogPostProcess(convertedData.ProcessId, convertedData.Endpoint.ToString(), externalRequest, providerResponse, $"Error - {ex.Message}");

                throw;
            }
            if (providerResponse.Success && providerResponse.Result != null)
            {
                result.Success = providerResponse.Success;
                result.Result.ConvertedData = providerResponse.Result;

                //log in db
                await LogPostProcess(convertedData.ProcessId, convertedData.Endpoint.ToString(), externalRequest, providerResponse, "Success");
            }
            else if (!providerResponse.Success)
            {
                var httpStatusCode = providerResponse.Messages.FirstOrDefault().HttpCode;

                //log in db
                result.Success = providerResponse.Success;

                await LogPostProcess(convertedData.ProcessId, convertedData.Endpoint.ToString(), externalRequest, providerResponse, "Failed while getting data from KTN API");

                if (httpStatusCode == HttpStatusCode.BadRequest)
                {
                    result.Success = providerResponse.Success;
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
                await LogPostProcess(convertedData.ProcessId, convertedData.Endpoint.ToString(), externalRequest, providerResponse, Constants.ErrorNoDataFound);

                result.Messages = new List<ExecuteMessage>()
                    {
                        new ExecuteMessage() { Code = Enums.StatusCode.NoRecordFound, Description = Constants.ErrorNoDataFound },
                    };
            }


            return result;
        }

        private async Task<ExternalRequest> CreateAPIRequest(string endpointURL, Enums.InboundAPIEndpoint endpointType, InboundConverterModel convertedData, string companyId)
        {
            var result = new ExternalRequest();
            result.URL = endpointURL;
            //result.Body = body;
            result.Header = await GetHeader(endpointType, companyId);
            result.BearerToken = convertedData?.Tokens?.FirstOrDefault()?.Token;
            return result;
        }

        private async Task<ExternalRequest> CreatePostAPIRequest(string endpointURL, Enums.InboundAPIEndpoint endpointType, InboundConverterModel requestData, string companyId)
        {
            var result = new ExternalRequest();
            result.URL = endpointURL;
            result.Body = requestData.ConvertedData;
            //result.Header = await GetHeader(endpointType, companyId);
            result.BearerToken = requestData?.Tokens?.FirstOrDefault()?.Token;
            return result;
        }

        private async Task<Dictionary<string, string>> GetHeader(Enums.InboundAPIEndpoint key, string companyId)
        {
            var result = new Dictionary<string, string>();
            var headerParamsAll = DeliveryProcessHelper.LoadKtnHeaderParams(companyId);
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

        private void SetTokenStatus(Enums.InboundAPIEndpoint endpoint, out string tokenMessage)
        {
            tokenMessage = string.Format(Constants.ErrorNoDataFound, endpoint);
        }


        private async Task LogPostProcess(string processId, string requestType, ExternalRequest externalRequest, ExecuteResult<string> response, string comments)
        {
            InboundTransactionLogModel inboundTransaction = MapInboundLogDetails(processId, requestType, externalRequest, response, comments);
            await inboundTransactionRepository.CreateAsync(inboundTransaction);
        }

        public async Task<ExecuteResult<InboundConverterModel>> PostData(InboundConverterModel requestData, string requestURL)
        {
            var result = new ExecuteResult<InboundConverterModel>();
            var providerResponse = new ExecuteResult<string>();

            string tokenMessage = string.Empty;

            var externalRequest = new ExternalRequest();
            externalRequest.ContentType = "application/json";

            Enums.InboundAPIEndpoint endpointType = requestData.Endpoint;

            result.Result = new InboundConverterModel();
            result.Result.Endpoint = endpointType;
            result.Result.TotalRecords = 1;
            result.Result.ProcessId = requestData.ProcessId;
            string companyId = requestData.CompanyId;

            externalRequest = await CreatePostAPIRequest(endpointURL: requestURL, endpointType: endpointType, requestData: requestData, companyId);

            try
            {
                providerResponse = await requestHandler.PostAsync(externalRequest);
            }
            catch (Exception ex)
            {
                //log in db
                await LogPostProcess(requestData.ProcessId, requestData.Endpoint.ToString(), externalRequest, providerResponse, "Failed while posting the data to SAP BAPI URL");
                throw;
            }
            if (providerResponse.Success && providerResponse.Result != null)
            {
                result.Success = providerResponse.Success;
                result.Result.ConvertedData = providerResponse.Result;

            }
            else if (!providerResponse.Success)
            {
                var httpStatusCode = providerResponse.Messages.FirstOrDefault().HttpCode;

                //log in db
                result.Success = providerResponse.Success;

                if (httpStatusCode == HttpStatusCode.BadRequest)
                {
                    result.Success = providerResponse.Success;
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
                await LogPostProcess(requestData.ProcessId, requestData.Endpoint.ToString(), externalRequest, providerResponse, "Failed while posting the data to SAP BAPI URL");

                result.Messages = new List<ExecuteMessage>()
                    {
                        new ExecuteMessage() { Code = Enums.StatusCode.NoRecordFound, Description = Constants.ErrorNoDataFound },
                    };
            }


            return result;
        }

        public async Task<ExecuteResult<InboundConverterModel>> MapData(string request)
        {
            var result = new ExecuteResult<InboundConverterModel>();

            logger.LogInformation("Mapping Return Received Data.....");
            var message = JsonConvert.DeserializeObject<InboundMessageModel>(request);
            if (message != null)
            {
                var externalRequest = new ExternalRequest()
                {
                    Body = message.RequestData
                };
                Enums.InboundAPIEndpoint endpointType;
                if (Enum.TryParse<Enums.InboundAPIEndpoint>(message.InboundEndpoint, true, out endpointType))
                {
                    result.Result = new InboundConverterModel();
                    result.Result.Endpoint = endpointType;

                    if (endpointType.Equals(Enums.InboundAPIEndpoint.ReturnReceipt))
                    {
                        GoodsReceivedDetailsModel receiptData = JsonConvert.DeserializeObject<GoodsReceivedDetailsModel>(message.RequestData);
                        externalRequest.URL = configuration["KtnReturnReceivedConverter"];
                        result.Result.TotalRecords = 1;
                        result.Result.ProcessId = receiptData.SystemId;

                        await GetConveterMapDetails(message, result, externalRequest);
                    }
                }
                else
                {
                    result.Messages = new List<ExecuteMessage>()
                        {
                            new ExecuteMessage() { Code = Enums.StatusCode.NoRecordFound, Description = Constants.ErrorNoDataFound },
                        };
                }
                return result;
            }
            return result;
        }

        private async Task GetConveterMapDetails(InboundMessageModel message, ExecuteResult<InboundConverterModel> result, ExternalRequest externalRequest)
        {
            var response = await requestHandler.PostAsync(externalRequest);

            if (response.Success && response.Result != null)
            {
                result.Success = response.Success;
                result.Result.ConvertedData = response.Result;
            }
            else if (!response.Success)
            {
                var httpStatusCode = response.Messages.FirstOrDefault().HttpCode;

                //log process
                await LogPostProcess(message.RequestId, message.InboundEndpoint.ToString(), externalRequest, response, "Failed while fetching converter API response");

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
                result.Messages = new List<ExecuteMessage>()
                        {
                            new ExecuteMessage() { Code = Enums.StatusCode.NoRecordFound, Description = Constants.ErrorNoDataFound },
                        };

                await LogPostProcess(message.RequestId, message.InboundEndpoint.ToString(), externalRequest, response, Constants.ErrorNoDataFound);
            }
        }
    }
}
