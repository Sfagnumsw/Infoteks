using CsvHelper;
using CsvHelper.Configuration;
using Infoteks.Domain.Entities;

namespace Infoteks.Domain.Helpers.ClassMapCsvHelper
{
    public class CsvFileItemClassMap : ClassMap<Values>
    {
        public CsvFileItemClassMap() 
        {
            Map(p => p.Date).Name("Дата").Validate(field => ValidateDate(field)).TypeConverter<CsvHelper.TypeConversion.DateTimeConverter>().TypeConverterOption.Format("yyyy-MM-dd_HH-mm-ss");
            Map(p => p.CompletionTime).Name("Время выполнения").Validate(field => ValidateCompletionTime(field));
            Map(p => p.Indicator).Name("Показатель").Validate(field => ValidateIndicator(field));
        }
        private static bool ValidateDate(ValidateArgs date)
        {
            DateTime minDate = DateTime.ParseExact("2000-01-01_00-00-00", "yyyy-MM-dd_HH-mm-ss", System.Globalization.CultureInfo.InvariantCulture);
            DateTime inputDate = DateTime.ParseExact(date.Field, "yyyy-MM-dd_HH-mm-ss", System.Globalization.CultureInfo.InvariantCulture);
            return inputDate < minDate ? false : true;
        }

        private static bool ValidateCompletionTime(ValidateArgs completionTime) 
        {
            int inputTime = int.Parse(completionTime.Field);
            return inputTime < 0 ? false : true;
        }

        private static bool ValidateIndicator(ValidateArgs indicator)
        {
            double inputIndicator = double.Parse(indicator.Field, System.Globalization.CultureInfo.GetCultureInfo("ru-RU"));
            return inputIndicator < 0 ? false : true;
        }
    }
}
