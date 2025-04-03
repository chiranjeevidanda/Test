namespace NEC.Fulf3PL.Application.Admin.Dtos
{
    public class RetriggerResponseDto
    {
        public int SuccessCount { get; set; }
        public long ActiveMessageCount { get; set; }
        public string PayloadType { get; set; }
    }
}