using Infoteks.Domain.Entities;

namespace Infoteks.DAL.Interfaces
{
    public interface IValuesRepository
    {
        Task Save(Values model);
        Task<IEnumerable<Values>> Get();
        Task Remove(Guid id);
    }
}
