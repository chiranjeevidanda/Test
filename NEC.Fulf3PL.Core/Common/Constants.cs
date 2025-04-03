
namespace NEC.Fulf3PL.Core.Common
{
    public static class Constants
    {
        public const string EMPTY = "";
        public const string ErrorOccured = "Error occured while processing";
        public const string ErrorTimeOut = "Time Out Error";
        public const string ErrorWebhookSend = "Error occured while sending webhook request.";
        public const string ErrorWebhookGone = "Access to the target resource is no longer available at the origin server.";
        public const string ErrorNoBearerTokenInvalidKey = "Token not generated for the end point {0}";

        public const string ErrorNoDataFound = "No data found";

        public const string RequestBlobRetrivalActivity = "RequestBlobRetrivalActivity";
        public const string RequestMaterialExistActivity = "RequestMaterialExistActivity";
        public const string RequestMaterialSentActivity = "RequestMaterialSentActivity";
        public const string RequestDBRetrivalActivity = "RequestDBRetrivalActivity";
        public const string RequestBlobStorageActivity = "RequestBlobStorageActivity";
        public const string RequestDataDeliveryActivity = "RequestDataDeliveryActivity";
        public const string RequestReprocessQueueActivity = "RequestReprocessQueueActivity";
        public const string OutboundReportQueueActivity = "OutboundReportQueueActivity";
        public const string OutboundRequestPostLogActivity = "OutboundRequestPostLogActivity";
        public const string OutboundSubscriberPostLogActivity = "OutboundSubscriberPostLogActivity";

        public const string ProductMasterDeliveyOrchestration = "ProductMasterDeliveyOrchestration";
        public const string PurchaseOrderDeliveyOrchestration = "PurchaseOrderDeliveyOrchestration";
        public const string CreateOrderDeliveyOrchestration = "CreateOrderDeliveyOrchestration";
        public const string ReturnOrderDeliveyOrchestration = "ReturnOrderDeliveyOrchestration";

        public const string GenerateTokenActivity = "GenerateTokenActivity";
        public const string ItemMasterConverterActivity = "ItemMasterConverterActivity";
        public const string ItemMasterPostDataApiActivity = "ItemMasterPostDataApiActivity";
        public const string PurchaseOrderConverterActivity = "PurchaseOrderConverterActivity";
        public const string PurchaseOrderPostDataApiActivity = "PurchaseOrderPostDataApiActivity";
        public const string CreateOrderConverterActivity = "CreateOrderConverterActivity";
        public const string CreateOrderPostDataApiActivity = "CreateOrderPostDataApiActivity";
        public const string ReturnOrderConverterActivity = "ReturnOrderConverterActivity";
        public const string ReturnOrderPostDataApiActivity = "ReturnOrderPostDataApiActivity";
        public const string CreateOrderItemTrackingActivity = "CreateOrderItemTrackingActivity";
        public const string LineMaterialServiceActivity = "LineMaterialServiceActivity";

        public const string ColorPostDataApiActivity = "ColorPostDataApiActivity";
        public const string DesignPostDataApiActivity = "DesignPostDataApiActivity";
        public const string ModelPostDataApiActivity = "ModelPostDataApiActivity";
        public const string SeasonPostDataApiActivity = "SeasonPostDataApiActivity";
        public const string SizesPostDataApiActivity = "SizesPostDataApiActivity";
        public const string GenerateMasterDataTokenActivity = "GenerateTokenActivity";
        public const string MasterDataServiceOrchestration = "MasterDataServiceOrchestration";
        public const string ItemPostDbActivity = "ItemPostDbActivity";
        public const string MasterDataPostLogActivity = "MasterDataPostLogActivity";


        public const string InboundRequestPostLogActivity = "InboundRequestPostLogActivity";
        public const string GoodsReceivedSubscriberOrchestration = "GoodsReceivedSubscriberOrchestration";
        public const string GoodsIssuedSubscriberOrchestration = "GoodsIssuedSubscriberOrchestration";
        public const string ReturnReceiptSubscriberOrchestration = "ReturnReceiptSubscriberOrchestration";
        public const string InventorySubscriberOrchestration = "InventorySubscriberOrchestration";
        public const string InboundSubscriberPostLogActivity = "InboundSubscriberPostLogActivity";
        public const string TransportInboundLinksActivity = "TransportInboundLinksActivity";
        public const string TransportInboundStatusActivity = "TransportInboundStatusActivity";
        public const string TransportInboundCollectionActivity = "TransportInboundCollectionActivity";
        public const string TransportInboundExpandLinesActivity = "TransportInboundExpandLinesActivity";
        public const string SendNotificationToQueueActivity = "SendNotificationToQueueActivity";
        public const string InventoryPostLogActivity = "InventoryPostLogActivity";
        public const string OutboundStatusActivity = "OutboundStatusActivity";
        public const string OutboundExpandLinesActivity = "OutboundExpandLinesActivity";
        public const string OutboundParcelsActivity = "OutboundParcelsActivity";
        public const string OutboundPalletsActivity = "OutboundPalletsActivity";
        public const string OutboundStockChangesActivity = "OutboundStockChangesActivity";
        public const string OutboundLineParcelsActivity = "OutboundLineParcelsActivity";
        public const string TransportOutboundStatusActivity = "TransportOutboundStatusActivity";
        public const string TransportOutboundLinksActivity = "TransportOutboundLinksActivity";
        public const string TransportInboundStockChangeActivity = "TransportInboundStockChangeActivity";
        public const string ReturnInboundStockChangeActivity = "ReturnInboundStockChangeActivity";
        public const string OutboundCollectionActivity = "OutboundCollectionActivity";
        public const string TransportOutboundCollectionActivity = "TransportOutboundCollectionActivity";
        public const string ReturnExpandLinesActivity = "ReturnExpandLinesActivity";
        //SOFIA
        public const string SofiaOutboundRequestPostLogActivity = "SofiaOutboundRequestPostLogActivity";
        public const string SofiaOrderBlobStorageActivity = "SofiaOrderBlobStorageActivity";
        public const string SofiaReturnBlobStorageActivity = "SofiaReturnBlobStorageActivity";
        public const string SofiaGoodsIssuedSubscriberOrchestration = "SofiaGoodsIssuedSubscriberOrchestration";
        public const string SofiaReturnReceiptSubscriberOrchestration = "SofiaReturnReceiptSubscriberOrchestration";
        public const string UKShipmentPostDataActivity = "UKShipmentPostDataActivity";
        public const string EUShipmentPostDataActivity = "EUShipmentPostDataActivity";
        public const string EUReturnPostDataActivity = "EUReturnPostDataActivity";
        public const string UKReturnPostDataActivity = "UKReturnPostDataActivity";
        public const string EUOrderAcknowledgedActivity = "EUOrderAcknowledgedActivity";
        public const string EUOrderAllocatedActivity = "EUOrderAllocatedActivity";
        public const string EUOrderShippedActivity = "EUOrderShippedActivity";
        public const string EUOrderCancelledActivity = "EUOrderCancelledActivity";
        public const string EUOrderReturnActivity = "EUOrderReturnActivity";
        public const string UKOrderAcknowledgedActivity = "UKOrderAcknowledgedActivity";
        public const string UKOrderAllocatedActivity = "UKOrderAllocatedActivity";
        public const string UKOrderShippedActivity = "UKOrderShippedActivity";
        public const string UKOrderCancelledActivity = "UKOrderCancelledActivity";
        public const string UKOrderReturnActivity = "UKOrderReturnActivity";
        public const string MXOrderAcknowledgedActivity = "MXOrderAcknowledgedActivity";
        public const string MXOrderAllocatedActivity = "MXOrderAllocatedActivity";
        public const string MXOrderShippedActivity = "MXOrderShippedActivity";
        public const string MXOrderCancelledActivity = "MXOrderCancelledActivity";
        public const string MXOrderReturnActivity = "MXOrderReturnActivity";

        public const string BrianInventorySubscriberOrchestration = "BrianInventorySubscriberOrchestration";
        public const string BRIANInventoryPostDataActivity = "BRIANInventoryPostDataActivity";

        public const string KtnGoodsReceivedConverterActivity = "KtnGoodsReceivedConverterActivity";
        public const string KtnGoodsIssuedConverterActivity = "KtnGoodsIssuedConverterActivity";
        public const string KtnReturnReceiptConverterActivity = "KtnReturnReceiptConverterActivity";
        public const string KtnInventoryConverterActivity = "KtnInventoryConverterActivity";
        public const string KtnGoodsReceivedSapBapiDataActivity = "KtnGoodsReceivedSapBapiDataActivity";
        public const string KtnInventorySapBapiActivity = "KtnInventorySapBapiActivity";
        public const string KtnGoodsIssueSapBapiDataActivity = "KtnGoodsIssueSapBapiDataActivity";
        public const string ReturnReceivedSapBapiDataActivity = "ReturnReceivedSapBapiDataActivity";
        public const string ReturnReceivedSapPostDataActivity = "ReturnReceivedSapPostDataActivity";
        public const string GoodsReceivedSapPostDataActivity = "GoodsReceivedSapPostDataActivity";
        public const string InventorySapPostActivity = "InventorySapPostActivity";
        public const string GoodsIssueSapPostDataActivity = "GoodsIssueSapPostDataActivity";
        

        public const int KtnKeyPairCount = 1;

        public const string OutboundRequest = "OutboundRequest";
        public const string InboundRequest = "InboundRequest";
        public const string NEWERAB2B = "NEWERAB2B";
        public const string NEWERAB2C = "NEWERAB2C";
        public const string NEWERAPTO = "NEWERAPTO";

        public const string InboundWebhookResource = "inbound/v2.0/companies({0})/inbounds";
        public const string TransportsInboundWebhookResource = "inbound/v2.0/companies({0})/transports";
        public const string TransportsOutboundWebhookResource = "outbound/v2.0/companies({0})/transports";
        public const string ReturnsWebhookResource = "inbound/v2.0/companies({0})/returns";
        public const string OutboundWebhookResource = "outbound/v2.0/companies({0})/outbounds";

        public const string KTN = "KTN";
        public const string GEODIS = "GEODIS";
    }
}
