using NEC.Fulf3PL.Core.Common;

namespace NEC.Fulf3PL.Application.Inbound.Interface
{
    public interface IGoodsReceivedPO<T>
    {
        Task<ExecuteResult<T>> GetData(T convertedData);

        Task<ExecuteResult<T>> PostData(T convertedData);

        Task<ExecuteResult<T>> MapData(string request);
    }
}
