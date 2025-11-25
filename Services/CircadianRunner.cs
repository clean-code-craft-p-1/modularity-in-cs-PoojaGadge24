using System;
using TemperatureAnalysis.Domain;
using TemperatureAnalysis.Analytics;

namespace TemperatureAnalysis.Services
{
    public static class CircadianRunner
    {
        public static void Evaluate(ValidationResult validation, CircadianAnalysisService circadian)
        {
            Console.WriteLine("\nCircadian Pattern Summary:");
            Console.WriteLine(circadian.GenerateSummary(validation.ValidReadings));
        }
    }
}
