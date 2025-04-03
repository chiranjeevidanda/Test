
using Azure.Core;
using NEC.Fulf3PL.Core.Common;
using NEC.Fulf3PL.Core.Entities;
using Newtonsoft.Json;

namespace NEC.Fulf3PL.Application.Outbound.Implementation.GEODIS.Common
{
    public static class DeliveryProcessMapper
    {

        public static OutboundTransactionLogModel MapOutboundLogDetails(string processId, string requestType, ExternalRequest externalRequest, ExecuteResult<string> response, string comments)
        {
            OutboundTransactionLogModel outboundTransactionLogModel = new OutboundTransactionLogModel()
            {
                Id = Guid.NewGuid().ToString(),
                OutboundRequestId = processId,
                Provider = Enums.WarehouseProvider.GEODIS.ToString(),
                RequestType = requestType,
                RequestURL = externalRequest.URL,
                LoggedOn = Helper.GetCurrentDatetime(),
                RequestPayload = JsonConvert.DeserializeObject(externalRequest.Body) ?? "",
                Headers = (externalRequest.Header != null && externalRequest.Header.Any()) ? JsonConvert.SerializeObject(externalRequest.Header) : string.Empty,
                Response = JsonConvert.DeserializeObject(response.Result) ?? "",
                Status = response.Success,
                Comments = comments
            };

            return outboundTransactionLogModel;

        }

    }
}
