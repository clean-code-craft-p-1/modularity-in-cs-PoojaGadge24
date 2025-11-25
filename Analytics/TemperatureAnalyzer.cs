using System.Linq;
using TemperatureAnalysis.Domain;

namespace TemperatureAnalysis.Analytics
{
    public class TemperatureAnalyzer
    {
        public (double Max, double Min, double Avg) Analyze(IEnumerable<TemperatureReading> readings)
        {
            var temps = readings.Select(r => r.Value).ToList();
            return (temps.Max(), temps.Min(), temps.Average());
        }
    }
}
