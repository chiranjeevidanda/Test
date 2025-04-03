namespace NEC.Fulf3PL.Core.DTO.Admin
{
    public class InboundItemMasterReportDto
    {
        public string? Plant { get; set; }

        public string? ProductCode { get; set; }

        public string? Provider { get; set; }

        public List<object>? Entries { get; set; }

        public List<AdditionalDataDTO> AdditionalData { get; set; }

        public DateTime UpdatedOn { get; set; }
    }
}
