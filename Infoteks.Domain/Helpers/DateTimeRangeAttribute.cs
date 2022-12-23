using System.ComponentModel.DataAnnotations;

namespace Infoteks.Domain.Helpers
{
    public class DateTimeRangeAttribute : RangeAttribute
    {
        public DateTimeRangeAttribute() : base(typeof(DateTime), "2000-01-01 00-00-00", DateTime.Now.ToString("yyyy-MM-dd hh-mm-ss")) { }
    }
}