using System.Text.RegularExpressions;

namespace InterViewQ
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            var filePath = Path.Combine("C:/Temp/Input.txt");

            var workFile = new GetEmployeeWorkFile(filePath);

            var checkIn = new EmployeeCheckinAndCheckout(workFile);

            await checkIn.ExportFileAsync(fileTypes.csv);
        }
        
    }
}