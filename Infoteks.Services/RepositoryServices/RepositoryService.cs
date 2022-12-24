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

namespace Infoteks.Services.RepositoryServices
{
    internal class RepositoryService : IFilteringRepositoryService
    {
        private readonly IResultsRepository resultsRepository;
        private readonly IValuesRepository valuesRepository;
        public RepositoryService(IResultsRepository resultsRepository, IValuesRepository valuesRepository)
        {
            this.resultsRepository = resultsRepository;
            this.valuesRepository = valuesRepository;
        }
        public async Task FileRegistration(IFormFile file)
        {
            using MemoryStream memoryStream = new MemoryStream(new byte[file.Length]);
            await file.CopyToAsync(memoryStream);
            memoryStream.Position = 0;
            using (StreamReader reader = new StreamReader(memoryStream))
            using (CsvReader csvReader = new CsvReader(reader, System.Globalization.CultureInfo.InvariantCulture))
            {
                var records = csvReader.GetRecordsAsync<>(); //TODO
            }
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
    }
}
