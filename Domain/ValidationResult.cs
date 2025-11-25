using System.Collections.Generic;

namespace TemperatureAnalysis.Domain
{
    public class ValidationResult
    {
        public List<TemperatureReading> ValidReadings { get; } = new();
        public List<(int Line, string Content)> Errors { get; } = new();
    }
}
