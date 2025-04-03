using NEC.Fulf3PL.Core.DTO.Admin;

namespace NEC.Fulf3PL.Application.Admin.Services.ExcelGenerateService;
public interface IApiReportSpreadsheetService
{
    public byte[] GenerateExcel(IEnumerable<InventoryAdjustmentReportDto> data);
    public byte[] GenerateExcel(IEnumerable<PoReceiptReportDto> data, string provider);
    public byte[] GenerateExcel(IEnumerable<ReturnReceiptReportDto> data, string provider);
}
