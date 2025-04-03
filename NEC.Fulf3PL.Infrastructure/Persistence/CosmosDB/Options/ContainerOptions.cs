namespace NEC.Fulf3PL.Infrastructure.Persistence.CosmosDB.Options;

public class ContainerOptions
{
    public string ContainerId { get; set; } = string.Empty;

    public string PartitionKey { get; set; } = "id";
}
