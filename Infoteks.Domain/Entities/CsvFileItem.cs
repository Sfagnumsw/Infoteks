using CsvHelper.Configuration.Attributes;

namespace Infoteks.Domain.Entities
{
    public class CsvFileItem
    {
        public DateTime Date { get; set; }
        public int CompletionTime { get; set; }
        public double Indicator { get; set; }
    }
}
