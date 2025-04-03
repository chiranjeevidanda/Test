using NEC.Fulf3PL.Application.Admin.Dtos;
using NEC.Fulf3PL.Application.Common;
using NEC.Fulf3PL.Core.Entities.Admin;
using NEC.Fulf3PL.Core.Common;
using Npoi.Mapper;
using static NEC.Fulf3PL.Core.Common.Enums;
using NEC.Fulf3PL.Application.Outbound.Implementation.KTN.DTO;
using NEC.Fulf3PL.Core.Common.Admin;
using Microsoft.Azure.Cosmos.Linq;

namespace NEC.Fulf3PL.Infrastructure.Services;

public class KtnSpreadSheetServices : ISpreadSheetService
{
    public byte[] Export(string? requestType, IEnumerable<OutboundResponseDto> transactions)
    {
        using var stream = new MemoryStream();


        var mapper = requestType switch
        {
            OutboundTransactionRequestType.ProductMaster => new Mapper()
                .Map<OutboundResponseDto>(0, x => x.ModifiedDate, TransactionExcelHeaders.Timestamp)
                .Map<OutboundResponseDto>(1, x => x.EventId, TransactionExcelHeaders.EventId)
                .Map<OutboundResponseDto>(2, x => x.DocumentId, TransactionExcelHeaders.ProductCode)
                .Map<OutboundResponseDto>(3, x => x.Status, TransactionExcelHeaders.KtnResponse)
                .Map<OutboundResponseDto>(4, x => x.DocumentType, TransactionExcelHeaders.OrderType)
                .Map<OutboundResponseDto>(5, x => x.ErrorMessage, TransactionExcelHeaders.ErrorMessage)
                .Map<OutboundResponseDto>(6, x => x.SystemId, TransactionExcelHeaders.SystemId)
                .Map<OutboundResponseDto>(7, x => x.Customer, TransactionExcelHeaders.Customer)
                .Ignore<OutboundResponseDto>(x => x.Provider)
                .Ignore<OutboundResponseDto>(x => x.ProcessId)
                .Ignore<OutboundResponseDto>(x => x.RequestInput)
                .Ignore<OutboundResponseDto>(x => x.RequestData)
                .Ignore<OutboundResponseDto>(x => x.ContainerNumber)
                .Ignore<OutboundResponseDto>(x => x.BookingReference)
                 .Ignore<OutboundResponseDto>(x => x.OutboundRequestId)
                .Ignore<OutboundResponseDto>(x => x.Response),

            OutboundTransactionRequestType.PurchaseOrder => new Mapper()
                 .Map<OutboundResponseDto>(0, x => x.ModifiedDate, TransactionExcelHeaders.Timestamp)
                 .Map<OutboundResponseDto>(1, x => x.EventId, TransactionExcelHeaders.EventId)
                 .Map<OutboundResponseDto>(2, x => x.DocumentId, TransactionExcelHeaders.PoNumber)
                 .Map<OutboundResponseDto>(3, x => x.ContainerNumber, TransactionExcelHeaders.ContainerNumber)
                  .Map<OutboundResponseDto>(4, x => x.Status, TransactionExcelHeaders.KtnResponse)
                 .Map<OutboundResponseDto>(5, x => x.DocumentType, TransactionExcelHeaders.OrderType)
                 .Map<OutboundResponseDto>(6, x => x.ErrorMessage, TransactionExcelHeaders.ErrorMessage)
                   .Map<OutboundResponseDto>(7, x => x.SystemId, TransactionExcelHeaders.SystemId)
                .Map<OutboundResponseDto>(8, x => x.Customer, TransactionExcelHeaders.Customer)
                 .Ignore<OutboundResponseDto>(x => x.Provider)
                 .Ignore<OutboundResponseDto>(x => x.ProcessId)
                 .Ignore<OutboundResponseDto>(x => x.RequestInput)
                 .Ignore<OutboundResponseDto>(x => x.RequestData)
                 .Ignore<OutboundResponseDto>(x => x.BookingReference)
                  .Ignore<OutboundResponseDto>(x => x.OutboundRequestId)
                 .Ignore<OutboundResponseDto>(x => x.Response),

            OutboundTransactionRequestType.ReturnOrder => new Mapper()
                 .Map<OutboundResponseDto>(0, x => x.ModifiedDate, TransactionExcelHeaders.Timestamp)
                 .Map<OutboundResponseDto>(1, x => x.EventId, TransactionExcelHeaders.EventId)
                 .Map<OutboundResponseDto>(2, x => x.DocumentId, TransactionExcelHeaders.Delivery)
               .Map<OutboundResponseDto>(3, x => x.Status, TransactionExcelHeaders.KtnResponse)
                 .Map<OutboundResponseDto>(4, x => x.DocumentType, TransactionExcelHeaders.OrderType)
                 .Map<OutboundResponseDto>(5, x => x.ErrorMessage, TransactionExcelHeaders.ErrorMessage)
                    .Map<OutboundResponseDto>(6, x => x.SystemId, TransactionExcelHeaders.SystemId)
                .Map<OutboundResponseDto>(7, x => x.Customer, TransactionExcelHeaders.Customer)
                 .Ignore<OutboundResponseDto>(x => x.Provider)
                 .Ignore<OutboundResponseDto>(x => x.ProcessId)
                 .Ignore<OutboundResponseDto>(x => x.RequestInput)
                 .Ignore<OutboundResponseDto>(x => x.RequestData)
                 .Ignore<OutboundResponseDto>(x => x.ContainerNumber)
                 .Ignore<OutboundResponseDto>(x => x.BookingReference)
                  .Ignore<OutboundResponseDto>(x => x.OutboundRequestId)
                 .Ignore<OutboundResponseDto>(x => x.Response),

            OutboundTransactionRequestType.CreateOrder => new Mapper()
                 .Map<OutboundResponseDto>(0, x => x.ModifiedDate, TransactionExcelHeaders.Timestamp)
                 .Map<OutboundResponseDto>(1, x => x.EventId, TransactionExcelHeaders.EventId)
                 .Map<OutboundResponseDto>(2, x => x.DocumentId, TransactionExcelHeaders.Delivery)
                 .Map<OutboundResponseDto>(3, x => x.Status, TransactionExcelHeaders.KtnResponse)
                 .Map<OutboundResponseDto>(4, x => x.DocumentType, TransactionExcelHeaders.OrderType)
                 .Map<OutboundResponseDto>(5, x => x.ErrorMessage, TransactionExcelHeaders.ErrorMessage)
                  .Map<OutboundResponseDto>(6, x => x.SystemId, TransactionExcelHeaders.SystemId)
                .Map<OutboundResponseDto>(7, x => x.Customer, TransactionExcelHeaders.Customer)
                 .Ignore<OutboundResponseDto>(x => x.Provider)
                 .Ignore<OutboundResponseDto>(x => x.ProcessId)
                 .Ignore<OutboundResponseDto>(x => x.RequestInput)
                 .Ignore<OutboundResponseDto>(x => x.RequestData)
                 .Ignore<OutboundResponseDto>(x => x.ContainerNumber)
                 .Ignore<OutboundResponseDto>(x => x.BookingReference)
                  .Ignore<OutboundResponseDto>(x => x.OutboundRequestId)
                 .Ignore<OutboundResponseDto>(x => x.Response),

            _ => throw new InvalidOperationException("Invalid Order Type")
        };

        mapper.Save(stream, transactions, false);

        return stream.ToArray();
    }

    public byte[] Export(string? requestType, IEnumerable<InboundTransactionsDto> transactions)
    {
        using var stream = new MemoryStream();

        var mapper = new Mapper();

        mapper.Map<InboundTransactionsDto>(0, x => x.OrderType, TransactionExcelHeaders.OrderType)
              .Map<InboundTransactionsDto>(1, x => x.Id, TransactionExcelHeaders.EventId)
              .Map<InboundTransactionsDto>(2, x => x.CreatedDate, TransactionExcelHeaders.ReceiveTimestamp)
              .Map<InboundTransactionsDto>(3, x => x.OrderNumber, TransactionExcelHeaders.OrderNumber)
              .Map<InboundTransactionsDto>(4, x => x.TransactionStatus, TransactionExcelHeaders.SAPResponse)
              .Ignore<InboundTransactionsDto>(x => x.WebhookPayload)
              .Ignore<InboundTransactionsDto>(x => x.SapConverterPayload)
              .Ignore<InboundTransactionsDto>(x => x.SapBapiInput)
              .Ignore<InboundTransactionsDto>(x => x.SapBapiResponse)
              .Ignore<InboundTransactionsDto>(x => x.Customer)
            .Ignore<InboundTransactionsDto>(x => x.Provider);

        mapper.Save(stream, transactions, false);
        return stream.ToArray();
    }

    public byte[] Export(string? requestType, IEnumerable<OutboundProductDetailsDto> transactions)
    {
        using var stream = new MemoryStream();

        var mapper = new Mapper();

        mapper
.Map<OutboundProductDetailsDto>(0, x => x.ModifiedDate, TransactionExcelHeaders.Timestamp)
.Map<OutboundProductDetailsDto>(1, x => x.Status, TransactionExcelHeaders.KtnResponse)
.Map<OutboundProductDetailsDto>(2, x => x.ProductDetails.Customer, TransactionExcelHeaders.Customer)
.Map<OutboundProductDetailsDto>(3, x => x.ProductDetails.Code, TransactionExcelHeaders.SKUId)
.Map<OutboundProductDetailsDto>(4, x => x.ProductDetails.Description, TransactionExcelHeaders.Description)
.Map<OutboundProductDetailsDto>(5, x => x.ProductDetails.StockKeepingUnit, TransactionExcelHeaders.StockKeepingUnit)
.Map<OutboundProductDetailsDto>(6, x => x.ProductDetails.ItemGroup, TransactionExcelHeaders.ItemGroup)
.Map<OutboundProductDetailsDto>(7, x => x.ProductDetails.CodeOfGoods, TransactionExcelHeaders.CodeOfGoods)
.Map<OutboundProductDetailsDto>(8, x => x.ProductDetails.Reference1, TransactionExcelHeaders.Plant)
.Map<OutboundProductDetailsDto>(9, x => x.ProductDetails.Reference3, TransactionExcelHeaders.SeasonDescription)
.Map<OutboundProductDetailsDto>(10, x => x.ProductDetails.Model, TransactionExcelHeaders.Model)
.Map<OutboundProductDetailsDto>(11, x => x.ProductDetails.Design, TransactionExcelHeaders.Design)
.Map<OutboundProductDetailsDto>(12, x => x.ProductDetails.Size, TransactionExcelHeaders.Size)
.Map<OutboundProductDetailsDto>(13, x => x.ProductDetails.Color, TransactionExcelHeaders.Color)
.Map<OutboundProductDetailsDto>(14, x => x.ProductDetails.Season, TransactionExcelHeaders.Season)
.Ignore<OutboundProductDetailsDto>(x => x.ProductDetails.Reference2)
.Ignore<OutboundProductDetailsDto>(x => x.ProductDetails.Reference4)
.Ignore<OutboundProductDetailsDto>(x => x.ProductDetails.AdrGroup)
.Ignore<OutboundProductDetailsDto>(x => x.ProductDetails.BarCodes)
.Ignore<OutboundProductDetailsDto>(x => x.ProductDetails.MandatoryCheckItemOrSSCC)
.Ignore<OutboundProductDetailsDto>(x => x.ProductDetails.Configurations)
.Ignore<OutboundProductDetailsDto>(x => x.ProductDetails.ConfigDetails)
.Ignore<OutboundProductDetailsDto>(x => x.ProductDetails.MandatoryCountryOfOrigin)
.Ignore<OutboundProductDetailsDto>(x => x.ProductDetails.MandatoryLotNo)
.Ignore<OutboundProductDetailsDto>(x => x.ProductDetails.IsStockable)
.Ignore<OutboundProductDetailsDto>(x => x.Provider);

        mapper.Save(stream, transactions, false);

        return stream.ToArray();
    }
    //public byte[] Export(string? requestType, IEnumerable<OutboundGeodisProductDetailsDto> transactions)
    //{
    //    throw new NotImplementedException();
    //}

    }
