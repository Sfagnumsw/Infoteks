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
        public async Task<IEnumerable<Values>> FileRegistration(IFormFile file)
        {
            return await ParseCsvInValuesModel(file);
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

        //Private methods

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
    }
}
