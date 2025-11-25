using System;
using System.IO;
using TemperatureAnalysis.Domain;
using TemperatureAnalysis.Parsing;
using TemperatureAnalysis.Analytics;
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
            _feverService = new FeverDetectionService(38.0, 30); //  default temperature threshold and duration
            _circadianService = new CircadianAnalysisService();
        }

        public void ProcessBatch(string filename)
        {
            if (!File.Exists(filename))
            {
                Console.WriteLine("Error: File not found.");
                return;
            }

            var lines = File.ReadAllLines(filename);
            var validation = _parser.Parse(lines);

            if (!validation.ValidReadings.Any())
            {
                Console.WriteLine("No valid temperature data found.");
                return;
            }

            var stats = _analyzer.Analyze(validation.ValidReadings);
            _reportGenerator.Generate(filename, validation, stats);

            //Fever Detection 
            if (_feverService.TryDetectFever(validation.ValidReadings, out var start, out var end))
            {
                Console.WriteLine("\nFEVER ALERT: Temperature sustained above threshold.");
                Console.WriteLine($"   Start: {start:HH:mm:ss}, End: {end:HH:mm:ss}");
                Console.WriteLine($"   Duration: {end - start}");
            }
            else
            {
                Console.WriteLine("\nNo fever condition detected.");
            }

            //Circadian Summary
            Console.WriteLine("\nCircadian Pattern Summary:");
            Console.WriteLine(_circadianService.GenerateSummary(validation.ValidReadings));
        }
    }
}
