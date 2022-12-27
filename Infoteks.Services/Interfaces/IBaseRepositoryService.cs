using Infoteks.Domain.Entities;
using Microsoft.AspNetCore.Http;

namespace Infoteks.Services.Interfaces
{
    public interface IBaseRepositoryService
    {
        Task FileRegistration(IFormFile file);
        Task<string> GetJsonResults();
        Task<IEnumerable<Values>> GetValues();
    }
}
