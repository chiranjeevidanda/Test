namespace NEC.Fulf3PL.Core.Common.Admin;

public static class InboundTransactionRequestType
{
    public const string GoodsReceived = "PO Receipt";
    public const string GoodsIssued = "Shipment";
    public const string ReturnReceived = "Return Receipt";
    public const string Inventory = "Inventory";
}