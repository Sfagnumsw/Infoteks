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
        public FileController(IFilteringRepositoryService repositoryService, IWebHostEnvironment webHostEnvironment)
        {
            this.repositoryService = repositoryService;
            this.webHostEnvironment = webHostEnvironment;
        }

        [HttpPost]
        [Route("AddFile")]
        public async Task<CsvFileItem> AddFile(IFormFile file)
        {
            var a = await repositoryService.FileRegistration(file);
            return a.First();
        }
    }
}
