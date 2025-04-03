using NEC.Fulf3PL.Application.Admin.Dtos;
using NEC.Fulf3PL.Core.Entities.Admin;

namespace NEC.Fulf3PL.Application.Common;

public interface ISpreadSheetService
{
    byte[] Export(string? requestType, IEnumerable<OutboundResponseDto> transactions);
    byte[] Export(string? requestType, IEnumerable<InboundTransactionsDto> transactions);
    byte[] Export(string? requestType, IEnumerable<OutboundProductDetailsDto> transactions);
}