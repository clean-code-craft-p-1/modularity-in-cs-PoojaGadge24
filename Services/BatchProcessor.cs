using System;
using System.IO;
using TemperatureAnalysis.Analytics;
using TemperatureAnalysis.Domain;
using TemperatureAnalysis.Parsing;
using TemperatureAnalysis.Reporting;

namespace TemperatureAnalysis.Services
{
    public class BatchProcessor
    {
        private readonly TemperatureParser _parser;
        private readonly TemperatureAnalyzer _analyzer;
        private readonly IReportGenerator _reportGenerator;
        private readonly FeverDetectionService _feverService;
        private readonly CircadianAnalysisService _circadianService;

        public BatchProcessor()
        {
            _parser = new TemperatureParser();
            _analyzer = new TemperatureAnalyzer();
            _reportGenerator = new FileReportGenerator();
            _feverService = new FeverDetectionService();
            _circadianService = new CircadianAnalysisService();
        }

        public void ProcessBatch(string filename)
        {
            if (!ValidateInputFile(filename)) return;

            var validation = _parser.Parse(File.ReadAllLines(filename));
            if (!validation.ValidReadings.Any())
            {
                Console.WriteLine("No valid temperature data found.");
                return;
            }

            GenerateStats(filename, validation);
            DetectFever(validation);
            DetectCircadian(validation);
        }

        private bool ValidateInputFile(string filename)
        {
            if (File.Exists(filename)) return true;
            Console.WriteLine("Error: File not found.");
            return false;
        }

        private void GenerateStats(string filename, ValidationResult validation)
        {
            var stats = _analyzer.Analyze(validation.ValidReadings);
            _reportGenerator.Generate(filename, validation, stats);
        }

        private void DetectFever(ValidationResult validation)
        {
            if (_feverService.TryDetectFever(validation.ValidReadings, out var start, out var end))
                Console.WriteLine($"\nFEVER ALERT: {start:HH:mm:ss} → {end:HH:mm:ss} ({end - start})");
            else
                Console.WriteLine("\nNo fever condition detected.");
        }

        private void DetectCircadian(ValidationResult validation)
        {
            Console.WriteLine("\nCircadian Pattern Summary:");
            Console.WriteLine(_circadianService.GenerateSummary(validation.ValidReadings));
        }
    }
}
