namespace NEC.Fulf3PL.Core.Common.Admin;
public static class ConstantsHelpers
{
    public const string GoodsReceived = "GoodsReceived";
    public const string GoodsIssued = "GoodsIssued";
    public const string ReturnReceipt = "ReturnReceipt";
    public const string Inventory = "Inventory";
    public const string Timestamp = "Timestamp";


    public static string InboubdApiErrorReportSubject(string functionName, string emailEnvironment, string provider) => functionName switch
    {
        InboundTransactionRequestType.Inventory => $"{provider} - Error while posting Inventory Adjustment data to SAP ({emailEnvironment})",
        InboundTransactionRequestType.GoodsIssued => $"{provider} - Error while posting Goods Issued data to SAP ({emailEnvironment})",
        InboundTransactionRequestType.ReturnReceived => $"{provider} - Error while posting Return Receipt data to SAP ({emailEnvironment})",
        InboundTransactionRequestType.GoodsReceived => $"{provider} - Error while posting PO Receipt data to SAP ({emailEnvironment})",
        _ => string.Empty
    };

    public static string InboubdApiErrorReportAttachmentName(string functionName, string provider) => functionName switch
    {
        InboundTransactionRequestType.Inventory => $"{provider}-Inventory-Adjustment.xlsx",
        InboundTransactionRequestType.GoodsIssued => $"{provider}-Goods-Issued.xlsx",
        InboundTransactionRequestType.ReturnReceived => $"{provider}-Return-Receipt.xlsx",
        InboundTransactionRequestType.GoodsReceived => $"{provider}-PO-Receipt.xlsx",
        _ => string.Empty
    };


    public static string GetDirection(string? direction) => direction switch
    {
        "Decrement" => "-",
        "Increment" => "+",
        _ => string.Empty
    };
}
