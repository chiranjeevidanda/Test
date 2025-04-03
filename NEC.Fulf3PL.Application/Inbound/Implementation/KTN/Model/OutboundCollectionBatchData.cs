using Azure.Core;
using NEC.Fulf3PL.Application.Common.KTN.DTO;
using NEC.Fulf3PL.Application.Inbound.Implementation.KTN.DTO;
using NEC.Fulf3PL.Core.Common;

namespace NEC.Fulf3PL.Application.Outbound.Implementation.KTN.DTO
{
    public class OutboundCollectionBatchData
    {
        public string InboundConverterModel { get; set; }
        public string OutboundId { get; set; }
        public ExecuteResult<GenerateTokenDTO> TokenGenerateResponse { get; set; }
        public NotificationDetails Notification { get; set; }
        public string CompanyId { get; set; }
        public ExecuteResult<bool> Result { get; set; }
        public string OutboundRequestId { get; set; }
    }

}
