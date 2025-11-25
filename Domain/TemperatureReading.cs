using System;

namespace TemperatureAnalysis.Domain
{
    public class TemperatureReading
    {
        public DateTime Timestamp { get; }
        public double Value { get; }

        public TemperatureReading(DateTime timestamp, double value)
        {
            Timestamp = timestamp;
            Value = value;
        }
    }
}
