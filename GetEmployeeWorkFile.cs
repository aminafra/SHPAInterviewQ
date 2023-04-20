using System.Text.RegularExpressions;
using Model;

namespace InterViewQ;

public class GetEmployeeWorkFile : object
{
    public GetEmployeeWorkFile(string filePath) : base()
    {
        DateEntities = new List<DateTime>();
        InputEntities = new List<Input>();
        EmployeeEntities = new List<Employee>();
        EmployeeByDates = new List<EmployeeByDate>();

        foreach (string line in File.ReadLines(filePath))
        {
            var text = FixText(line)?.Split(" ");
            if (text != null)
                InputEntities.Add(new Input
                {
                    Id = int.Parse(text[0]),
                    Name = text[1],
                    Date = DateTime.Parse(text[2]),
                    Time = TimeOnly.Parse(text[3]),
                });
        }

        for (int i = 0; i < InputEntities.Count; i++)
        {
            if (i == 0)
            {
                EmployeeEntities.Add(new Employee
                {
                    Id = InputEntities[i].Id,
                    Name = InputEntities[i].Name,
                });
                DateEntities.Add(InputEntities[i].Date);
                continue;
            }
            if (EmployeeEntities.ToList().Exists(x => x.Id == InputEntities[i].Id) == false)
            {
                EmployeeEntities.Add(new Employee
                {
                    Id = InputEntities[i].Id,
                    Name = InputEntities[i].Name,
                });
            }
            if (DateEntities.ToList().Exists(x => x == InputEntities[i].Date) == false)
            {
                DateEntities.Add(InputEntities[i].Date);
            }
        }

        for (int i = 0; i < DateEntities.Count; i++)
        {
            for (int j = 0; j < EmployeeEntities.Count; j++)
            {
                EmployeeByDates.Add(new EmployeeByDate
                {
                    Date = DateEntities[i],
                    Id = EmployeeEntities[j].Id,
                    Name = EmployeeEntities[j].Name,
                });
            }
        }
    }
    //*****************************************************
    public IList<Input> InputEntities { get; }

    public IList<Employee> EmployeeEntities { get; }

    public IList<DateTime> DateEntities { get; }
    
    public IList<EmployeeByDate> EmployeeByDates { get; }
    //*****************************************************
    //*****************************************************
    




    //*****************************************************
    #region FixText
    private static string? FixText(string? text)
    {
        if (string.IsNullOrWhiteSpace(value: text))
        {
            return null;
        }

        text =
            text.Trim();

        text = Regex.Replace(text, @"\t|\n|\r", " ");

        while (text.Contains(value: "  "))
        {
            text = text.Replace
                (oldValue: "  ", newValue: " ");
        }


        return text;
    }
    #endregion
}