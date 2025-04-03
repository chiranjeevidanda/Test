using NEC.Fulf3PL.Core.Common;

namespace NEC.Fulf3PL.Application.Inbound.Interface
{
    public interface IInventoryAdjustment<T>
    {
        Task<ExecuteResult<T>> GetData(T convertedData);

        Task<ExecuteResult<T>> PostData(T convertedData, string requestURL);

        Task<ExecuteResult<T>> MapData(string request);
    }
}
