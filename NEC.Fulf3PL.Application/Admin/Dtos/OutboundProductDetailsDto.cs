using Newtonsoft.Json;
using NEC.Fulf3PL.Application.Outbound.Implementation.KTN.DTO;

namespace NEC.Fulf3PL.Application.Admin.Dtos
{
    public class OutboundProductDetailsDto
    {
        public string? ModifiedDate { get; set; }
        public string? Status { get; set; }
        public ProductMasterDTO ProductDetails { get; set; }
        public string? Provider { get; set; }
    }
}
