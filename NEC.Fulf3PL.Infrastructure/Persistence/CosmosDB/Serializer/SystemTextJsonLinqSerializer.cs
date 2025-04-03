using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;
using Azure.Core.Serialization;
using Microsoft.Azure.Cosmos;

namespace NEC.Fulf3PL.Infrastructure.Persistence.CosmosDB.Serializer;

/// <summary>
///     This can be removed once the Azure SDK addd default support for System.Text.Json.
///     https://github.com/Azure/azure-cosmos-dotnet-v3/issues/4330
///     https://github.com/Azure/azure-cosmos-dotnet-v3/pull/4332
///     Reference: https://github.com/Azure/azure-cosmos-dotnet-v3/blob/dee9abaedf66b5663fdf33ea1f943cd9303e152a/Microsoft.Azure.Cosmos/tests/Microsoft.Azure.Cosmos.EmulatorTests/Linq/LinqTestsCommon.cs#L817
/// </summary>
/// <param name="jsonSerializerOptions"></param>
public class SystemTextJsonLinqSerializer : CosmosLinqSerializer
{
    private readonly JsonObjectSerializer _systemTextJsonSerializer;
    private readonly JsonSerializerOptions _jsonSerializerOptions;

    public SystemTextJsonLinqSerializer(JsonSerializerOptions jsonSerializerOptions)
    {
        _jsonSerializerOptions = jsonSerializerOptions;
        _systemTextJsonSerializer = new JsonObjectSerializer(jsonSerializerOptions);
    }

    public override T FromStream<T>(Stream stream)
    {
        ArgumentNullException.ThrowIfNull(stream);

        using (stream)
        {
            if (stream.CanSeek && stream.Length == 0)
            {
                return default!;
            }

            if (typeof(Stream).IsAssignableFrom(typeof(T)))
            {
                return (T)(object)stream;
            }

            return (T)_systemTextJsonSerializer.Deserialize(stream, typeof(T), default)!;
        }
    }

    public override Stream ToStream<T>(T input)
    {
        var streamPayload = new MemoryStream();
        _systemTextJsonSerializer.Serialize(streamPayload, input, input.GetType(), default);
        streamPayload.Position = 0;
        return streamPayload;
    }

    public override string SerializeMemberName(MemberInfo memberInfo)
    {
        var jsonExtensionDataAttribute = memberInfo.GetCustomAttribute<JsonExtensionDataAttribute>(true);
        if (jsonExtensionDataAttribute != null)
        {
            return null!;
        }

        var jsonPropertyNameAttribute = memberInfo.GetCustomAttribute<JsonPropertyNameAttribute>(true);
        if (!string.IsNullOrEmpty(jsonPropertyNameAttribute?.Name))
        {
            return jsonPropertyNameAttribute.Name;
        }

        if (_jsonSerializerOptions.PropertyNamingPolicy != null)
        {
            return _jsonSerializerOptions.PropertyNamingPolicy.ConvertName(memberInfo.Name);
        }

        // Do any additional handling of JsonSerializerOptions here.

        return memberInfo.Name;
    }
}
