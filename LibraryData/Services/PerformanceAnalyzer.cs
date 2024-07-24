#nullable disable
using LibraryData.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace LibraryData.Services
{
    public class PerformanceAnalyzer
    {
        public static async Task GetStoredProcedurePerformance()
        {
            List<string> searchTerms = new List<string>
            {
                "Santhosh",
                "Programming in C",
                "Santhosh, Programming in C",
                "Santhosh, Kausthuban",
                "Santhosh, Programming in C, Computer Technology",
                "Kausthuban, Life lesson",
                "Kausthuban, Azure Fundamentals, CodeGamers, Cloud Computing"
            };

            List<double> executionTimeLoop = new List<double>();
            List<double> executionTimeJoin = new List<double>();

            using (var context = new LibrarydbContext())
            {
                foreach (string item in searchTerms)
                {
                    var result = await ExecuteStoredProcedure(context, item);
                    executionTimeLoop.Add(result.Item1);
                    executionTimeJoin.Add(result.Item2);
                }
            }
            Console.WriteLine($"Average Execution Time for Loop: {executionTimeLoop.Average()} ms");
            Console.WriteLine($"Average Execution Time for Join: {executionTimeJoin.Average()} ms");
        }

        public static async Task<(double, double)> ExecuteStoredProcedure(LibrarydbContext context, string searchTerm)
        {
            var cmd = context.Database.GetDbConnection().CreateCommand();
            cmd.CommandText = "[dbo].[Measure_Performance_Of_Procedures]";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@searchTerms", searchTerm));

            await context.Database.OpenConnectionAsync();
            double loopTime = 0;
            double joinsTime = 0;

            using (var result = await cmd.ExecuteReaderAsync())
            {
                while (await result.ReadAsync())
                {
                    string storedProcedure = result["Procedurename"].ToString();
                    double executionTime = Convert.ToDouble(result["ExecutionTime"]);

                    if (storedProcedure == "SearchForBookLoop")
                    {
                        loopTime = executionTime;
                    }
                    else if (storedProcedure == "SearchForBook")
                    {
                        joinsTime = executionTime;
                    }
                }
            }

            await context.Database.CloseConnectionAsync();

            return (loopTime, joinsTime);
        }
    }
}
