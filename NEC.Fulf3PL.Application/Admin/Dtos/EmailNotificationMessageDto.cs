using Newtonsoft.Json;

namespace NEC.Fulf3PL.Application.Admin.Dtos
{
    public class EmailNotificationMessageDto
    {
        public string EventId { get; set; } = string.Empty;

        public string ErrorMessage { get; set; } = string.Empty;

        public string RequestPayload { get; set; } = string.Empty;
        
        public DateTime LoggedOn { get; set; }

        public string Status { get; set; } = string.Empty;
    }
}
