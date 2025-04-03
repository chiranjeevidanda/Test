using NEC.Fulf3PL.Core.Common;

namespace NEC.Fulf3PL.Application.Outbound.Interface
{
    public interface ILineMaterialService<T>
    {
        Task<ExecuteResult<bool>> PostData(T convertedData, string requestURL);
    }
}
