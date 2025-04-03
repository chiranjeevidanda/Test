using NEC.Fulf3PL.Application.Inbound.Implementation.KTN.Model;
using NEC.Fulf3PL.Core.Common;
using NEC.Fulfillment.Application.Models;

namespace NEC.Fulf3PL.Application.Inbound.Interface
{
    public interface IGoodsIssuedOrder<T>
    {
        Task<ExecuteResult<T>> GetData(T convertedData);

        Task<ExecuteResult<T>> PostData(T convertedData, string requestURL);

        Task<ExecuteResult<T>> MapData(string request);
    }
}
