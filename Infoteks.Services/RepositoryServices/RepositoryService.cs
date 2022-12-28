using Infoteks.Domain.Entities;
using Infoteks.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Infoteks.DAL.Interfaces;
using CsvHelper;
using CsvHelper.Configuration;
using System.Globalization;
using Infoteks.Domain.Helpers.ClassMapCsvHelper;
using Microsoft.Extensions.Logging;
using System.Text.Json;
using Infoteks.Domain.Helpers.Exceptions;

namespace Infoteks.Services.RepositoryServices
{
    public class RepositoryService : IFilteringRepositoryService
    {
        private readonly IResultsRepository resultsRepository;
        private readonly IValuesRepository valuesRepository;
        private static ILogger logger;
        public RepositoryService(IResultsRepository resultsRepository, IValuesRepository valuesRepository, ILogger<RepositoryService> _logger)
        {
            this.resultsRepository = resultsRepository;
            this.valuesRepository = valuesRepository;
            logger = _logger;
        }

        public async Task FileRegistration(IFormFile csvFile)
        {
            await TrySaveInDb(csvFile);
        }

        public async Task<string> GetJsonResults(string fileName)
        {
            return await TrySerializeRezult(fileName);
        }

        public async Task<string> GetJsonResults(DateTime firstOperationTime)
        {
            return await TrySerializeRezult(firstOperationTime);
        }

        public async Task<string> GetJsonResults(double averageIndicator)
        {
            return await TrySerializeRezult(averageIndicator);
        }

        public async Task<string> GetJsonResults(int averageCompletionTime)
        {
            return await TrySerializeRezult(averageCompletionTime);
        }

        public async Task<string> GetJsonResults()
        {
            return await TrySerializeRezult();
        }

        public async Task<IEnumerable<Values>> GetValues(string fileName)
        {
            return await TryGetValues(fileName);
        }

        public async Task<IEnumerable<Values>> GetValues()
        {
            return await TryGetValues();
        }

        //Private methods//

        private async Task TrySaveInDb(IFormFile csvFile)
        {
            try
            {
                var values = await ParseCsvInValuesModel(csvFile);
                var results = ResultsModelFormation(values);
                await SaveOrOverwrite(values, results);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
            }
        }

        private async Task<CsvReader> GetCsvReaderObject(IFormFile csvFile)
        {
            MemoryStream memoryStream = new MemoryStream(new byte[csvFile.Length]);
            await csvFile.CopyToAsync(memoryStream);
            memoryStream.Position = 0;
            var config = new CsvConfiguration(CultureInfo.GetCultureInfo("ru-RU")) { Delimiter = ";" };
            StreamReader reader = new StreamReader(memoryStream);
            return new CsvReader(reader, config);
        }

        private async Task<IEnumerable<Values>> ParseCsvInValuesModel(IFormFile csvFile)
        {
            using (var csvReader = await GetCsvReaderObject(csvFile))
            {
                csvReader.Context.RegisterClassMap<CsvFileItemClassMap>();
                var records = csvReader.GetRecords<Values>().ToList();
                if (records.Count != 0)
                {
                    foreach (var i in records)
                    {
                        i.Id = Guid.NewGuid();
                        i.FileName = csvFile.FileName;
                    }
                    return records;
                }
                else throw new EmptyScvException("Method: ParseCsvInValuesModel | Message: CsvFile is empty") { };
            }
        }

        private Domain.Entities.Results ResultsModelFormation(IEnumerable<Values> values)
        {
            Domain.Entities.Results results = new Domain.Entities.Results()
            {
                Id = Guid.NewGuid(),
                FileName = values.First().FileName,
                AllTime = CalculateAllTime(values).ToString("dd'|'hh'|'mm'|'ss"),
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

        private int CalculateAverageCompletionTime(IEnumerable<Values> values)
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

        private async Task<bool> CheckForFileName(string fileName)
        {
            var relevant = await resultsRepository.GetOnFileName(fileName);
            return relevant != null ? true : false;
        }

        private async Task SaveOrOverwrite(IEnumerable<Values> values, Domain.Entities.Results results)
        {
            if (await CheckForFileName(results.FileName))
            {
                var dbValues = await valuesRepository.GetOnFileName(results.FileName);
                var dbResults = await resultsRepository.GetOnFileName(results.FileName);
                foreach (var v in dbValues) await valuesRepository.Remove(v);
                await resultsRepository.Remove(dbResults);
            }
            foreach (var v in values) await valuesRepository.Save(v);
            await resultsRepository.Save(results);
        }

        private async Task<string> TrySerializeRezult(string fileName)
        {
            try
            {
                var results = await resultsRepository.GetOnFileName(fileName);
                return JsonSerializer.Serialize<Domain.Entities.Results>(results);
            }
            catch (Exception ex)
            {
                logger.LogInformation(ex.Message);
                return ex.Message;
            }
        }

        private async Task<string> TrySerializeRezult(DateTime firstOperation)
        {
            try
            {
                var results = await resultsRepository.Get();
                var result = results.Where(i => i.FirstOperation.Equals(firstOperation));
                return JsonSerializer.Serialize<IEnumerable<Domain.Entities.Results>>(result);
            }
            catch (Exception ex)
            {
                logger.LogInformation(ex.Message);
                return ex.Message;
            }
        }

        private async Task<string> TrySerializeRezult(double averageIndicator)
        {
            try
            {
                var results = await resultsRepository.Get();
                var result = results.Where(i => i.AverageIndicator.Equals(averageIndicator));
                return JsonSerializer.Serialize<IEnumerable<Domain.Entities.Results>>(result);
            }
            catch (Exception ex)
            {
                logger.LogInformation(ex.Message);
                return ex.Message;
            }
        }

        private async Task<string> TrySerializeRezult(int averageCompletionTime)
        {
            try
            {
                var results = await resultsRepository.Get();
                var result = results.Where(i => i.AverageCompletionTime.Equals(averageCompletionTime));
                return JsonSerializer.Serialize<IEnumerable<Domain.Entities.Results>>(result);
            }
            catch (Exception ex)
            {
                logger.LogInformation(ex.Message);
                return ex.Message;
            }
        }

        private async Task<string> TrySerializeRezult()
        {
            try
            {
                var results = await resultsRepository.Get();
                return JsonSerializer.Serialize<IEnumerable<Domain.Entities.Results>>(results);
            }
            catch (Exception ex)
            {
                logger.LogInformation(ex.Message);
                return ex.Message;
            }
        }

        private async Task<IEnumerable<Values>> TryGetValues(string fileName)
        {
            try
            {
                return await valuesRepository.GetOnFileName(fileName);
            }
            catch (Exception ex)
            {
                logger.LogInformation(ex.Message);
                return new List<Values>();
            }
        }

        private async Task<IEnumerable<Values>> TryGetValues()
        {
            try
            {
                return await valuesRepository.Get();
            }
            catch (Exception ex)
            {
                logger.LogInformation(ex.Message);
                return new List<Values>();
            }
        }
    }
}