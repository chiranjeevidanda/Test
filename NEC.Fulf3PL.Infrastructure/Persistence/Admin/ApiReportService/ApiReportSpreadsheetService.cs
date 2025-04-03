using NEC.Fulf3PL.Application.Admin.Services.ExcelGenerateService;
using NEC.Fulf3PL.Core.Common;
using NEC.Fulf3PL.Core.DTO.Admin;
using Npoi.Mapper;

namespace NEC.Fulf3PL.Infrastructure.Persistence.Admin.ApiReportService;
public class ApiReportSpreadsheetService : IApiReportSpreadsheetService
{
    public byte[] GenerateExcel(IEnumerable<InventoryAdjustmentReportDto> data)
    {
        using (var stream = new MemoryStream())
        {
            var mapper = new Mapper();

            mapper.Map<InventoryAdjustmentReportDto>(0, x => x.EventId, ApiReportExcelHeaders.EventId)
                  .Map<InventoryAdjustmentReportDto>(1, x => x.ReceivedTimestamp, ApiReportExcelHeaders.ReceivedTimestamp)
                  .Map<InventoryAdjustmentReportDto>(2, x => x.NecSapPlant, ApiReportExcelHeaders.NecSapPlant)
                  .Map<InventoryAdjustmentReportDto>(3, x => x.AdjustmentNumber, ApiReportExcelHeaders.AdjustmentNumber)
                  .Map<InventoryAdjustmentReportDto>(4, x => x.AdjustmentDate, ApiReportExcelHeaders.AdjustmentDate)
                  .Map<InventoryAdjustmentReportDto>(5, x => x.Quantity, ApiReportExcelHeaders.Quantity)
                  .Map<InventoryAdjustmentReportDto>(6, x => x.UPC, ApiReportExcelHeaders.UPC)
                  .Map<InventoryAdjustmentReportDto>(7, x => x.AdjustmentDirection, ApiReportExcelHeaders.AdjustmentDirection)
                  .Map<InventoryAdjustmentReportDto>(8, x => x.AdjustmentReasonText, ApiReportExcelHeaders.AdjustmentReasonText)
                  .Map<InventoryAdjustmentReportDto>(9, x => x.ErrorReason, ApiReportExcelHeaders.ErrorReason);

            mapper.Save(stream, data, false);

            return stream.ToArray();
        }
    }

    public byte[] GenerateExcel(IEnumerable<PoReceiptReportDto> data, string provider)
    {
        using (var stream = new MemoryStream())
        {
            var mapper = new Mapper();

            mapper.Map<PoReceiptReportDto>(0, x => x.EventId, ApiReportExcelHeaders.EventId)
                  .Map<PoReceiptReportDto>(1, x => x.EventTimestamp, ApiReportExcelHeaders.EventTimestamp)
                  .Map<PoReceiptReportDto>(2, x => x.KTNUniqueIdentifier, provider == Constants.KTN ? ApiReportExcelHeaders.KTNUniqueIdentifier : ApiReportExcelHeaders.GeodisUniqueIdentifier)
                  .Map<PoReceiptReportDto>(3, x => x.ReceiptDate, ApiReportExcelHeaders.ReceiptDate)
                  .Map<PoReceiptReportDto>(4, x => x.NecSapPlant, ApiReportExcelHeaders.NecSapPlant)
                  .Map<PoReceiptReportDto>(5, x => x.NecPurchaseOrderNumber, ApiReportExcelHeaders.NecPurchaseOrderNumber)
                  .Map<PoReceiptReportDto>(6, x => x.LINNO, ApiReportExcelHeaders.LINNO)
                  .Map<PoReceiptReportDto>(7, x => x.PoLineItem, ApiReportExcelHeaders.PoLineItem)
                  .Map<PoReceiptReportDto>(8, x => x.PoScheduleLine, ApiReportExcelHeaders.PoScheduleLine)
                  .Map<PoReceiptReportDto>(9, x => x.QtyReceived, ApiReportExcelHeaders.QtyReceived)
                  .Map<PoReceiptReportDto>(10, x => x.UPC, ApiReportExcelHeaders.UPC)
                  .Map<PoReceiptReportDto>(11, x => x.MID, ApiReportExcelHeaders.MID)
                  .Map<PoReceiptReportDto>(12, x => x.SAPReturnMessage, ApiReportExcelHeaders.ErrorReason);

            mapper.Save(stream, data, false);

            return stream.ToArray();
        }
    }

    public byte[] GenerateExcel(IEnumerable<ReturnReceiptReportDto> data, string provider)
    {
        using (var stream = new MemoryStream())
        {
            var mapper = new Mapper();
            mapper.Map<ReturnReceiptReportDto>(0, x => x.EventId, ApiReportExcelHeaders.EventId)
                  .Map<ReturnReceiptReportDto>(1, x => x.EventTimestamp, ApiReportExcelHeaders.EventTimestamp)
                  .Map<ReturnReceiptReportDto>(2, x => x.KTNUniqueIdentifier, provider == Constants.KTN ? ApiReportExcelHeaders.KTNUniqueIdentifier : ApiReportExcelHeaders.GeodisUniqueIdentifier)
                  .Map<ReturnReceiptReportDto>(3, x => x.ReceiptDate, ApiReportExcelHeaders.ReceiptDate)
                  .Map<ReturnReceiptReportDto>(4, x => x.NecSapPlant, ApiReportExcelHeaders.NecSapPlant)
                  .Map<ReturnReceiptReportDto>(5, x => x.NecPurchaseOrderNumber, ApiReportExcelHeaders.NecPurchaseOrderNumber)
                  .Map<ReturnReceiptReportDto>(6, x => x.DeliveryItem, ApiReportExcelHeaders.LINNO)
                  .Map<ReturnReceiptReportDto>(7, x => x.PoLineItem, ApiReportExcelHeaders.PoLineItem)
                  .Map<ReturnReceiptReportDto>(8, x => x.PoScheduleLine, ApiReportExcelHeaders.PoScheduleLine)
                  .Map<ReturnReceiptReportDto>(9, x => x.QtyReceived, ApiReportExcelHeaders.QtyReceived)
                  .Map<ReturnReceiptReportDto>(10, x => x.UPC, ApiReportExcelHeaders.UPC)
                  .Map<ReturnReceiptReportDto>(11, x => x.MID, ApiReportExcelHeaders.MID)
                  .Map<ReturnReceiptReportDto>(12, x => x.SAPReturnMessage, ApiReportExcelHeaders.ErrorReason);

            mapper.Save(stream, data, false);

            return stream.ToArray();
        }
    }
}
