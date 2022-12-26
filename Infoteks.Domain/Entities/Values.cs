using Infoteks.Domain.Helpers.AttributesValidation;
using System.ComponentModel.DataAnnotations;

namespace Infoteks.Domain.Entities
{
    public class Values
    {
        public Guid Id { get; set; }

        [Required]
        public string? FileName { get; set; }

        public DateTime Date { get; set; }

        public int CompletionTime { get; set; }

        public double Indicator { get; set; }
    }
}
