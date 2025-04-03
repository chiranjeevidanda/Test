
using Azure.Core;
using NEC.Fulf3PL.Core.Common;
using NEC.Fulf3PL.Core.Entities;
using Newtonsoft.Json;

namespace NEC.Fulf3PL.Application.Inbound.Implementation.KTN.Common
{
    public static class DeliveryProcessMapper
    {

        public static InboundTransactionLogModel MapInboundLogDetails(string processId, string requestType, ExternalRequest externalRequest, ExecuteResult<string> response, string comments)
        {
            object responseObject;
            if (response.Result is string)
            {
                responseObject = JsonConvert.DeserializeObject(response.Result) ?? "";
            }
            else
            {
                responseObject = response.Result;
            }

            InboundTransactionLogModel inboundTransactionLogModel = new InboundTransactionLogModel()
            {
                Id = Guid.NewGuid().ToString(),
                InboundRequestId = processId,
                Provider = Enums.WarehouseProvider.KTN.ToString(),
                RequestType = requestType,
                RequestURL = externalRequest.URL,
                LoggedOn = Helper.GetCurrentDatetime(),
                RequestPayload = (externalRequest.Body != null) ? JsonConvert.SerializeObject(externalRequest.Body) : string.Empty,
                Headers = (externalRequest.Header != null) ? JsonConvert.SerializeObject(externalRequest.Header) : string.Empty,
                Response = responseObject,
                Status = response.Success,
                Comments = comments
            };

            return inboundTransactionLogModel;

        }


        public static SofiaInboundRequestsLogModel MapSofiaInboundLogDetails(string processId, string requestType, ExternalRequest externalRequest, ExecuteResult<string> response, string comments)
        {
            object responseObject;
            if (response.Result is string)
            {
                responseObject = JsonConvert.DeserializeObject(response.Result) ?? "";
            }
            else
            {
                responseObject = response.Result;
            }

            object requestObject;
            if (externalRequest.Body is string)
            {
                requestObject = JsonConvert.DeserializeObject(externalRequest.Body) ?? "";
            }
            else
            {
                requestObject = externalRequest.Body;
            }

            SofiaInboundRequestsLogModel sofiaInboundRequestsLogModel = new SofiaInboundRequestsLogModel()
            {
                Id = Guid.NewGuid().ToString(),
                InboundRequestId = processId,
                Provider = Enums.WarehouseProvider.KTN.ToString(),
                RequestType = requestType,
                RequestURL = externalRequest.URL,
                LoggedOn = Helper.GetCurrentDatetime(),
                RequestPayload = requestObject,
                Headers = (externalRequest.Header != null) ? JsonConvert.SerializeObject(externalRequest.Header) : string.Empty,
                Response = responseObject,
                Status = response.Success,
                Comments = comments
            };

            return sofiaInboundRequestsLogModel;

        }

    }
}
