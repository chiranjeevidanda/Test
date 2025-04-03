namespace NEC.Fulf3PL.AdminWebApp.Extensions;

internal static class DateTimeExtensions
{
    public static DateTime AdjustTimeIfZero(this DateTime dateTime)
    {
        return dateTime.TimeOfDay == TimeSpan.Zero
            ? dateTime.Date.AddHours(23).AddMinutes(59).AddSeconds(59)
            : dateTime;
    }
}
