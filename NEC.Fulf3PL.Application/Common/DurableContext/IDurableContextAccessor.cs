namespace NEC.Fulf3PL.Application.Common.DurableContext;

public interface IDurableContextAccessor
{
    public DurableContext? Current { get; }
}
