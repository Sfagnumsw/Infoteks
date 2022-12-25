using Infoteks.Domain.Helpers.AttributesValidation;
using System.ComponentModel.DataAnnotations;

namespace Infoteks.Domain.Entities
{
    public class Values
    {
        [ScaffoldColumn(false)]
        public Guid Id { get; set; }

        [Display(Name = "Название файла")]
        public string FileName { get; set; }

        [DateTimeRange]
        [Display(Name = "Дата и время")]
        public DateTime Date { get; set; }

        [Range(0,Int32.MaxValue)]
        [Display(Name = "Время выполнения")]
        public int CompletionTime { get; set; }

        [Range(0, Double.MaxValue)]
        [Display(Name = "Показатель")]
        public double Indicator { get; set; }
    }
}
