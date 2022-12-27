using Infoteks.Domain.Entities;

namespace Infoteks.Services.Interfaces
{
    public interface IFilteringRepositoryService : IBaseRepositoryService
    {
        Task<string> GetJsonResults(string fileName);
        Task<string> GetJsonResults(DateTime firstOperationTime);
        Task<string> GetJsonResults(double averageIndicator);
        Task<string> GetJsonResults(int averageCompletionTime);
        Task<IEnumerable<Values>> GetValues(string fileName);
    }
}
