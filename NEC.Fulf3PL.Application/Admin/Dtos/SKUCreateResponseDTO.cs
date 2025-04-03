namespace NEC.Fulf3PL.Application.Admin.Dtos
{
    public class SKUCreateResponseDTO
    {
        public int SuccessCount { get; set; }

        public long ActiveMessageCount { get; set; }

        public List<string> SkuProcessed { get; set; }

        public List<string> SentForProcess { get; set; }
    }
}