using System;
using System.Collections.Generic;
using System.Linq;
using TemperatureAnalysis.Domain;

namespace TemperatureAnalysis.Analytics
{
    public class CircadianAnalysisService
    {
        public string GenerateSummary(IEnumerable<TemperatureReading> readings)
        {
            var grouped = readings
                .GroupBy(r => r.Timestamp.Hour < 12 ? "Daytime (0–11)" : "Night (12–23)")
                .Select(g => new
                {
                    Period = g.Key,
                    Max = g.Max(x => x.Value),
                    Min = g.Min(x => x.Value),
                    Avg = g.Average(x => x.Value)
                });

            return string.Join(Environment.NewLine, grouped.Select(g =>
                $"{g.Period}: Max = {g.Max:F2}, Min = {g.Min:F2}, Avg = {g.Avg:F2}"));
        }
    }
}
