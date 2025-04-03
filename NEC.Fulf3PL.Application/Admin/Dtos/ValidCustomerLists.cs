using NEC.Fulf3PL.Core.Common.Admin;

namespace NEC.Fulf3PL.Application.Admin.Dtos;

public class ValidCustomerLists
{
    public List<string> InventoryCustomer { get; set; }
    public List<string> GoodsIssuedCustomer { get; set; }
    public List<string> GoodsReceivedCustomer { get; set; }
    public List<string> ReturnReceivedCustomer { get; set; }
    public List<string> ReturnOrderCustomer { get; set; }
    public List<string> PurchaseOrderCustomer { get; set; }
    public List<string> CreateOrderCustomer { get; set; }
    public List<string> ProductMasterCustomer { get; set; }
}
