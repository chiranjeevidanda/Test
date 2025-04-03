using NEC.Fulf3PL.Core.Common;

namespace NEC.Fulf3PL.Application.Outbound.Interface
{
    public interface IMasterDataService<T>
    {
        Task<ExecuteResult<bool>> PostData(T convertedData, string customer);

        Task<ExecuteResult<bool>> PutData(T convertedData, string systemId, string customer);

        Task<ExecuteResult<T>> MapData(string request);
    }
}
