namespace NEC.Fulf3PL.Core.Common
{
    public static class Helper
    {
        public const string ISO_TIMESTAMP_FORMAT = "yyyy-MM-ddTHH:mm:ssZ";
        public const string UTC_TIMESTAMP_FORMAT = "yyyy-MM-ddTHH:mm:ssZ";

        public static string GetFormattedDatetime()
        {
            TimeZoneInfo estTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time");
            DateTime currentTimeEst = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, estTimeZone);
            string formattedDateTime = currentTimeEst.ToString(ISO_TIMESTAMP_FORMAT);

            return formattedDateTime;
        }

        public static DateTime GetCurrentDatetime()
        {
            TimeZoneInfo estTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time");
            DateTime currentTimeEst = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, estTimeZone);

            return currentTimeEst;
        }

        public static string GetUtcDateTime()
        {
            DateTime utcNow = DateTime.UtcNow;
            string formattedTime = utcNow.ToString("yyyy-MM-ddTHH:mm:ss.fffZ");

            return formattedTime;
        }
    }
}
