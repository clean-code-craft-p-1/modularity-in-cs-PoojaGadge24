using System;
using TemperatureAnalysis.Domain;
using TemperatureAnalysis.Analytics;

namespace TemperatureAnalysis.Services
{
    public static class FeverRunner
    {
        public static void Evaluate(ValidationResult validation, FeverDetectionService fever)
        {
            if (fever.TryDetectFever(validation.ValidReadings, out var start, out var end))
                Console.WriteLine($"\nFEVER ALERT: {start:HH:mm:ss} → {end:HH:mm:ss} ({end - start})");
            else
                Console.WriteLine("\nNo fever condition detected.");
        }
    }
}
