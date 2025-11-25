using TemperatureAnalysis.Domain;

namespace TemperatureAnalysis.Reporting
{
    public interface IReportGenerator
    {
        void Generate(string filename, ValidationResult validation, (double Max, double Min, double Avg) stats);
    }
}
