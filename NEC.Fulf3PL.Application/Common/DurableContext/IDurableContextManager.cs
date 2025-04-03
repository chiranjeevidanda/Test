namespace NEC.Fulf3PL.Application.Common.DurableContext;

public interface IDurableContextManager : IDurableContextAccessor
{
    public void SetInstanceId(string instanceId, string functionName);
}
