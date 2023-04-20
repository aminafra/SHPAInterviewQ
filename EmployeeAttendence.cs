using System.Globalization;
using System.Text;
using InterviewQ.ExtensionMethods;

namespace InterViewQ;

public class EmployeeAttendence
{


    public GetEmployeeWorkFile EmployeeDetails { get; }
    public CheckInAndCheckOut GetInputs { get; }


    #region Cnstructor
    public EmployeeAttendence(GetEmployeeWorkFile employeeDetails)
    {
        EmployeeDetails = employeeDetails;
        GetInputs = new CheckInAndCheckOut(EmployeeDetails);
    }
    #endregion

    //*****************************************************

    public async Task ExportFileAsync(fileTypes fileType)
    {
        var fileName = $"Output.{fileType}";
        var filePath = Path.Combine("C:\\Temp", fileName);
        var separator = ",";
        var output = new StringBuilder();


        string[] headings = { "ردیف", "تاریخ", "روز", "نام فرد", "نوع",
                              "کارکرد", "اولین ورود", "آخرین خروج", "رکوردها" };
        output.AppendLine(string.Join(separator, headings));

        var inputs = GetInputs.InputToExport();
        for (int i = 0; i < inputs.Count; i++)
        {
            var row = inputs[i];

            List<string?> newLine = new List<string?>
                {
                    (i + 1).ToString(),
                    row.Date,
                    row.DayOfWeek,
                    row.Name!,
                    row.DailyType,
                    row.HourlyWork,
                    row.FirstCheckIn.ToString(),
                    row.LastCheckout.ToString(),
                    row.Records!.RecordsToString(),
                };

            output.AppendLine(string.Join(separator, newLine));
        }

        try
        {
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }

            await using (StreamWriter sw = new StreamWriter
                       (new FileStream(filePath, FileMode.CreateNew),encoding: Encoding.UTF8))
            {
                await sw.WriteAsync(output.ToString()); 
            }
            
            Console.WriteLine($"The data has been successfully saved to {filePath}");
        }
        catch (Exception)
        {
            Console.WriteLine($"Data could not be written to the {fileType} file.");
            return;
        }
        
    }


    //*****************************************************
    //*****************************************************
    //private List<TimeOnly>? Records(DateTime date, int id)
    //{
    //    var data = EmployeeDetails.InputEntities!.ToList()
    //        .Where(x => x.Id == id && x.Date == date)
    //        .Select(r => r.Time).ToList();

    //    return data;
    //}

    //*****************************************************
    //public string RecordsToString(DateTime date, int id)
    //{
    //    var data = Records(date, id);
    //    var result = new StringBuilder();
    //    for (int i = 0; i < data.Count; i++)
    //    {
    //        result.Append(data[i].ToString("HH:mm") + " / ");
    //    }
    //    return result.ToString();
    //}

    //*****************************************************

    //public string FirstCheckin(DateTime date, int id)
    //{
    //    var data = Records(date, id);
    //    if (data.Count % 2 == 0)
    //    {
    //        var result = data.First().ToString("HH:mm");
    //        return result;
    //    }
    //    else
    //    {
    //        return string.Empty;
    //    }
    //}

    //*****************************************************

    //public string LastCheckout(DateTime date, int id)
    //{
    //    var data = Records(date, id);
    //    if (data.Count % 2 == 0)
    //    {
    //        var result = data.Last().ToString("HH:mm");
    //        return result;
    //    }
    //    else
    //    {
    //        return string.Empty;
    //    }
    //}

    //*****************************************************

    //public string? HourlyWork(DateTime date, int id)
    //{
    //    TimeSpan? hourlyWork = null;

    //    TimeOnly defaultStart = TimeOnly.Parse("8:30:00");
    //    TimeOnly defaultEnd = TimeOnly.Parse("17:15:00");

    //    List<TimeOnly>? records = Records(date, id);

    //    TimeOnly startTime;
    //    TimeOnly endTime;

    //    if (records.Count % 2 == 0)
    //    {
    //        startTime = records.First();
    //        endTime = records.Last();
    //    }
    //    else
    //    {
    //        return string.Empty;
    //    }


    //    if (startTime <= defaultStart)
    //    {
    //        startTime = defaultStart;
    //    }

    //    if (endTime >= defaultEnd)
    //    {
    //        endTime = defaultEnd;
    //    }

    //    if (records!.Count == 2)
    //    {
    //        hourlyWork = endTime - startTime;
    //        return TimeOnly.Parse(hourlyWork.ToString()!).ToString("HH:mm");
    //    }
    //    else
    //    {
    //        for (int i = 1; i < records.Count; i++)
    //        {
    //            if (i == 1)
    //            {
    //                hourlyWork = records[i] - startTime;
    //                continue;
    //            }
    //            else if (i % 2 != 0)
    //            {
    //                hourlyWork += records[i] - records[i - 1];
    //                continue;
    //            }
    //        }
    //        return TimeOnly.Parse(hourlyWork.ToString()!).ToString("HH:mm");
    //    }
    //}

    //*****************************************************
    //public string DailyType(DateTime date, int id)
    //{
    //    TimeOnly defaultStart = TimeOnly.Parse("8:45:00");
    //    TimeOnly defaultEnd = TimeOnly.Parse("17:00:00");
    //    TimeOnly defaultHourlyWork = TimeOnly.Parse("8:30:00");

    //    List<TimeOnly>? records = Records(date, id);
    //    if (records.Count % 2 != 0)
    //    {
    //        return "خطا";
    //    }
    //    var startTime = records.First();
    //    var endTime = records.Last();
    //    TimeOnly? hourlyWork =TimeOnly.Parse(HourlyWork(date, id)!);

    //    if (startTime > defaultStart || endTime < defaultEnd)
    //    {
    //        return "تاخیر";
    //    }

    //    if (hourlyWork >= defaultHourlyWork)
    //    {
    //        return "عادی";
    //    }

    //    if (records.Count > 2 || hourlyWork < defaultHourlyWork)
    //    {
    //        return "مرخصی ساعتی";
    //    }

    //    if (expr)
    //    {
            
    //    }
    //}
}