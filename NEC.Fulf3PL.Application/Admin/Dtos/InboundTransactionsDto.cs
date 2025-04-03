using NEC.Fulf3PL.Application.Common.Converters;
using Newtonsoft.Json;
using System;
using System.Text.Json.Serialization;

namespace NEC.Fulf3PL.Application.Admin.Dtos;

public class InboundTransactionsDto
{
    [JsonPropertyName("id")]
    public string Id { get; set; }

    [JsonPropertyName("createdDate")]
    public string? CreatedDate { get; set; }

    [JsonPropertyName("orderNumber")]
    public string? OrderNumber { get; set; } = string.Empty;

    [JsonPropertyName("orderType")]
    public string? OrderType { get; set; } = string.Empty;

    [JsonPropertyName("webhookPayload")]
    public string? WebhookPayload { get; set; } = string.Empty;

    [JsonPropertyName("sapBapiInput")]
    public string? SapBapiInput { get; set; } = string.Empty;

    [JsonPropertyName("sapBapiResponse")]
    public string? SapBapiResponse { get; set; } = string.Empty;

    [JsonPropertyName("sapConverterPayload")]
    public string? SapConverterPayload { get; set; } = string.Empty;

    [JsonPropertyName("errorMessage")]
    public string? ErrorMessage { get; set; } = string.Empty;

    [JsonPropertyName("transactionStatus")]
    public string? TransactionStatus { get; set; } = string.Empty;
    [JsonPropertyName("customer")]
    public string? Customer { get; set; } = string.Empty;
    [JsonProperty("provider")]
    public string? Provider { get; set; }
}
