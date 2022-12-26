using Infoteks.Domain.Entities;
using Microsoft.AspNetCore.Http;

namespace Infoteks.Services.Interfaces
{
    public interface IBaseRepositoryService
    {
        Task<IEnumerable<Values>> FileRegistration(IFormFile file);
        Task<string> GetJsonResults();
        Task<IEnumerable<Values>> GetValues();
    }
}
