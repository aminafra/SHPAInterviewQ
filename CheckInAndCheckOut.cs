namespace InterViewQ;

public class CheckInAndCheckOut : object
{
    private GetEmployeeWorkFile EmployeesWorks { get; }
    private List<ExportEntity> ExportEntities { get; }


    public CheckInAndCheckOut(GetEmployeeWorkFile employeesWorks) : base()
    {
        EmployeesWorks = employeesWorks;
        ExportEntities = new List<ExportEntity>();
    }

    public List<ExportEntity> InputToExport()
    {
        for (var i = 0; i < EmployeesWorks.EmployeeByDates.Count; i++)
        {
            var employee = EmployeesWorks.EmployeeByDates[i];

            ExportEntities.Add(new ExportEntity
            {
                Date = 
                    employee.Date.ToShamsi(),
                DayOfWeek =
                    employee.Date.PersianDay(),
                Name =
                    employee.Name,
                DailyType =
                    DailyType(employee.Date, employee.Id),
                HourlyWork = 
                    HourlyWork(employee.Date, employee.Id),
                FirstCheckIn =
                    FirstCheckIn(employee.Date, employee.Id),
                LastCheckout =
                    LastCheckout(employee.Date, employee.Id),
                Records =
                    Records(employee.Date, employee.Id),
            });

        }

        return ExportEntities.ToList();
    }


    private TimeOnly? FirstCheckIn(DateTime date, int id)
    {
        var records = Records(date, id);
        if (records.Count != 0 && records.Count % 2 == 0)
        {
            var result = records.FirstOrDefault();
            return result;
        }
        else
        {
            return null;
        }
    }


    private TimeOnly? LastCheckout(DateTime date, int id)
    {
        var records = Records(date, id);
        if (records.Count != 0 && records.Count % 2 == 0)
        {
            var result = records.Last();
            return result;
        }
        else
        {
            return null;
        }
    }


    private List<TimeOnly> Records(DateTime date, int id)
    {
        var data = EmployeesWorks.InputEntities!.ToList()
            .Where(x => x.Id == id && x.Date == date)
            .Select(r => r.Time).ToList();

        return data;

    }


    private string? HourlyWork(DateTime date, int id)
    {
        TimeSpan? hourlyWork = null;

        TimeOnly defaultStart = TimeOnly.Parse("8:30:00");
        TimeOnly defaultEnd = TimeOnly.Parse("17:15:00");

        List<TimeOnly> records = Records(date, id);
        var startTime = FirstCheckIn(date, id);
        var endTime = LastCheckout(date, id);


        if (startTime == null || endTime == null)
        {
            return null;
        }

        if (startTime <= defaultStart)
        {
            startTime = defaultStart;
        }

        if (endTime >= defaultEnd)
        {
            endTime = defaultEnd;
        }

        if (records!.Count == 2)
        {
            hourlyWork = endTime - startTime;
            return TimeOnly.Parse(hourlyWork.ToString()!).ToString("HH:mm");
        }
        else
        {
            for (int i = 1; i < records.Count; i++)
            {
                if (i == 1)
                {
                    hourlyWork = records[i] - startTime;
                    continue;
                }
                else if (i % 2 != 0)
                {
                    hourlyWork += records[i] - records[i - 1];
                    continue;
                }
            }
            return TimeOnly.Parse(hourlyWork.ToString()!).ToString("HH:mm");
        }
    }

    private string DailyType(DateTime date, int id)
    {
        TimeOnly defaultStart = TimeOnly.Parse("8:45:00");
        TimeOnly defaultEnd = TimeOnly.Parse("17:00:00");
        TimeOnly defaultHourlyWork = TimeOnly.Parse("8:30:00");

        var records = Records(date, id);
        
        if (records.Count == 0)
        {
            return "مرخصی روزانه";
        }

        if (records.Count % 2 != 0)
        {
            return "خطا";
        }
        var startTime = FirstCheckIn(date, id);
        var endTime = LastCheckout(date, id);
        TimeOnly? hourlyWork = TimeOnly.Parse(HourlyWork(date, id)!);

        if (startTime > defaultStart || endTime < defaultEnd)
        {
            return "تاخیر";
        }

        if (hourlyWork >= defaultHourlyWork)
        {
            return "عادی";
        }

        if (records.Count > 2 || hourlyWork < defaultHourlyWork)
        {
            return "مرخصی ساعتی";
        }

        

        return "خطای محاسباتی";
    }
}
