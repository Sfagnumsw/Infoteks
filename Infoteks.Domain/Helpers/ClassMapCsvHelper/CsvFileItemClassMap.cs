using CsvHelper.Configuration;
using Infoteks.Domain.Entities;
using Infoteks.Domain.Helpers.AttributesValidation;

namespace Infoteks.Domain.Helpers.ClassMapCsvHelper
{
    public class CsvFileItemClassMap : ClassMap<CsvFileItem>
    {
        public CsvFileItemClassMap() 
        {
            Map(p => p.Date).Name("Дата");
            Map(p => p.CompletionTime).Name("Время выполнения");
            Map(p => p.Indicator).Name("Показатель");

            Map(p => p.Date).TypeConverter<CsvHelper.TypeConversion.DateTimeConverter>().TypeConverterOption.Format("yyyy-MM-dd_hh-mm-ss");

            Map(p => p.Date).Validate();//TODO
        }
    }
}
