﻿using Infoteks.Domain.Entities;
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
        public async Task<IEnumerable<Values>> AddFile(IFormFile file)
        {
            var a = await repositoryService.FileRegistration(file);
            return a;
        }
    }
}
