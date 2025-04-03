using NEC.Fulf3PL.Application.Admin.Dtos;

namespace NEC.Fulf3PL.Application.Admin.Services;

public interface IRetriggerDocumentService
{
    public Task PostRetriggerDocuments(RetriggerDocumentsListDto request);
    public Task PostCreateSku(List<RetriggerEventDetail> request);
}
