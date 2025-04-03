using AutoMapper;
using MediatR;
using NEC.Fulf3PL.Application.Admin.Services;
using NEC.Fulf3PL.Core.Contact.Admin;
using NEC.Fulf3PL.Core.Entities.Admin;
using Newtonsoft.Json.Linq;

namespace NEC.Fulf3PL.Application.Admin.Command;

public record UpsertAdminOutboundRquestsCommand(OutboundResponseDto Dto) : IRequest<string>;

public class UpsertAdminOutboundRquestsCommandHandler
    : IRequestHandler<UpsertAdminOutboundRquestsCommand, string>
{
    private readonly IAdminOutboundRequestsDocumentRepository _repository;
    private readonly IMapper _mapper;

    public UpsertAdminOutboundRquestsCommandHandler(IAdminOutboundRequestsDocumentRepository repository,
    IAdminOutboundRequestsQueryService queryService, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<string> Handle(UpsertAdminOutboundRquestsCommand request, CancellationToken cancellationToken)
    {
        var entity = _mapper.Map<OutboundRequests>(request.Dto);

        if (!string.IsNullOrEmpty(request.Dto.Response))
        {
            var parsedResponse = JObject.Parse(request.Dto.Response);
            var responseSystemId = parsedResponse["systemId"]?.ToString();
            if (!string.IsNullOrEmpty(responseSystemId))
            {
                entity.SystemId = responseSystemId;
            }
        }

        await _repository.CreateAsync(entity);

        return entity.Id;
    }
}