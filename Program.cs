using System;
using System.IO;
using TemperatureAnalysis.Services;

namespace TemperatureAnalysis
{
    static class Program
    {
        static void Main(string[] args)
        {
            string testFilename = "test_temps.csv";
            File.WriteAllLines(testFilename, new[]
            {
                "09:15:30,23.5",            
                "09:16:00,24.1",
                "09:16:30,22.8",
                "09:17:00,25.3",
                "09:17:30,23.9",
                "09:18:00,24.7",
                "09:18:30,22.4",
                "09:19:00,26.1",
                "09:19:30,23.2",
                "09:20:00,25.0",

                // Start of fever (continuous above threshold)
                "09:40:00,38.3",
                "09:50:00,38.5",
                "10:00:00,38.6",
                "10:10:00,38.4"
            });

            new BatchProcessor().ProcessBatch(testFilename);
        }
    }
}
