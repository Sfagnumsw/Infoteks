using Infoteks.Domain.Entities;

namespace Infoteks.DAL.Interfaces
{
    public interface IResultsRepository
    {
        Task Save(Results model);
        Task Remove(Guid id);
        Task<IEnumerable<Results>> Get();
        Task<Results> GetOnFileName(string fileName);
    }
}
