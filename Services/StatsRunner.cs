using TemperatureAnalysis.Domain;
using TemperatureAnalysis.Analytics;
using TemperatureAnalysis.Reporting;

namespace TemperatureAnalysis.Services
{
    public static class StatsRunner
    {
        public static void Generate(string filename, ValidationResult validation,
            TemperatureAnalyzer analyzer, IReportGenerator reporter)
        {
            var stats = analyzer.Analyze(validation.ValidReadings);
            reporter.Generate(filename, validation, stats);
        }
    }
}
