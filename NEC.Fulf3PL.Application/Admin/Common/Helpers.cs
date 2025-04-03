using NEC.Fulf3PL.Core.Common.Admin;
using Newtonsoft.Json.Linq;

namespace NEC.Fulf3PL.Application.Admin.Common
{
    public static class Helpers
    {
        public const string ISO_TIMESTAMP_FORMAT = "yyyy-MM-ddTHH:mm:ssZ";
        public static string ToOutboundDocumentType(string functionName) => functionName switch
        {
            OutboundTransactionRequestType.PurchaseOrder => TransactionOrderType.PoCreate,
            OutboundTransactionRequestType.CreateOrder => TransactionOrderType.OrderCreate,
            OutboundTransactionRequestType.ReturnOrder => TransactionOrderType.ReturnOrder,
            OutboundTransactionRequestType.ProductMaster => TransactionOrderType.SKU,
            _ => string.Empty
        };

        public static string OutboundApiErrorReportSubject(string functionName, string emailEnvironment, string provider) => functionName switch
        {
            OutboundTransactionRequestType.PurchaseOrder => $"{provider} ({emailEnvironment}) - Failure when sending Inbound PO to {provider}",
            OutboundTransactionRequestType.CreateOrder => $"{provider} ({emailEnvironment}) - Failure when sending Order Create to {provider}",
            OutboundTransactionRequestType.ReturnOrder => $"{provider} ({emailEnvironment}) - Failure when sending Return Order to {provider}",
            OutboundTransactionRequestType.ProductMaster => $"{provider} ({emailEnvironment}) - Failure when sending SKU Create to {provider} ",
            _ => throw new NotImplementedException(),
        };

        public static string? GetLinNo(JObject? entryDetails)
        {
            return entryDetails?["scheduleLineNo"]?.ToString() ?? string.Empty;
        }

        public static string? GetLastFiveCharacters(JObject? entryDetails)
        {
            string input = entryDetails?["scheduleLineNo"]?.ToString() ?? string.Empty;
            return input.Length > 5 ? input[^5..] : input;
        }

        public static string? GetFirstFourCharacters(JObject? entryDetails)
        {
            string input = entryDetails?["scheduleLineNo"]?.ToString() ?? string.Empty;
            return input.Length > 4 ? input[..4] : input;
        }

        public static string GetFormattedDate(this Nullable<DateTime> value)
        {
            if (value == null) return string.Empty;

            TimeZoneInfo estTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time");
            DateTime currentTimeEst = TimeZoneInfo.ConvertTimeFromUtc(value.Value.ToUniversalTime(), estTimeZone);

            string formattedDateTime = currentTimeEst.ToString(ISO_TIMESTAMP_FORMAT);
            return formattedDateTime;
        }
    }
}
