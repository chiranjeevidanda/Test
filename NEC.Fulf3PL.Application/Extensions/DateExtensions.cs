namespace NEC.Fulf3PL.Application.Extensions
{
    public static class DateExtensions
    {
        public const string ISO_TIMESTAMP_FORMAT = "yyyy-MM-ddTHH:mm:ssZ";
        public static string GetFormattedDate(this DateTime value)
        {
            TimeZoneInfo estTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time");
            DateTime currentTimeEst = TimeZoneInfo.ConvertTimeFromUtc(value.ToUniversalTime(), estTimeZone);

            string formattedDateTime = currentTimeEst.ToString(ISO_TIMESTAMP_FORMAT);
            return formattedDateTime;
        }
    }
}