using Microsoft.Extensions.Options;
using MimeKit;
using MailKit.Security;
using MailKit.Net.Smtp;
using NEC.Fulf3PL.Infrastructure.EmailSenderService.Options;
using NEC.Fulf3PL.Application.Common.EmailSenderService;
using Ardalis.GuardClauses;

namespace NEC.Fulf3PL.Infrastructure.EmailSenderService;
public class EmailService: IEmailService
{
    private readonly SmtpOptions _smtpOptions;
    public EmailService(IOptions<SmtpOptions> smtpOptions)
    {
        _smtpOptions = smtpOptions.Value;
    }
    public async Task SendMail(string subject, IEnumerable<string> recipientEmails, string content)
    {
        var message = new MimeMessage();
        message.From.Add(MailboxAddress.Parse(_smtpOptions.SenderEmail));
        message.To.AddRange(recipientEmails.Select(x => MailboxAddress.Parse(x)));
        message.Subject = subject;
        var bodyBuilder = new BodyBuilder
        {
            TextBody = content
        };
        message.Body = bodyBuilder.ToMessageBody();
        using var client = new SmtpClient();
        await client.ConnectAsync(_smtpOptions.SmtpServer, _smtpOptions.Port, SecureSocketOptions.StartTls);
        await client.AuthenticateAsync(_smtpOptions.SenderEmail, _smtpOptions.Password);
        await client.SendAsync(message);
        await client.DisconnectAsync(true);
    }

    public async Task SendMail(byte[] fileContent, IEnumerable<string> recipientEmails, string subject, string fileName)
    {
        var message = new MimeMessage();
        message.From.Add(MailboxAddress.Parse(_smtpOptions.SenderEmail));
        Guard.Against.Null(recipientEmails);
        message.To.AddRange(recipientEmails.Select(x => MailboxAddress.Parse(x)));
        message.Subject = subject;

        var bodyBuilder = new BodyBuilder();
        bodyBuilder.TextBody = "Please find the attached report for more details";

        var attachment = new MimePart("application", "octet-stream")
        {
            Content = new MimeContent(new MemoryStream(fileContent), ContentEncoding.Default),
            ContentDisposition = new ContentDisposition(ContentDisposition.Attachment),
            ContentTransferEncoding = ContentEncoding.Base64,
            FileName = fileName
        };

        bodyBuilder.Attachments.Add(attachment);

        message.Body = bodyBuilder.ToMessageBody();

        using (var client = new SmtpClient())
        {
            await client.ConnectAsync(_smtpOptions.SmtpServer, _smtpOptions.Port, SecureSocketOptions.StartTls);
            await client.AuthenticateAsync(_smtpOptions.SenderEmail, _smtpOptions.Password);
            await client.SendAsync(message);
            await client.DisconnectAsync(true);
        }
    }
}
