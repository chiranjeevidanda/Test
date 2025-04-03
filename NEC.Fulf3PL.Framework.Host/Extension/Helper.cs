using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NEC.Fulf3PL.Framework.Host.Extension
{
    public static class FuncHelper
    {
        public static RateLimitData GetAPIRateLimitSettings(string name, string endpoint, int limit, int periodInSeconds)
        {
            return new RateLimitData
            {
                Name = name,
                Endpoint = endpoint,
                Limit = limit,
                PeriodInSeconds = periodInSeconds
            };
        }

        //declare ENUM for RateLimitType
        public enum RateLimitAPI
        {
            CreateItemColorAPI,
            CreateItemDesignAPI,
            CreateItemModelAPI,
            CreateItemSizeAPI,
            CreateItemSeasonAPI,
            DeliveryServiceCreateOrderAPI,
            DeliveryServiceItemMasterPostDataAPI,
            DeliveryServicePurchaseOrderAPI,
            DeliveryServiceReturnOrderAPI,
        }

    }
}
