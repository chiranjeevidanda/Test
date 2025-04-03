using MediatR;
using NEC.Fulf3PL.Application.Admin.Services;
using NEC.Fulf3PL.Core.Common;
using NEC.Fulf3PL.Core.Common.Admin;
using NEC.Fulf3PL.Core.Contact.Admin;
using NEC.Fulf3PL.Core.Entities.Admin;
using Newtonsoft.Json.Linq;

namespace NEC.Fulf3PL.Application.Admin.Command;

public record UpsertInboundRquestsCommand(SapTransactionLog entity) : IRequest<string>;

public class UpsertInboundRquestsCommandHandler
    : IRequestHandler<UpsertInboundRquestsCommand, string>
{
    private readonly IAdminSapTransactionLogRepository _repository;
    private readonly ISapTransactionsQueryService _queryService;

    public UpsertInboundRquestsCommandHandler(IAdminSapTransactionLogRepository repository,
    ISapTransactionsQueryService queryService)
    {
        _repository = repository;
        _queryService = queryService;
    }

    public async Task<string> Handle(UpsertInboundRquestsCommand request, CancellationToken cancellationToken)
    {
        var existingEntity = await _queryService.GetItemAsync(x => x.Id == request.entity.EventId, x => x);

        if (request.entity.WebhookPayload == null)
        {
            request.entity.WebhookPayload = existingEntity?.WebhookPayload;
        }

        request.entity.ModifiedDate = Helper.GetCurrentDatetime();

        if (request.entity.Status != null)
        {
            request.entity.TransactionStatus = request.entity.Status == true ? TransactionStatus.Success : TransactionStatus.Failed;
        }

        if (existingEntity != null)
        {
            request.entity.Id = existingEntity.Id;
            if (string.IsNullOrEmpty(request.entity.ProcessId))
            {
                request.entity.ProcessId = existingEntity.ProcessId;
            }
            await _repository.UpdateAsync(request.entity);

            return existingEntity.Id;
        }
        else
        {
            await _repository.CreateAsync(request.entity);

            return request.entity.Id;
        }
    }
}