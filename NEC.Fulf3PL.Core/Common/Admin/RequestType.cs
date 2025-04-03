namespace NEC.Fulf3PL.Core.Common.Admin;

public static class RequestType
{
    public const string GoodsReceived = nameof(GoodsReceived);
    public const string GoodsIssued = nameof(GoodsIssued);
    public const string ReturnReceipt = nameof(ReturnReceipt);
    public const string Inventory = nameof(Inventory);
    public const string PurchaseOrder = nameof(PurchaseOrder);
    public const string ProductMaster = nameof(ProductMaster);
    public const string CreateOrder = nameof(CreateOrder);
    public const string ReturnOrder = nameof(ReturnOrder);
}
