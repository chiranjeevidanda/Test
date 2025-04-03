using NEC.Fulf3PL.Core.Common;

namespace NEC.Fulf3PL.Application.Inbound.Interface.GEODIS
{
    public interface IGeodisInboundService<T>
    {
        Task<ExecuteResult<T>> GetData(T convertedData, string requestURL);

        Task<ExecuteResult<T>> PostData(T convertedData, string requestURL);

        Task<ExecuteResult<T>> MapData(string request, string requestURL);
    }
}
