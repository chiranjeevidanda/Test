using Ardalis.GuardClauses;
using NEC.Fulf3PL.Application.Common.DurableContext;

namespace NEC.Fulf3PL.Infrastructure.Common.Context;

public class DurableContextManager : IDurableContextManager, IDurableContextAccessor
{
    private static readonly AsyncLocal<DurableContext> Context = new();

    public DurableContext? Current => Context.Value;

    public void SetInstanceId(string instanceId, string functionName)
    {
        var durableContext = new DurableContext(instanceId, functionName);

        Context.Value = durableContext;
    }
}
