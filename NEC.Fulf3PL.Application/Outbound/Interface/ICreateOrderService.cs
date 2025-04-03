using NEC.Fulf3PL.Core.Common;

namespace NEC.Fulf3PL.Application.Outbound.Interface
{
    public interface ICreateOrderService<T>
    {
        Task<ExecuteResult<bool>> PostData(T convertedData);

        Task<ExecuteResult<bool>> PutData(T convertedData, string systemId);

        Task<ExecuteResult<T>> MapData(string request);
    }
}
