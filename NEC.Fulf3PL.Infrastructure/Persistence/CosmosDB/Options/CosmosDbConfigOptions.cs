namespace NEC.Fulf3PL.Infrastructure.Persistence.CosmosDB.Options;

public class CosmosDbConfigOptions
{
    public const string SectionName = "CosmosDbSettings";

    public string ConnectionString { get; set; } = string.Empty;

    public string DatabaseId { get; set; } = string.Empty;

    public ContainerOptions AdminOutboundRequestsContainer { get; set; } = new ContainerOptions
    {
        ContainerId = "AdminOutboundRequests"
    };

    public ContainerOptions AdminSapTransactionLogContainer { get; set; } = new ContainerOptions
    {
        ContainerId = "AdminSAPTransactionLog"
    };
    
    public ContainerOptions ItemMasterContainer { get; set; } = new ContainerOptions
    {
        ContainerId = "ItemMaster"
    };
}

