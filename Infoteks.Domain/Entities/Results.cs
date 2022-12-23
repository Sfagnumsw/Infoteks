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
        [ScaffoldColumn(false)]
        public Guid Id { get; set; }

        [Display(Name = "Название файла")]
        public string FileName { get; set; }

        [Display(Name = "Все время")]
        public int AllTime { get; set; }

        [Display(Name = "Время запуска первой операции")]
        public DateTime FirstOperation { get; set; }

        [Display(Name = "Среднее время выполнения")]
        public int AverageCompletionTime { get; set; }

        [Display(Name = "Средней показатель")]
        public double AverageIndicator { get; set; }

        [Display(Name = "Медиана показателей")]
        public double MedianIndicator { get; set; }

        [Display(Name = "Максимальный показатель")]
        public double MaxIndicator { get; set; }

        [Display(Name = "Минимальный показатель")]
        public double MinIndicator { get; set; }
        
        [Range(0, 10000)]
        [Display(Name = "Количество строк")]
        public int CountString { get; set; }
    }
}
