using NEC.Fulf3PL.Core.Entities.Admin;

namespace NEC.Fulf3PL.Application.Common.DurableContext;

public record DurableContext(
    string InstanceId,
    string ActionName);
