using System.Globalization;
using System.Runtime.CompilerServices;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace NEC.Fulf3PL.Application.Common.Converters;

public class DateTimeGeneralFormatConverter : JsonConverter<DateTime>
{
    public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        return DateTime.Parse(reader.GetString());
    }

    public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(GetFormattedDate(value));
    }

    public static string GetFormattedDate(DateTime value)
    {
        return value.ToString("G", CultureInfo.CreateSpecificCulture("en-us"));
    }
}
