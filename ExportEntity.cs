namespace InterViewQ;

public class ExportEntity
{

    public string? Date { get; set; }
    public string? DayOfWeek { get; set; }
    public string? Name { get; set; }
    public string? DailyType { get; set; }
    public string? HourlyWork { get; set; }
    public TimeOnly? FirstCheckIn { get; set; }
    public TimeOnly? LastCheckout { get; set; }
    public List<TimeOnly>? Records { get; set; }

}