using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NEC.Fulf3PL.Core.Common
{
    public class Enums
    {
        public enum TraceLogStatus
        {
            [Description("None")]
            None,

            [Description("Request posted to the Endpoint.")]
            Request_Posted,

            [Description("Request successfully posted to the Endpoint.")]
            RequesPost_Success,

            [Description("Request failed on posted to the Endpoint.")]
            RequesPost_Failed
        }

        public enum StatusCode
        {

            [Description("Success")]
            Success,

            [Description("Error")]
            Error,

            [Description("No Record Found")]
            NoRecordFound,
        }

        public enum BlobStorageType
        {
            OutboundPayload,
            InboundRequest
        }
        public enum APIRequestHandlerType
        {
            Common,
            KTN
        }

        public enum OutboundAPIEndpoint
        {
            [Description("Product Master")]
            ProductMaster,

            [Description("Purchase Order")]
            PurchaseOrder,

            [Description("Create Order")]
            CreateOrder,

            [Description("Order Return")]
            ReturnOrder,

            [Description("Color")]
            Color,

            [Description("Design")]
            Design,

            [Description("Size")]
            Size,

            [Description("Model")]
            Model,

            [Description("Season")]
            Season,

            [Description("Other")]
            Other
        }

        public enum InboundAPIEndpoint
        {
            [Description("Transport Status")]
            TransportStatus,

            [Description("Transport Collection")]
            TransportCollection,

            [Description("Transport Links")]
            TransportLinks,

            [Description("Transport Stock Change")]
            TransportStockChange,

            [Description("Transport Expand Lines")]
            TransportExpandLines,

            [Description("Transport Inbounds")]
            TransportInbounds,

            [Description("Outbound Status")]
            OutboundStatus,

            [Description("Outbound Parcels")]
            OutboundParcels,

            [Description("Outbound Pallets")]
            OutboundPallets,

            [Description("Outbound Stock Changes")]
            OutboundStockChanges,

            [Description("Outbound Line Parcels")]
            OutboundLineParcels,

            [Description("Outbound Expand Lines")]
            OutboundExpandLines,

            [Description("Outbounds")]
            Outbounds,

            [Description("TransportOutbounds")]
            TransportOutbounds,

            [Description("Transport Outbound Status")]
            TransportOutboundStatus,

            [Description("Transport Outbound Links")]
            TransportOutboundLinks,

            [Description("Inventory")]
            Inventory,

            [Description("Outbound Collection")]
            OutboundCollection,

            [Description("Transport Outbound Collection")]
            TransportOutboundCollection,

            [Description("Sofia Order Acknowledged")]
            SofiaOrderAcknowledged,

            [Description("Sofia Order Allocated")]
            SofiaOrderAllocated,

            [Description("Sofia Order Shipped")]
            SofiaOrderShipped,

            [Description("Sofia Order Cancelled")]
            SofiaOrderCancelled,

            [Description("Sofia Order Return")]
            SofiaOrderReturn,

            [Description("Return Receipt")]
            ReturnReceipt,

            [Description("Return Expand Lines")]
            ReturnExpandLines,

            [Description("Brian Inventory Event")]
            BrianInventoryEvent,

            [Description("Goods Receipt")]
            GoodsReceipt,

        }

        public enum WarehouseProvider
        {
            [Description("ktn")]
            KTN,

            [Description("sofia")]
            SOFIA,

            [Description("geodis")]
            GEODIS,

            [Description("sofia-geodis")]
            SOFIA_GEODIS,
        }

        public enum Region
        {
            [Description("uk")]
            UK,

            [Description("eu")]
            EU,

            [Description("brian")]
            BRIAN,

            [Description("brian geodis")]
            BRIAN_GEODIS,

            [Description("Sofia MX geodis")]
            SOFIA_GEODIS,
        }

        // Enum for the type of the webhook subscription
        public enum WebhookSubscriptionType
        {
            [Description("inbound")]
            TransportInbound,

            [Description("inbound")]
            Inbound,

            [Description("outbound")]
            TransportOutbound,

            [Description("outbound")]
            Outbound,

            [Description("inbound")]
            ReturnReceipt,

            [Description("inbound")]
            WebhookSubscription
        }
    }
}
