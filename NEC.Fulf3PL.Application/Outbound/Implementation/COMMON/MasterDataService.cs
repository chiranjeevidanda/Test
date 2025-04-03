using Microsoft.Extensions.Logging;
using NEC.Fulf3PL.Application.Outbound.Interface;
using NEC.Fulf3PL.Core.Common;
using Newtonsoft.Json;
using Microsoft.Extensions.Configuration;
using NEC.Fulf3PL.Core.Interface;
using NEC.Fulf3PL.Core.Entities;
using static NEC.Fulf3PL.Application.Outbound.Implementation.COMMON.Mapper.DeliveryProcessMapper;
using NEC.Fulf3PL.Application.Outbound.Implementation.COMMON.Models;
using NEC.Fulf3PL.Application.Outbound.Implementation.COMMON.DTO;
using NEC.Fulf3PL.Application.Outbound.Implementation.COMMON.Helpers;

namespace NEC.Fulf3PL.Application.Outbound.Implementation.COMMON
{
    public class MasterDataService : IMasterDataService<OutboundConverterModel>
    {
        private readonly ILogger<MasterDataService> logger;
        private readonly IConfiguration configuration;
        private readonly IRequestHandler requestHandler;
        private readonly IOutboundTransactionRepository outboundTransactionRepository;
        private readonly IMasterDataRequestRepository masterDataRequestRepository;

        public MasterDataService(ILogger<MasterDataService> logger, IConfiguration configuration, IRequestHandler requestHandler, IOutboundTransactionRepository outboundTransactionRepository, IMasterDataRequestRepository masterDataRequestRepository)
        {
            this.logger = logger;
            this.configuration = configuration;
            this.requestHandler = requestHandler;
            this.outboundTransactionRepository = outboundTransactionRepository;
            this.masterDataRequestRepository = masterDataRequestRepository;
        }

        public async Task<ExecuteResult<bool>> PostData(OutboundConverterModel convertedData, string customer)
        {
            var result = new ExecuteResult<bool>();
            var providerResponse = new ExecuteResult<string>();

            string tokenMessage = string.Empty;

            var externalRequest = new ExternalRequest();
            externalRequest.ContentType = "application/json";

            Enums.OutboundAPIEndpoint endpointType = convertedData.Endpoint;

            var masterDataDetails = JsonConvert.DeserializeObject<MasterDataDTO>(convertedData.ConvertedData);
            if (masterDataDetails == null)
            {
                throw new ArgumentNullException(nameof(masterDataDetails));
            }

            switch (endpointType)
            {
                case Enums.OutboundAPIEndpoint.Color:
                    externalRequest = await CreateAPIRequest(endpointURL: configuration["CreateItemColorAPI"],
    body: convertedData.ConvertedData, endpointType: endpointType, convertedData: convertedData, true, customer);
                    break;
                case Enums.OutboundAPIEndpoint.Design:
                    externalRequest = await CreateAPIRequest(endpointURL: configuration["CreateItemDesignAPI"],
    body: convertedData.ConvertedData, endpointType: endpointType, convertedData: convertedData, true, customer);
                    break;
                case Enums.OutboundAPIEndpoint.Model:
                    externalRequest = await CreateAPIRequest(endpointURL: configuration["CreateItemModelAPI"],
    body: convertedData.ConvertedData, endpointType: endpointType, convertedData: convertedData, true, customer);
                    break;
                case Enums.OutboundAPIEndpoint.Size:
                    externalRequest = await CreateAPIRequest(endpointURL: configuration["CreateItemSizeAPI"],
    body: convertedData.ConvertedData, endpointType: endpointType, convertedData: convertedData, true, customer);
                    break;
                case Enums.OutboundAPIEndpoint.Season:
                    externalRequest = await CreateAPIRequest(endpointURL: configuration["CreateItemSeasonAPI"],
    body: convertedData.ConvertedData, endpointType: endpointType, convertedData: convertedData, true, customer);
                    break;
                default:

                    break;
            }

            // set end point name in request
            externalRequest.AdditionalInfo = endpointType.ToString();
            if (string.IsNullOrEmpty(externalRequest.BearerToken))
            {
                SetTokenStatus(endpointType, out tokenMessage);

                //log in db
                providerResponse.Success = false;
                providerResponse.Result = Constants.ErrorNoBearerTokenInvalidKey;
                await LogPostProcess(convertedData.ProcessId, convertedData.Endpoint.ToString(), externalRequest, providerResponse, Constants.ErrorNoBearerTokenInvalidKey);

                result.Messages = new List<ExecuteMessage>()
                    {
                        new ExecuteMessage() { Code = Enums.StatusCode.NoRecordFound, Description = Constants.ErrorNoBearerTokenInvalidKey },
                    };
                return result;
            }
            //log post process

            try
            {
                providerResponse = await requestHandler.PostAsync(externalRequest);
            }
            catch (Exception ex)
            {
                //log in db
                await LogPostProcess(convertedData.ProcessId, convertedData.Endpoint.ToString(), externalRequest, providerResponse, $"Error - {ex.Message}");

                throw;
            }
            if (providerResponse.Success && providerResponse.Result != null)
            {
                result.Success = providerResponse.Success;
                result.Result = providerResponse.Success;

                //log in db
                MasterDataResponseDTO masterData = JsonConvert.DeserializeObject<MasterDataResponseDTO>(providerResponse.Result);

                if (masterData != null)
                {
                    await CreateMasterDataDetails(masterData, endpointType.ToString(), customer);
                }
                await LogPostProcess(convertedData.ProcessId, convertedData.Endpoint.ToString(), externalRequest, providerResponse, "Success");
            }
            else if (!providerResponse.Success)
            {
                var httpStatusCode = providerResponse.Messages.FirstOrDefault().HttpCode;
                //log post process
                //log in db
                result.Success = providerResponse.Success;
                result.Result = providerResponse.Success;

                await LogPostProcess(convertedData.ProcessId, convertedData.Endpoint.ToString(), externalRequest, providerResponse, "Failed while posting   data to KTN API");

                if (httpStatusCode == System.Net.HttpStatusCode.BadRequest)
                {
                    result.Success = providerResponse.Success;
                }
                else if (httpStatusCode == System.Net.HttpStatusCode.Gone)
                {
                    throw new InternalServierException(Constants.ErrorWebhookGone);
                }
                else if (httpStatusCode == System.Net.HttpStatusCode.InternalServerError)
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
                //log in db
                result.Success = false;
                result.Result = false;
                //log in db
                await LogPostProcess(convertedData.ProcessId, convertedData.Endpoint.ToString(), externalRequest, providerResponse, Constants.ErrorNoDataFound);

                result.Messages = new List<ExecuteMessage>()
                    {
                        new ExecuteMessage() { Code = Enums.StatusCode.NoRecordFound, Description = Constants.ErrorNoDataFound },
                    };
            }


            return result;
        }

        public async Task<ExecuteResult<bool>> PutData(OutboundConverterModel convertedData, string systemId, string customer)
        {
            var result = new ExecuteResult<bool>();
            var providerResponse = new ExecuteResult<string>();

            var currentIndex = convertedData.CurrentIndex;
            string tokenMessage = string.Empty;

            if (!currentIndex.HasValue)
            {
                throw new ArgumentNullException(nameof(currentIndex));
            }
            var externalRequest = new ExternalRequest();
            externalRequest.ContentType = "application/json";

            Enums.OutboundAPIEndpoint endpointType = convertedData.Endpoint;

            switch (endpointType)
            {
                case Enums.OutboundAPIEndpoint.Color:

                    externalRequest = await CreateAPIRequest(endpointURL: string.Format(configuration["UpdateItemColorAPI"], systemId),
    body: convertedData.ConvertedData, endpointType: endpointType, convertedData: convertedData, true, customer);
                    break;
                case Enums.OutboundAPIEndpoint.Design:
                    externalRequest = await CreateAPIRequest(endpointURL: string.Format(configuration["UpdateItemDesignAPI"], systemId),
    body: convertedData.ConvertedData, endpointType: endpointType, convertedData: convertedData, true, customer);
                    break;
                case Enums.OutboundAPIEndpoint.Model:
                    externalRequest = await CreateAPIRequest(endpointURL: string.Format(configuration["UpdateItemModelAPI"], systemId),
    body: convertedData.ConvertedData, endpointType: endpointType, convertedData: convertedData, true, customer);
                    break;
                case Enums.OutboundAPIEndpoint.Size:
                    externalRequest = await CreateAPIRequest(endpointURL: string.Format(configuration["UpdateItemSizeAPI"], systemId),
    body: convertedData.ConvertedData, endpointType: endpointType, convertedData: convertedData, true, customer);
                    break;
                case Enums.OutboundAPIEndpoint.Season:
                    externalRequest = await CreateAPIRequest(endpointURL: string.Format(configuration["UpdateItemSeasonAPI"], systemId),
    body: convertedData.ConvertedData, endpointType: endpointType, convertedData: convertedData, true, customer);
                    break;
                default:

                    break;
            }

            // set end point name in request
            externalRequest.AdditionalInfo = endpointType.ToString();
            if (string.IsNullOrEmpty(externalRequest.BearerToken))
            {
                SetTokenStatus(endpointType, out tokenMessage);

                //log in db
                providerResponse.Success = false;
                providerResponse.Result = Constants.ErrorNoBearerTokenInvalidKey;
                await LogPostProcess(convertedData.ProcessId, convertedData.Endpoint.ToString(), externalRequest, providerResponse, Constants.ErrorNoBearerTokenInvalidKey);

                result.Messages = new List<ExecuteMessage>()
                    {
                        new ExecuteMessage() { Code = Enums.StatusCode.NoRecordFound, Description = Constants.ErrorNoBearerTokenInvalidKey },
                    };
                return result;
            }
            //log post process

            try
            {
                providerResponse = await requestHandler.PutAsync(externalRequest);
            }
            catch (Exception ex)
            {
                //log in db
                providerResponse.Success = false;
                //providerResponse.Result = Constants.ErrorOccured;
                await LogPostProcess(convertedData.ProcessId, convertedData.Endpoint.ToString(), externalRequest, providerResponse, $"Error - {ex.Message}");

                throw;
            }
            if (providerResponse.Success && providerResponse.Result != null)
            {
                result.Success = providerResponse.Success;
                result.Result = providerResponse.Success;

                //log in db
                MasterDataResponseDTO masterData = JsonConvert.DeserializeObject<MasterDataResponseDTO>(providerResponse.Result);

                if (masterData != null)
                {
                    await CreateMasterDataDetails(masterData, endpointType.ToString(), customer);
                }
                await LogPostProcess(convertedData.ProcessId, convertedData.Endpoint.ToString(), externalRequest, providerResponse, "Success");
            }
            else if (!providerResponse.Success)
            {
                var httpStatusCode = providerResponse.Messages.FirstOrDefault().HttpCode;
                //log post process
                //log in db
                result.Success = providerResponse.Success;
                result.Result = providerResponse.Success;

                await LogPostProcess(convertedData.ProcessId, convertedData.Endpoint.ToString(), externalRequest, providerResponse, "Failed while updating data to KTN API");

                if (httpStatusCode == System.Net.HttpStatusCode.BadRequest)
                {
                    result.Success = providerResponse.Success;
                }
                else if (httpStatusCode == System.Net.HttpStatusCode.Gone)
                {
                    throw new InternalServierException(Constants.ErrorWebhookGone);
                }
                else if (httpStatusCode == System.Net.HttpStatusCode.InternalServerError)
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
                //log in db
                result.Success = false;
                result.Result = false;
                //log in db
                await LogPostProcess(convertedData.ProcessId, convertedData.Endpoint.ToString(), externalRequest, providerResponse, Constants.ErrorNoDataFound);

                result.Messages = new List<ExecuteMessage>()
                    {
                        new ExecuteMessage() { Code = Enums.StatusCode.NoRecordFound, Description = Constants.ErrorNoDataFound },
                    };
            }


            return result;
        }

        public async Task<ExecuteResult<OutboundConverterModel>> MapData(string request)
        {
            var result = new ExecuteResult<OutboundConverterModel>();

            logger.LogInformation("Mapping ItemMaster Data.....");
            var message = JsonConvert.DeserializeObject<DeliveryDataModel>(request);
            if (message != null)
            {
                var requestMessage = JsonConvert.DeserializeObject<EventBlobRequestMessage>(message.Data);
                result.Result = new OutboundConverterModel();
                result.Result.TotalRecords = 1;

                var getBlobUrlRequest = new ExternalRequest();
                getBlobUrlRequest.URL = requestMessage.Data.URL;
                var response = await requestHandler.GetAsync(getBlobUrlRequest);

                if (response.Success && response.Result != null)
                {
                    result.Success = response.Success;
                    result.Result.ConvertedData = response.Result;

                    //log process
                }
                else if (!response.Success)
                {
                    var httpStatusCode = response.Messages.FirstOrDefault().HttpCode;

                    //log process
                    if (httpStatusCode == System.Net.HttpStatusCode.BadRequest)
                    {
                        result.Success = response.Success;
                    }
                    else if (httpStatusCode == System.Net.HttpStatusCode.Gone)
                    {
                        throw new InternalServierException(Constants.ErrorWebhookGone);
                    }
                    else if (httpStatusCode == System.Net.HttpStatusCode.InternalServerError)
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

                    //log process
                }
            }
            return result;
        }

        private async Task<ExternalRequest> CreateAPIRequest(string endpointURL, string body, Enums.OutboundAPIEndpoint endpointType, OutboundConverterModel convertedData, bool additionalHeadersFlag, string customer)
        {
            var result = new ExternalRequest();
            result.URL = endpointURL;
            result.Body = body;
            result.Header = await GetHeader(endpointType, additionalHeadersFlag, customer);
            result.BearerToken = convertedData?.Tokens?.FirstOrDefault()?.Token;
            return result;
        }

        private async Task<Dictionary<string, string>> GetHeader(Enums.OutboundAPIEndpoint key, bool additionalHeadersFlag, string customer)
        {
            var result = new Dictionary<string, string>();
            var headerParamsAll = DeliveryProcessHelper.LoadKtnHeaderParams(additionalHeadersFlag, customer);
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

        private void SetTokenStatus(Enums.OutboundAPIEndpoint endpoint, out string tokenMessage)
        {
            tokenMessage = string.Format(Constants.ErrorNoDataFound, endpoint);
        }

        private async Task LogPostProcess(string processId, string requestType, ExternalRequest externalRequest, ExecuteResult<string> response, string comments)
        {
            OutboundTransactionLogModel outboundTransaction = MapKtnOutboundLogDetails(processId, requestType, externalRequest, response, comments);
            await outboundTransactionRepository.CreateAsync(outboundTransaction);
        }

        private async Task CreateMasterDataDetails(MasterDataResponseDTO masterData, string type, string customer)
        {
            await masterDataRequestRepository.CreateAsync(new MasterDataModel()
            {
                Id = Guid.NewGuid().ToString(),
                Provider = Enums.WarehouseProvider.KTN.ToString(),
                Customer = customer,
                LoggedOn = Helper.GetCurrentDatetime(),
                Type = type,
                Code = masterData.Code,
                Description = masterData.Description,
                SystemId = masterData.SystemId

            });
        }
    }
}
