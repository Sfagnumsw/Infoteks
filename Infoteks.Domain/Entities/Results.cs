using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infoteks.Domain.Entities
{
    public class Results
    {
        public Guid Id { get; set; }

        public string? FileName { get; set; }

        public int AllTime { get; set; }

        public DateTime FirstOperation { get; set; }

        public int AverageCompletionTime { get; set; }

        public double AverageIndicator { get; set; }

        public double MedianIndicator { get; set; }

        public double MaxIndicator { get; set; }

        public double MinIndicator { get; set; }
         
        public int CountString { get; set; }
    }
}
