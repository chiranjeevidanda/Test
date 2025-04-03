namespace NEC.Fulf3PL.Application.Common.EmailSenderService;
public interface IEmailService
{
    public Task SendMail(string subject, IEnumerable<string> recipientEmails, string content);
    public Task SendMail(byte[] fileContent, IEnumerable<string> recipientEmails, string subject, string fileName);
}
