using NEC.Fulf3PL.Application.Outbound.Implementation.COMMON.Models;
using NEC.Fulf3PL.Core.Common;

namespace NEC.Fulf3PL.Application.Outbound.Interface.COMMON
{
    public interface IItemMasterServiceCommon<T>
    {
        Task<ExecuteResult<bool>> PostData(T convertedData);

        Task<ExecuteResult<bool>> PutData(T convertedData, string systemId);

        Task<ExecuteResult<T>> MapData(string request);

        Task<ExecuteResult<T>> MapData(OutboundDeliveryMessage deliveryMessage);
    }
}
