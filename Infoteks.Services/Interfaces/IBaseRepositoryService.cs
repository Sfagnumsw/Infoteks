using Infoteks.Domain.Entities;

namespace Infoteks.Services.Interfaces
{
    public interface IBaseRepositoryService
    {
        Task FileRegistration();
        Task<string> GetJsonResults();
        Task<IEnumerable<Values>> GetValues();
    }
}
