using System;
using System.IO;
using TemperatureAnalysis.Domain;

namespace TemperatureAnalysis.Reporting
{
    public class FileReportGenerator : IReportGenerator
    {
        public void Generate(string filename, ValidationResult validation, (double Max, double Min, double Avg) stats)
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

            string outName = filename + "_summary.txt";
            try
            {
                using var writer = new StreamWriter(outName);
                writer.WriteLine("Temperature Analysis Summary");
                writer.WriteLine($"File analyzed: {filename}");
                writer.WriteLine($"Total readings: {validation.ValidReadings.Count + validation.Errors.Count}");
                writer.WriteLine($"Valid readings: {validation.ValidReadings.Count}");
                writer.WriteLine($"Errors: {validation.Errors.Count}");
                writer.WriteLine($"Max: {stats.Max:F2}");
                writer.WriteLine($"Min: {stats.Min:F2}");
                writer.WriteLine($"Avg: {stats.Avg:F2}");

                if (validation.Errors.Any())
                {
                    writer.WriteLine("\nInvalid lines:");
                    foreach (var (line, content) in validation.Errors)
                        writer.WriteLine($"  Line {line}: {content}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error writing report: {ex.Message}");
            }
        }
    }
}
