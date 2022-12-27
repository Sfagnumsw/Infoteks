using System.ComponentModel.DataAnnotations;

namespace Infoteks.Domain.Entities
{
    public class Results
    {
        public Guid Id { get; set; }
        public string? FileName { get; set; }
        public string AllTime { get; set; }
        public DateTime FirstOperation { get; set; }
        public int AverageCompletionTime { get; set; }
        public double AverageIndicator { get; set; }
        public double MedianIndicators { get; set; }
        public double MaxIndicator { get; set; }
        public double MinIndicator { get; set; }
        [Range(1,10000)]
        public int CountString { get; set; }
    }
}
