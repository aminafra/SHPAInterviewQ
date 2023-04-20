using System.Globalization;
using System.Text;

namespace InterviewQ.ExtensionMethods;

public static class ExtensionMethods
{
    public static string? ToShamsi(this DateTime date)
    {
        PersianCalendar pc = new PersianCalendar();
        StringBuilder sb = new StringBuilder();
        sb.Append(pc.GetYear(date).ToString("0000"));
        sb.Append("/");
        sb.Append(pc.GetMonth(date).ToString("00"));
        sb.Append("/");
        sb.Append(pc.GetDayOfMonth(date).ToString("00"));

        return sb.ToString();
    }

    public static string PersianDay(this DateTime date)
    {
        PersianCalendar pc = new PersianCalendar();
        var dayOfWeek = pc.GetDayOfWeek(date);
        switch (dayOfWeek)
        {
            case DayOfWeek.Sunday:
                return "یکشنبه";
            case DayOfWeek.Monday:
                return "دوشنبه";
            case DayOfWeek.Tuesday:
                return "سه شنبه";
            case DayOfWeek.Wednesday:
                return "چهارشنبه";
            case DayOfWeek.Thursday:
                return "پنجشنبه";
            case DayOfWeek.Friday:
                return "جمعه";
            case DayOfWeek.Saturday:
                return "شنبه";
            default:
                return string.Empty;
        }
    }

    public static string RecordsToString(this List<TimeOnly> records)
    {
        var result = new StringBuilder();
        if (records.Count != 0)
        {
            foreach (var record in records)
            {
                result.Append(record.ToString("HH:mm") + " / ");
            }

            return result.ToString();
        }
        else
        {
            return string.Empty;
        }
    }
}
