using NEC.Fulf3PL.Application.Common.KTN.DTO;
using NEC.Fulf3PL.Core.Common;

namespace NEC.Fulf3PL.Application.Outbound.Implementation.KTN.DTO
{
    public class OutboundConverterModel
    {
        public Enums.OutboundAPIEndpoint Endpoint { get; set; }
        public string ConvertedData { get; set; }
        public int TotalRecords { get; set; }
        public int? CurrentIndex { get; set; }
        public IEnumerable<GenerateTokenDTO> Tokens { get; set; }
        public string ProcessId { get; set; }
    }
}
