namespace NEC.Fulf3PL.Infrastructure.EmailSenderService.Options;
public class SmtpOptions
{
    public const string SectionName = "SmtpSettings";

    public string SenderEmail { get; set; } = string.Empty;

    public string Password { get; set; } = string.Empty;

    public string SmtpServer { get; set; } = string.Empty;

    public int Port { get; set; }
}
