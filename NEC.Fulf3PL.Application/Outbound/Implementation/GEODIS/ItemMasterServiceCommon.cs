using Microsoft.Extensions.Logging;
using NEC.Fulf3PL.Application.Outbound.Interface.COMMON;
using NEC.Fulf3PL.Core.Common;
using Newtonsoft.Json;
using Microsoft.Extensions.Configuration;
using NEC.Fulf3PL.Core.Interface;
using NEC.Fulf3PL.Core.DTO;
using NEC.Fulf3PL.Application.Outbound.Implementation.GEODIS.DTO;
using NEC.Fulf3PL.Application.Outbound.Implementation.COMMON.Models;
using NEC.Fulf3PL.Application.Outbound.Implementation.COMMON.Helpers;
using NEC.Fulf3PL.Core.Entities;
using static NEC.Fulf3PL.Application.Outbound.Implementation.GEODIS.Common.DeliveryProcessMapper;
using System.Net;

namespace NEC.Fulf3PL.Application.Outbound.Implementation.GEODIS
{
    public class ItemMasterServiceCommon : IItemMasterServiceCommon<OutboundConverterModel>
    {
        private readonly ILogger<ItemMasterServiceCommon> logger;
        private readonly IConfiguration configuration;
        private readonly IRequestHandler requestHandler;
        private readonly IOutboundTransactionRepository outboundTransactionRepository;
        private readonly IItemMasterRequestRepository itemMasterRequestRepository;

        public ItemMasterServiceCommon(ILogger<ItemMasterServiceCommon> logger, IConfiguration configuration, IRequestHandler requestHandler, IOutboundTransactionRepository outboundTransactionRepository, IItemMasterRequestRepository itemMasterRequestRepository)
        {
            this.logger = logger;
            this.configuration = configuration;
            this.requestHandler = requestHandler;
            this.outboundTransactionRepository = outboundTransactionRepository;
            this.itemMasterRequestRepository = itemMasterRequestRepository;
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

            if (endpointType.Equals(Enums.OutboundAPIEndpoint.ProductMaster))
            {
                List<ProductMasterDTO> productData = JsonConvert.DeserializeObject<List<ProductMasterDTO>>(convertedData.ConvertedData);
                var productOrderPayload = productData.ElementAt(currentIndex.Value);

                externalRequest = await CreateAPIRequest(endpointURL: configuration["GeodisOutboundAPI"],
                    body: JsonConvert.SerializeObject(productOrderPayload), endpointType: endpointType, convertedData: convertedData, false, productOrderPayload.Customer);
            }

            // set end point name in request
            externalRequest.AdditionalInfo = endpointType.ToString();

            try
            {
                providerResponse = await requestHandler.PostAsync(externalRequest);
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

                result.Messages.Add(new ExecuteMessage()
                {
                    Code = Enums.StatusCode.Success,
                    Description = providerResponse.Result,
                    HttpCode = HttpStatusCode.OK,
                });

                //log in db
                ProductMasterDTO productData = JsonConvert.DeserializeObject<ProductMasterDTO>(externalRequest.Body);

                if (productData != null)
                {
                    await UpdateItemDetails(productData.UpcCode, productData.Plant);
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

                await LogPostProcess(convertedData.ProcessId, convertedData.Endpoint.ToString(), externalRequest, providerResponse, "Failed while posting   data to GEODIS API");

                if (httpStatusCode == System.Net.HttpStatusCode.BadRequest)
                {
                    result.Success = providerResponse.Success;
                    result.Messages.Add(new ExecuteMessage()
                    {
                        Code = Enums.StatusCode.Error,
                        Description = providerResponse.Result,
                        HttpCode = httpStatusCode
                    });
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

            if (endpointType.Equals(Enums.OutboundAPIEndpoint.ProductMaster))
            {
                List<ProductMasterDTO> productData = JsonConvert.DeserializeObject<List<ProductMasterDTO>>(convertedData.ConvertedData);
                var productOrderPayload = productData.ElementAt(currentIndex.Value);

                externalRequest = await CreateAPIRequest(endpointURL: string.Format(configuration["GeodisOutboundAPI"], systemId),
                    body: JsonConvert.SerializeObject(productOrderPayload), endpointType: endpointType, convertedData: convertedData, true, productOrderPayload.Customer);
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
                ProductMasterDTO productData = JsonConvert.DeserializeObject<ProductMasterDTO>(externalRequest.Body);

                if (productData != null)
                {
                    await UpdateItemDetails(productData.UpcCode, productData.Plant);
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

                await LogPostProcess(convertedData.ProcessId, convertedData.Endpoint.ToString(), externalRequest, providerResponse, "Failed while updating  data to GEODIS API");

                if (httpStatusCode == System.Net.HttpStatusCode.BadRequest)
                {
                    result.Success = providerResponse.Success;
                    result.Messages.Add(new ExecuteMessage()
                    {
                        Code = Enums.StatusCode.Error,
                        Description = providerResponse.Result,
                        HttpCode = httpStatusCode
                    });
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
                    var requestData = JsonConvert.SerializeObject(blobResponse.RequestData);
                    //requestData = requestData.TrimStart('\uFEFF');

                    var externalRequest = new ExternalRequest()
                    {
                        Body = requestData
                    };
                    Enums.OutboundAPIEndpoint endpointType;
                    if (Enum.TryParse<Enums.OutboundAPIEndpoint>(blobResponse.InboundEndpoint, true, out endpointType))
                    {
                        result.Result = new OutboundConverterModel();
                        result.Result.Endpoint = endpointType;

                        if (endpointType.Equals(Enums.OutboundAPIEndpoint.ProductMaster))
                        {
                            List<ProductMasterRequestDTO> productData = JsonConvert.DeserializeObject<List<ProductMasterRequestDTO>>(requestData);
                            externalRequest.URL = configuration["GeodisProductMasterConverter"];
                            result.Result.TotalRecords = productData.Count;
                            result.Result.ProcessId = blobResponse.ProcessId;
                        }

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

                if (httpStatusCode == System.Net.HttpStatusCode.BadRequest)
                {
                    result.Success = response.Success;
                    result.Messages.Add(new ExecuteMessage()
                    {
                        Code = Enums.StatusCode.Error,
                        Description = response.Result,
                        HttpCode = httpStatusCode
                    });
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

                await LogPostProcess(blobResponse.ProcessId, blobResponse.InboundEndpoint.ToString(), externalRequest, response, Constants.ErrorNoDataFound);
            }
        }

        private async Task<ExternalRequest> CreateAPIRequest(string endpointURL, string body, Enums.OutboundAPIEndpoint endpointType, OutboundConverterModel convertedData, bool additionalHeadersFlag, string customer)
        {
            var result = new ExternalRequest();
            result.URL = endpointURL;
            result.Body = body;
            result.Header = await GetHeader(endpointType);
            result.BearerToken = convertedData?.Tokens?.FirstOrDefault()?.Token;
            return result;
        }

        private async Task<Dictionary<string, string>> GetHeader(Enums.OutboundAPIEndpoint key)
        {
            var result = new Dictionary<string, string>();
            var headerParamsAll = DeliveryProcessHelper.LoadGeodisHeaderParams();
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
            OutboundTransactionLogModel outboundTransaction = MapOutboundLogDetails(processId, requestType, externalRequest, response, comments);
            await outboundTransactionRepository.CreateAsync(outboundTransaction);
        }

        private async Task UpdateItemDetails(string productCode, string plant)
        {
            string inputQuery = $"SELECT * FROM c WHERE c.data.productCode = '{productCode}' and c.data.plant = '{plant}' order by c.loggedOn desc";

            IEnumerable<ItemMasterModel> productResults = await itemMasterRequestRepository.GetByQueryAsync(inputQuery);

            if (productResults.Any())
            {
                ItemMasterModel product = productResults.First();
                product.DeliveredToProvider = true;
                product.UpdatedOn = Helper.GetCurrentDatetime();
                await itemMasterRequestRepository.UpdateAsync(product.Id, product);
            }
        }

        public async Task<ExecuteResult<OutboundConverterModel>> MapData(OutboundDeliveryMessage deliveryMessage)
        {
            var result = new ExecuteResult<OutboundConverterModel>();

            if (deliveryMessage?.RequestData != null)
            {
                var requestData = JsonConvert.SerializeObject(deliveryMessage.RequestData);

                var externalRequest = new ExternalRequest()
                {
                    Body = requestData
                };

                result.Result = new OutboundConverterModel();
                result.Result.Endpoint = Enums.OutboundAPIEndpoint.ProductMaster;

                List<ProductMasterRequestDTO> productData = JsonConvert.DeserializeObject<List<ProductMasterRequestDTO>>(requestData);
                externalRequest.URL = configuration["GeodisProductMasterConverter"];
                result.Result.TotalRecords = productData.Count;
                result.Result.ProcessId = deliveryMessage.ProcessId;

                await GetConveterMapDetails(deliveryMessage, result, externalRequest);
            }

            return result;
        }
    }
}
