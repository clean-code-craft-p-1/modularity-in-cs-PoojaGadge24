using System;
using System.Collections.Generic;
using System.Linq;
using TemperatureAnalysis.Domain;

namespace TemperatureAnalysis.Analytics
{
    public class FeverDetectionService
    {
        private readonly double _thresholdTemp;
        private readonly TimeSpan _thresholdDuration;

        public FeverDetectionService(double thresholdTemp = 38.0, int minutes = 30)
        {
            _thresholdTemp = thresholdTemp;
            _thresholdDuration = TimeSpan.FromMinutes(minutes);
        }

        public bool TryDetectFever(IList<TemperatureReading> readings, out DateTime start, out DateTime end)
        {
            start = end = default;
            var ordered = readings.OrderBy(r => r.Timestamp).ToList();

            DateTime? possibleStart = null;

            foreach (var reading in ordered)
            {
                if (reading.Value < _thresholdTemp)
                {
                    possibleStart = null; 
                    continue;
                }

                if (possibleStart == null)
                    possibleStart = reading.Timestamp;

                end = reading.Timestamp;
                start = possibleStart.Value;

                if (end - start >= _thresholdDuration)
                    return true;
            }

            return false;
        }

    }
}
