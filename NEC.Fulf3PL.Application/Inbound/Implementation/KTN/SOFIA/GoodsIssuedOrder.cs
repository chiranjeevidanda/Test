using Microsoft.Extensions.Logging;
using NEC.Fulf3PL.Core.Common;
using NEC.Fulf3PL.Core.Interface;
using System.Net;
using NEC.Fulf3PL.Application.Inbound.Interface;
using NEC.Fulf3PL.Application.Inbound.Implementation.KTN.Model;
using NEC.Fulf3PL.Application.Inbound.Implementation.KTN.Common;
using NEC.Fulf3PL.Core.Entities;
using static NEC.Fulf3PL.Application.Inbound.Implementation.KTN.Common.DeliveryProcessMapper;

namespace NEC.Fulf3PL.Application.Inbound.Implementation.KTN.SOFIA
{
    public class SofiaGoodsIssuedOrder : IGoodsIssuedOrder<InboundConverterModel>
    {
        private readonly ILogger<SofiaGoodsIssuedOrder> logger;
        private readonly IRequestHandler requestHandler;
        private readonly ISofiaInboundRequestsRepository sofiaInboundRequestsRepository;

        public SofiaGoodsIssuedOrder(ILogger<SofiaGoodsIssuedOrder> logger, IRequestHandler requestHandler, ISofiaInboundRequestsRepository sofiaInboundRequestsRepository)
        {
            this.logger = logger;
            this.requestHandler = requestHandler;
            this.sofiaInboundRequestsRepository = sofiaInboundRequestsRepository;
        }

        public Task<ExecuteResult<InboundConverterModel>> GetData(InboundConverterModel convertedData)
        {
            throw new NotImplementedException();
        }

        public Task<ExecuteResult<InboundConverterModel>> MapData(string request)
        {
            throw new NotImplementedException();
        }

        public async Task<ExecuteResult<InboundConverterModel>> PostData(InboundConverterModel data, string requestURL)
        {
            var result = new ExecuteResult<InboundConverterModel>();
            var providerResponse = new ExecuteResult<string>();

            result.Result = new InboundConverterModel();

            var externalRequest = new ExternalRequest();
            externalRequest.ContentType = "application/json";
            externalRequest.URL = requestURL;
            externalRequest.Body = data.ConvertedData;
            externalRequest.Header = await GetHeader(data.CompanyId);

            try
            {
                providerResponse = await requestHandler.PostAsync(externalRequest);
            }
            catch (Exception ex)
            {
                //log in db
                providerResponse.Success = false;
                await LogPostProcess(data.ProcessId, data.Endpoint.ToString(), externalRequest, providerResponse, $"Error - {ex.Message}");

                throw;
            }
            if (providerResponse.Success && providerResponse.Result != null)
            {
                result.Success = providerResponse.Success;
                //result.Result.ConvertedData = providerResponse.Result;

                logger.LogInformation("Data posted to Sofia.....");

                //log in db
                await LogPostProcess(data.ProcessId, data.Endpoint.ToString(), externalRequest, providerResponse, "Success");

            }
            else if (!providerResponse.Success)
            {
                var httpStatusCode = providerResponse.Messages.FirstOrDefault().HttpCode;

                logger.LogError("API Failed while posting data to Sofia.....");

                //log in db
                result.Success = providerResponse.Success;

                await LogPostProcess(data.ProcessId, data.Endpoint.ToString(), externalRequest, providerResponse, "Failed while getting data from KTN API");

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
                //log in db
                result.Success = false;
                //log in db
                await LogPostProcess(data.ProcessId, data.Endpoint.ToString(), externalRequest, providerResponse, Constants.ErrorNoDataFound);

                result.Messages = new List<ExecuteMessage>()
                    {
                        new ExecuteMessage() { Code = Enums.StatusCode.NoRecordFound, Description = Constants.ErrorNoDataFound },
                    };
            }
            return result;
        }

        private async Task<Dictionary<string, string>> GetHeader(string region)
        {
            var result = new Dictionary<string, string>();
            var headerParamsAll = DeliveryProcessHelper.LoadSofiaHeaderParams();
            var headerParams = new List<(string name, string value)>();
            if (headerParamsAll.TryGetValue(region, out headerParams))
            {
                foreach (var item in headerParams)
                {
                    result.Add(item.name, item.value);
                }
            }

            return result;
        }

        private async Task LogPostProcess(string processId, string requestType, ExternalRequest externalRequest, ExecuteResult<string> response, string comments)
        {
            SofiaInboundRequestsLogModel inboundTransaction = MapSofiaInboundLogDetails(processId, requestType, externalRequest, response, comments);
            await sofiaInboundRequestsRepository.CreateAsync(inboundTransaction);
        }
    }
}
