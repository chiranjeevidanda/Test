namespace NEC.Fulf3PL.Application.Admin.Options;

public class EmailNotificationOptions
{
    public const string SectionName = "EmailNotificationSettings";

    private string? _recipientEmails;
    public string? RecipientEmails
    {
        get => _recipientEmails;
        set
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException("No recipient emails found to send the email.", nameof(value));
            }

            RecipientEmailList = value.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                                         .Select(email => email.Trim())
                                         .ToList();

            _recipientEmails = value;
        }
    }

    public string? ProductMaster { get; set; }
    public string? PurchaseOrder { get; set; }
    public string? CreateOrder { get; set; }
    public string? ReturnOrder { get; set; }
    public string? Inventory { get; set; }
    public string? ReturnReceived { get; set; }
    public string? GoodsReceived { get; set; }

    public string? Environment { get; set; }
    public string? Provider { get; set; }

    public List<string>? RecipientEmailList { get; private set; } = new List<string>();
}
