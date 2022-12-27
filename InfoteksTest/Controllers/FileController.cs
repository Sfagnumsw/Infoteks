using Infoteks.Domain.Entities;
using Infoteks.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace InfoteksTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileController : ControllerBase
    {
        private readonly IFilteringRepositoryService repositoryService;
        public FileController(IFilteringRepositoryService repositoryService)
        {
            this.repositoryService = repositoryService;
        }

        [HttpPost]
        [Route("AddFile")]
        public async Task<string> AddFile(IFormFile file)
        {
            await repositoryService.FileRegistration(file);
            return "OK";
        }

        [HttpGet]
        [Route("GetResult")]
        public async Task<string> GetResult()
        {
            return await repositoryService.GetJsonResults();
        }

        [HttpGet]
        [Route("GetResult/{fileName}")]
        public async Task<string> GetResult(string fileName)
        {
            return await repositoryService.GetJsonResults(fileName);
        }

        [HttpGet]
        [Route("GetResult/{averageIndicator}")]
        public async Task<string> GetResult(double averageIndicator)
        {
            return await repositoryService.GetJsonResults(averageIndicator);
        }

        [HttpGet]
        [Route("GetResult/{averageCompletionTime}")]
        public async Task<string> GetResult(int averageCompletionTime)
        {
            return await repositoryService.GetJsonResults(averageCompletionTime);
        }

        [HttpGet]
        [Route("GetResult/{firstOperationTime}")]
        public async Task<string> GetResult(DateTime firstOperationTime)
        {
            return await repositoryService.GetJsonResults(firstOperationTime);
        }
    }
}
