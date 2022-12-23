using Infoteks.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infoteks.DAL.Interfaces
{
    public interface IResultsRepository
    {
        Task Save(Results model);
        Task Remove(Guid id);
        Task<IEnumerable<Results>> Get();
    }
}
