using System;
using System.IO;
using System.Linq;
using TemperatureAnalysis.Domain;

namespace TemperatureAnalysis.Reporting
{
    public class FileReportWriter
    {
        public void Write(string filename, ValidationResult validation, (double Max, double Min, double Avg) stats)
        {
            string outputFile = $"{filename}_summary.txt";

            try
            {
                using var writer = new StreamWriter(outputFile);
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
