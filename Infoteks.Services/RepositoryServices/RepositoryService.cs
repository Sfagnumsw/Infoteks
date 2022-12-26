using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infoteks.Domain.Entities;
using Infoteks.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Infoteks.DAL.Interfaces;
using CsvHelper;
using CsvHelper.Configuration;
using System.Globalization;
using Infoteks.Domain.Helpers.ClassMapCsvHelper;

namespace Infoteks.Services.RepositoryServices
{
    public class RepositoryService : IFilteringRepositoryService
    {
        private readonly IResultsRepository resultsRepository;
        private readonly IValuesRepository valuesRepository;
        public RepositoryService(IResultsRepository resultsRepository, IValuesRepository valuesRepository)
        {
            this.resultsRepository = resultsRepository;
            this.valuesRepository = valuesRepository;
        }
        public async Task<Results> FileRegistration(IFormFile csvFile)
        {
            var values = await ParseCsvInValuesModel(csvFile);
            var results = ResultsModelFormation(values);
            return results;
        }

        public Task<string> GetJsonResults(string fileName)
        {
            throw new NotImplementedException();
        }

        public Task<string> GetJsonResults(DateTime firstOperationTime)
        {
            throw new NotImplementedException();
        }

        public Task<string> GetJsonResults(double averageIndicator)
        {
            throw new NotImplementedException();
        }

        public Task<string> GetJsonResults(int averageCompletionTime)
        {
            throw new NotImplementedException();
        }

        public Task<string> GetJsonResults()
        {
            throw new NotImplementedException();
        }

        public Task<Values> GetValues(string fileName)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Values>> GetValues()
        {
            throw new NotImplementedException();
        }

        //Private methods//

        private async Task TrySaveInDataBase(IFormFile csvFile) //TODO
        {
            try
            {

            }
            catch
            {

            }
        }

        private async Task<IEnumerable<Values>> ParseCsvInValuesModel(IFormFile csvFile)
        {
            using MemoryStream memoryStream = new MemoryStream(new byte[csvFile.Length]);
            await csvFile.CopyToAsync(memoryStream);
            memoryStream.Position = 0;
            var config = new CsvConfiguration(CultureInfo.GetCultureInfo("ru-RU")) { Delimiter = ";" };
            using (StreamReader reader = new StreamReader(memoryStream))
            using (CsvReader csvReader = new CsvReader(reader, config))
            {
                csvReader.Context.RegisterClassMap<CsvFileItemClassMap>();
                var records = csvReader.GetRecords<Values>().ToList();
                foreach(var i in records)
                {
                    i.Id = Guid.NewGuid();
                    i.FileName = csvFile.FileName;
                }
                return records;
            }
        }

        private Results ResultsModelFormation(IEnumerable<Values> values)
        {
            Results results = new Results()
            {
                Id = Guid.NewGuid(),
                FileName = values.First().FileName,
                AllTime = CalculateAllTime(values),
                FirstOperation = CalculateFirstOperation(values),
                AverageCompletionTime = CalculateAverageCompletionTime(values),
                AverageIndicator = CalculateAverageIndicator(values),
                MedianIndicators = CalculateMedianIndicators(values),
                MaxIndicator = FormationIndicatorArray(values).Max(),
                MinIndicator = FormationIndicatorArray(values).Min(),
                CountString = values.Count()
            };
            return results;
        }

        private TimeSpan CalculateAllTime(IEnumerable<Values> values)
        {
            var dates = FormationDateArray(values);
            return dates.Max().Subtract(dates.Min());
        }

        private DateTime CalculateFirstOperation(IEnumerable<Values> values)
        {
            var dates = FormationDateArray(values);
            return dates.Min();
        }

        private double CalculateAverageCompletionTime(IEnumerable<Values> values)
        {
            int[] time = FormationCompletionTimeArray(values);
            int sum = 0;
            foreach(var t in time)
            {
                sum = sum + t;
            }
            return sum / time.Length;
        }

        private double CalculateAverageIndicator(IEnumerable<Values> values)
        {
            double[] indicators = FormationIndicatorArray(values);
            double sum = 0;
            foreach(var indicator in indicators)
            {
                sum = sum + indicator;
            }
            return sum / indicators.Length;
        }

        private double CalculateMedianIndicators(IEnumerable<Values> values)
        {
            var indicators = FormationIndicatorArray(values);
            Array.Sort(indicators);
            double median;
            int length = indicators.Length;
            if (length % 2 != 0) median = indicators[(length + 1) / 2 - 1];
            else median = (indicators[length / 2 - 1] + indicators[length / 2]) / 2;
            return median;
        }

        private DateTime[] FormationDateArray(IEnumerable<Values> values)
        {
            DateTime[] array = new DateTime[values.Count()];
            int i = 0;
            foreach (var v in values)
            {
                array[i++] = v.Date;
            }
            return array;
        }

        private double[] FormationIndicatorArray(IEnumerable<Values> values)
        {
            double[] array = new double[values.Count()];
            int i = 0;
            foreach(var v in values)
            {
                array[i++] = v.Indicator;
            }
            return array;
        }

        private int[] FormationCompletionTimeArray(IEnumerable<Values> values)
        {
            int[] array = new int[values.Count()];
            int i = 0;
            foreach (var v in values)
            {
                array[i++] = v.CompletionTime;
            } 
            return array;
        }
    }
}