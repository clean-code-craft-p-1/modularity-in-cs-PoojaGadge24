using System;
using System.Linq;
using TemperatureAnalysis.Domain;

namespace TemperatureAnalysis.Reporting
{
    public class ConsoleReportWriter
    {
        public void Write(ValidationResult validation, (double Max, double Min, double Avg) stats)
        {
            Console.WriteLine(new string('=', 60));
            Console.WriteLine("Temperature Analysis Summary");
            Console.WriteLine(new string('=', 60));
            Console.WriteLine($"Total readings: {validation.ValidReadings.Count + validation.Errors.Count}");
            Console.WriteLine($"Valid readings: {validation.ValidReadings.Count}");
            Console.WriteLine($"Errors: {validation.Errors.Count}");
            Console.WriteLine(new string('-', 60));
            Console.WriteLine($"Max temperature: {stats.Max:F2}");
            Console.WriteLine($"Min temperature: {stats.Min:F2}");
            Console.WriteLine($"Average temperature: {stats.Avg:F2}");

            if (validation.Errors.Any())
            {
                Console.WriteLine("Invalid lines:");
                foreach (var (line, content) in validation.Errors)
                    Console.WriteLine($"  Line {line}: {content}");
            }
        }
    }
}
