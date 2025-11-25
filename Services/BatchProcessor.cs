using System;
using System.IO;
using TemperatureAnalysis.Domain;
using TemperatureAnalysis.Parsing;
using TemperatureAnalysis.Reporting;
using TemperatureAnalysis.Analytics;

namespace TemperatureAnalysis.Services
{
    public class BatchProcessor
    {
        private readonly TemperatureParser _parser = new();
        private readonly TemperatureAnalyzer _analyzer = new();
        private readonly IReportGenerator _reporter = new FileReportGenerator();
        private readonly FeverDetectionService _fever = new();
        private readonly CircadianAnalysisService _circadian = new();

        public void ProcessBatch(string filename)
        {
            if (!FileValidator.Validate(filename)) return;

            var validation = _parser.Parse(File.ReadAllLines(filename));
            if (!validation.ValidReadings.Any())
            {
                Console.WriteLine("No valid temperature data found.");
                return;
            }

            StatsRunner.Generate(filename, validation, _analyzer, _reporter);
            FeverRunner.Evaluate(validation, _fever);
            CircadianRunner.Evaluate(validation, _circadian);
        }
    }
}
