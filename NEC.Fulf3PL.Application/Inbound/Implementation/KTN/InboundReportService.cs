using CsvHelper.Configuration;
using CsvHelper;
using System.Globalization;
using NEC.Fulf3PL.Application.Admin.Services;
using AutoMapper;
using NEC.Fulf3PL.Application.Inbound.Implementation.KTN.Model.Report.Inbound;
using NEC.Fulf3PL.Application.Inbound.Implementation.KTN.Model.Report.Converted;
using NEC.Fulf3PL.Application.Inbound.Interface;
using Microsoft.Extensions.Options;
using NEC.Fulf3PL.Application.Inbound.Implementation.KTN.Options;
using NEC.Fulf3PL.Core.Entities.Admin;

namespace NEC.Fulf3PL.Application.Inbound.Implementation.KTN
{
    public class InboundReportService : IInboundReportService
    {
        private readonly IItemMasterQueryService _itemMasterQueryService;
        private readonly IMapper _mapper;
        private readonly InboundReportFileOptions _reportFileOptions;

        public InboundReportService(
            IItemMasterQueryService itemMasterQueryService,
            IMapper mapper,
            IOptions<InboundReportFileOptions> options
            )
        {
            _itemMasterQueryService = itemMasterQueryService;
            _mapper = mapper;
            _reportFileOptions = options.Value;
        }

        public async Task SaveMappedCompareReport(string fileName, Stream inputFileStream, TextWriter outputWriter)
        {
            var extension = Path.GetExtension(fileName);
            if (IsFileMatched(_reportFileOptions.GoodsIssue))
            {
                await GoodsIssueCompare(inputFileStream, outputWriter, _reportFileOptions.GoodsIssue);
            }
            else if (IsFileMatched(_reportFileOptions.InventoryAdjustment))
            {
                await InventoryAdjustmentCompare(inputFileStream, outputWriter, _reportFileOptions.InventoryAdjustment);
            }
            else if (IsFileMatched(_reportFileOptions.POCompare))
            {
                await POReceiptCompare(inputFileStream, outputWriter, _reportFileOptions.POCompare);
            }
            else if (IsFileMatched(_reportFileOptions.ReturnReceipt))
            {
                await ReturnReceipt(inputFileStream, outputWriter, _reportFileOptions.ReturnReceipt);
            }
            else if (IsFileMatched(_reportFileOptions.StockBalance))
            {
                await StockBalance(inputFileStream, outputWriter, _reportFileOptions.StockBalance);
            }

            bool IsFileMatched(InboundReportFileSettings fileSettings)
            {
                return fileSettings.FileExtension.Equals(extension, StringComparison.OrdinalIgnoreCase) &&
                    fileSettings.FileNames.Any(x => fileName.Contains(x, StringComparison.OrdinalIgnoreCase));
            }
        }

        private async Task GoodsIssueCompare(
            Stream inputFileStream, TextWriter outputWriter, InboundReportFileSettings fileSettings)
        {
            var records = await ReadRecordsAsync<GoodsIssueCompareModel>(inputFileStream, fileSettings);
            var itemMasterKeyValue = await GetItemMasterKvp(records);
            var mappedRecords = _mapper.Map<IEnumerable<GoodsIssueConvertedModel>>(records);
            foreach (var mappedRecord in mappedRecords)
            {
                if (!string.IsNullOrEmpty(mappedRecord.Upc)
                    && itemMasterKeyValue.TryGetValue(mappedRecord.Upc, out var item))
                {
                    mappedRecord.Material = item.Data.BaseProductCode;
                    mappedRecord.Grid = item.Data.Size;
                }
            }

            await WriteRecordsAsync(mappedRecords, outputWriter, fileSettings);
        }

        private async Task InventoryAdjustmentCompare(
            Stream inputFileStream, TextWriter outputWriter, InboundReportFileSettings fileSettings)
        {
            var records = await ReadRecordsAsync<InventoryAdjustmentCompareModel>(inputFileStream, fileSettings);
            var itemMasterKeyValue = await GetItemMasterKvp(records);
            var mappedRecords = _mapper.Map<IEnumerable<InventoryAdjustmentConvertedModel>>(records);
            foreach (var mappedRecord in mappedRecords)
            {
                if (!string.IsNullOrEmpty(mappedRecord.Upc)
                    && itemMasterKeyValue.TryGetValue(mappedRecord.Upc, out var item))
                {
                    mappedRecord.Material = item.Data.BaseProductCode;
                    mappedRecord.MaterialDescription = item.Data.ProductDescription;
                    mappedRecord.MaterialGroup = item.Data.AdditionalData?.Find(x => x.Key == "productGroupDesc")?.Value;
                    mappedRecord.Uom = item.Data.AdditionalData?.Find(x => x.Key == "unitOfMeasure")?.Value;
                    mappedRecord.Grid = item.Data.Size;
                }
            }

            await WriteRecordsAsync(mappedRecords, outputWriter, fileSettings);
        }

        private async Task StockBalance(
            Stream inputFileStream, TextWriter outputWriter, InboundReportFileSettings fileSettings)
        {
            var records = await ReadRecordsAsync<StockBalanceModel>(inputFileStream, fileSettings);
            var itemMasterKeyValue = await GetItemMasterKvp(records);
            var mappedRecords = _mapper.Map<IEnumerable<StockBalanceConvertedModel>>(records);
            foreach (var mappedRecord in mappedRecords)
            {
                if (!string.IsNullOrEmpty(mappedRecord.Upc)
                    && itemMasterKeyValue.TryGetValue(mappedRecord.Upc, out var item))
                {
                    mappedRecord.Material = item.Data.BaseProductCode;
                    mappedRecord.MaterialDescription = item.Data.ProductDescription;
                    mappedRecord.MaterialGroup = item.Data.AdditionalData?.Find(x => x.Key == "productGroupDesc")?.Value;
                    mappedRecord.Grid = item.Data.Size;
                }
            }

            await WriteRecordsAsync(mappedRecords, outputWriter, fileSettings);
        }

        private async Task POReceiptCompare(
            Stream inputFileStream, TextWriter outputWriter, InboundReportFileSettings fileSettings)
        {
            var records = await ReadRecordsAsync<POCompareModel>(inputFileStream, fileSettings);
            var itemMasterKeyValue = await GetItemMasterKvp(records);
            var mappedRecords = _mapper.Map<IEnumerable<POCompareConvertedModel>>(records);
            foreach (var mappedRecord in mappedRecords)
            {
                if (!string.IsNullOrEmpty(mappedRecord.Upc)
                    && itemMasterKeyValue.TryGetValue(mappedRecord.Upc, out var item))
                {
                    mappedRecord.Material = item.Data.BaseProductCode;
                    mappedRecord.MaterialDescription = item.Data.ProductDescription;
                    mappedRecord.Grid = item.Data.Size;
                }
            }

            await WriteRecordsAsync(mappedRecords, outputWriter, fileSettings);
        }

        private async Task ReturnReceipt(
            Stream inputFileStream, TextWriter outputWriter, InboundReportFileSettings fileSettings)
        {
            var records = await ReadRecordsAsync<ReturnReceiptCompareModel>(inputFileStream, fileSettings);
            var itemMasterKeyValue = await GetItemMasterKvp(records);
            var mappedRecords = _mapper.Map<IEnumerable<ReturnReceiptConvertedModel>>(records);
            foreach (var mappedRecord in mappedRecords)
            {
                if (!string.IsNullOrEmpty(mappedRecord.Upc)
                    && itemMasterKeyValue.TryGetValue(mappedRecord.Upc, out var item))
                {
                    mappedRecord.Material = item.Data.BaseProductCode;
                    mappedRecord.MaterialDescription = item.Data.ProductDescription;
                    mappedRecord.Grid = item.Data.Size;
                }
            }

            await WriteRecordsAsync(mappedRecords, outputWriter, fileSettings);
        }

        private static async Task<IEnumerable<T>> ReadRecordsAsync<T>(Stream inputFileStream, InboundReportFileSettings fileSettings)
        {
            var records = new List<T>();
            var config = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                Delimiter = fileSettings.FileDelimiter,
                HeaderValidated = null,
                MissingFieldFound = null
            };
            using var reader = new StreamReader(inputFileStream);
            using var csv = new CsvReader(reader, config);
            await foreach (var record in csv.GetRecordsAsync<T>())
            {
                records.Add(record);
            }
            return records;
        }

        private static async Task WriteRecordsAsync<T>(
            IEnumerable<T> records, TextWriter outputWriter, InboundReportFileSettings fileSettings)
        {
            var config = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                Delimiter = fileSettings.FileDelimiter
            };
            using var csvWrite = new CsvWriter(outputWriter, config);
            await csvWrite.WriteRecordsAsync(records);
        }

        private async Task<Dictionary<string, ItemMaster>> GetItemMasterKvp(IEnumerable<IReportProductCode> records)
        {
            var productCodes = records.Select(x => x.Upc).ToHashSet();
            var itemMaster = productCodes.Count > 0 ? await _itemMasterQueryService.GetItemsAsync(x => productCodes.Contains(x.Data.ProductCode))
                : new List<ItemMaster>();
            var itemMasterKeyValue = itemMaster.Where(x => !string.IsNullOrEmpty(x.Data.ProductCode))
                .GroupBy(x => x.Data.ProductCode)
                .ToDictionary(g => g.Key, g => g.First());
            return itemMasterKeyValue;
        }
    }
}
