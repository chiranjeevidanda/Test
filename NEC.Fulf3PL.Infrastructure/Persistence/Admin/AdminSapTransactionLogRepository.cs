using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Options;
using NEC.Fulf3PL.Core.Contact.Admin;
using NEC.Fulf3PL.Core.Entities.Admin;
using NEC.Fulf3PL.Infrastructure.Persistence.CosmosDB;
using NEC.Fulf3PL.Infrastructure.Persistence.CosmosDB.Options;

namespace NEC.Fulf3PL.Infrastructure.Persistence.Admin;

public class AdminSapTransactionLogRepository :
    GenericRepository<SapTransactionLog>, IAdminSapTransactionLogRepository
{
    public AdminSapTransactionLogRepository(CosmosClient cosmosClient, IOptions<CosmosDbConfigOptions> options)
        : base(cosmosClient, options.Value, options.Value.AdminSapTransactionLogContainer)
    {
    }
}