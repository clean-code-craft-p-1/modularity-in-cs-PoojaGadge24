namespace TemperatureAnalysis.Reporting
{
    public class FileReportGenerator : IReportGenerator
    {
        private readonly ConsoleReportWriter _consoleWriter = new();
        private readonly FileReportWriter _fileWriter = new();

        public void Generate(string filename, ValidationResult validation, (double Max, double Min, double Avg) stats)
        {
            _consoleWriter.Write(validation, stats);
            _fileWriter.Write(filename, validation, stats);
        }
    }
}