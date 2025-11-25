using System;
using System.Globalization;
using TemperatureAnalysis.Domain;

namespace TemperatureAnalysis.Parsing
{
    public class TemperatureParser
    {
        public ValidationResult Parse(string[] lines)
        {
            var result = new ValidationResult();

            for (int i = 0; i < lines.Length; i++)
            {
                string line = lines[i].Trim();
                if (string.IsNullOrWhiteSpace(line))
                    continue;

                var parts = line.Split(',');
                if (parts.Length != 2)
                {
                    result.Errors.Add((i + 1, line));
                    continue;
                }

                string timestamp = parts[0].Trim();
                string valueStr = parts[1].Trim();

                // Time parsing (strict format & culture-safe)
                if (!TimeSpan.TryParseExact(
                    timestamp,
                    "hh\\:mm\\:ss",
                    CultureInfo.InvariantCulture,
                    out var timePart))
                {
                    result.Errors.Add((i + 1, line));
                    continue;
                }

                // Temperature parsing
                if (!double.TryParse(valueStr, NumberStyles.Float, CultureInfo.InvariantCulture, out double temp) ||
                    temp < -100 || temp > 200)
                {
                    result.Errors.Add((i + 1, line));
                    continue;
                }

                result.ValidReadings.Add(new TemperatureReading(DateTime.Today.Add(timePart), temp));
            }

            return result;
        }
    }
}
