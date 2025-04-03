﻿using NEC.Fulf3PL.Core.Common;

namespace NEC.Fulf3PL.Application.Outbound.Interface.COMMON
{
    public interface IPurchaseOrderServiceCommon<T>
    {
        Task<ExecuteResult<bool>> PostData(T convertedData);

        Task<ExecuteResult<bool>> PutData(T convertedData, string systemId);

        Task<ExecuteResult<T>> MapData(string request);
    }
}
