using System;
using System.IO;

namespace TemperatureAnalysis.Services
{
    public static class FileValidator
    {
        public static bool Validate(string filename)
        {
            if (File.Exists(filename)) return true;
            Console.WriteLine("Error: File not found.");
            return false;
        }
    }
}
