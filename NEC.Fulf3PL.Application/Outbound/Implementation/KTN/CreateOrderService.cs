﻿using Microsoft.Extensions.Logging;
using NEC.Fulf3PL.Application.Outbound.Interface;
using NEC.Fulf3PL.Core.Common;
using Newtonsoft.Json;
using Microsoft.Extensions.Configuration;
using NEC.Fulf3PL.Core.Interface;
using NEC.Fulf3PL.Application.Outbound.Implementation.KTN.DTO;
using NEC.Fulf3PL.Application.Outbound.Implementation.KTN.Models;
using NEC.Fulf3PL.Application.Outbound.Implementation.KTN.Common;
using NEC.Fulf3PL.Core.Entities;
using static NEC.Fulf3PL.Application.Outbound.Implementation.COMMON.Mapper.DeliveryProcessMapper;
using System.Net;
using System.Globalization;

namespace NEC.Fulf3PL.Application.Outbound.Implementation.KTN
{
    public class CreateOrderService : ICreateOrderService<OutboundConverterModel>
    {
        private readonly ILogger<CreateOrderService> logger;
        private readonly IConfiguration configuration;
        private readonly IRequestHandler requestHandler;
        private readonly IOutboundTransactionRepository outboundTransactionRepository;

        public CreateOrderService(ILogger<CreateOrderService> logger, IConfiguration configuration, IRequestHandler requestHandler, IOutboundTransactionRepository outboundTransactionRepository)
        {
            this.logger = logger;
            this.configuration = configuration;
            this.requestHandler = requestHandler;
            this.outboundTransactionRepository = outboundTransactionRepository;
        }

        public async Task<ExecuteResult<bool>> PostData(OutboundConverterModel convertedData)
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

            if (endpointType.Equals(Enums.OutboundAPIEndpoint.CreateOrder))
            {
                CreateOrderDTO createOrderPayload = JsonConvert.DeserializeObject<CreateOrderDTO>(convertedData.ConvertedData);

                if (createOrderPayload == null)
                {
                    throw new InternalServierException(Constants.ErrorOccured);
                }

                externalRequest = await CreateAPIRequest(endpointURL: configuration["KtnCreateOrderAPI"], body: JsonConvert.SerializeObject(createOrderPayload), endpointType: endpointType, convertedData: convertedData, false, createOrderPayload.Customer);
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
                providerResponse.Success = false;
                await LogPostProcess(convertedData.ProcessId, convertedData.Endpoint.ToString(), externalRequest, providerResponse, $"Error - {ex.Message}");

                throw;
            }
            if (providerResponse.Success && providerResponse.Result != null)
            {
                result.Success = providerResponse.Success;
                result.Result = providerResponse.Success;

                result.Messages.Add(new ExecuteMessage()
                {
                    Code = Enums.StatusCode.Success,
                    Description = providerResponse.Result,
                    HttpCode = HttpStatusCode.OK,
                });


                //log in db
                await LogPostProcess(convertedData.ProcessId, convertedData.Endpoint.ToString(), externalRequest, providerResponse, "Success");
            }
            else if (!providerResponse.Success)
            {
                var httpStatusCode = providerResponse.Messages.FirstOrDefault().HttpCode;
                //log post process
                //log in db
                result.Success = providerResponse.Success;
                result.Result = providerResponse.Success;

                await LogPostProcess(convertedData.ProcessId, convertedData.Endpoint.ToString(), externalRequest, providerResponse, "Failed while posting  data to KTN API");

                if (httpStatusCode == HttpStatusCode.BadRequest)
                {
                    result.Success = providerResponse.Success;
                    result.Messages.Add(new ExecuteMessage()
                    {
                        Code = Enums.StatusCode.Error,
                        Description = providerResponse.Result,
                        HttpCode = httpStatusCode
                    });
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

        public async Task<ExecuteResult<bool>> PutData(OutboundConverterModel convertedData, string systemId)
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

            if (endpointType.Equals(Enums.OutboundAPIEndpoint.CreateOrder))
            {
                CreateOrderDTO createOrderPayload = JsonConvert.DeserializeObject<CreateOrderDTO>(convertedData.ConvertedData);
                string customerVal = createOrderPayload.Customer;
                createOrderPayload.Customer = null;

                var processData = JsonConvert.SerializeObject(createOrderPayload);

                externalRequest = await CreateAPIRequest(endpointURL: string.Format(configuration["KtnUpdateOrderAPI"], systemId), body: processData, endpointType: endpointType, convertedData: convertedData, true, customerVal);
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
                await LogPostProcess(convertedData.ProcessId, convertedData.Endpoint.ToString(), externalRequest, providerResponse, $"Error - {ex.Message}");

                throw;
            }
            if (providerResponse.Success && providerResponse.Result != null)
            {
                result.Success = providerResponse.Success;
                result.Result = providerResponse.Success;

                result.Messages.Add(new ExecuteMessage()
                {
                    Code = Enums.StatusCode.Success,
                    Description = providerResponse.Result,
                    HttpCode = HttpStatusCode.OK,
                });

                //log in db
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

                if (httpStatusCode == HttpStatusCode.BadRequest)
                {
                    result.Success = providerResponse.Success;
                    result.Messages.Add(new ExecuteMessage()
                    {
                        Code = Enums.StatusCode.Error,
                        Description = providerResponse.Result,
                        HttpCode = httpStatusCode
                    });
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

            logger.LogInformation("Mapping Create Order Data.....");
            var message = JsonConvert.DeserializeObject<EventBlobRequestMessage>(request);
            if (message != null)
            {
                var getBlobUrlRequest = new ExternalRequest();
                getBlobUrlRequest.URL = message.Data.URL;
                var response = await requestHandler.GetAsync(getBlobUrlRequest);
                if (!response.Success)
                {
                    return result;
                }

                var blobResponse = JsonConvert.DeserializeObject<OutboundDeliveryMessage>(response.Result);

                if (blobResponse != null)
                {
                    var requestData = blobResponse.RequestData;

                    var externalRequest = new ExternalRequest()
                    {
                        Body = requestData
                    };
                    Enums.OutboundAPIEndpoint endpointType;
                    if (Enum.TryParse<Enums.OutboundAPIEndpoint>(blobResponse.InboundEndpoint, true, out endpointType))
                    {
                        result.Result = new OutboundConverterModel();
                        result.Result.Endpoint = endpointType;

                        externalRequest.URL = configuration["KtnCreateOrderConverter"];
                        result.Result.TotalRecords = 1;
                        result.Result.ProcessId = blobResponse.ProcessId;

                        //log process

                        await GetConveterMapDetails(blobResponse, result, externalRequest);
                    }
                    else
                    {
                        result.Messages = new List<ExecuteMessage>()
                    {
                        new ExecuteMessage() { Code = Enums.StatusCode.NoRecordFound, Description = Constants.ErrorNoDataFound },
                    };
                    }

                }
                return result;
            }
            return result;
        }

        private async Task GetConveterMapDetails(OutboundDeliveryMessage blobResponse, ExecuteResult<OutboundConverterModel> result, ExternalRequest externalRequest)
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
                await LogPostProcess(blobResponse.ProcessId, blobResponse.InboundEndpoint.ToString(), externalRequest, response, "Failed while fetching converter API response");

                if (httpStatusCode == HttpStatusCode.BadRequest)
                {
                    result.Success = response.Success;
                    result.Messages.Add(new ExecuteMessage()
                    {
                        Code = Enums.StatusCode.Error,
                        Description = response.Result,
                        HttpCode = httpStatusCode
                    });
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

                await LogPostProcess(blobResponse.ProcessId, blobResponse.InboundEndpoint.ToString(), externalRequest, response, Constants.ErrorNoDataFound);
            }
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

    }
}
