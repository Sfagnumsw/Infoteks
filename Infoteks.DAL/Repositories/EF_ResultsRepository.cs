using Infoteks.DAL.Context.EFContext;
using Infoteks.DAL.Interfaces;
using Infoteks.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infoteks.DAL.Repositories
{
    public class EF_ResultsRepository : IResultsRepository
    {
        private readonly AppDbContext _context;
        public EF_ResultsRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Results>> Get() => await _context.Results.ToListAsync();

        public async Task Remove(Guid id)
        {
            Deteching(id);
            _context.Remove(new Results { Id = id });
            await _context.SaveChangesAsync();
        }

        public async Task Save(Results model)
        {
            await _context.Results.AddAsync(model);
            await _context.SaveChangesAsync();
        }

        private void Deteching(Guid id)
        {
            var local = _context.Set<Results>().Local.FirstOrDefault(i => i.Id == id);
            if (local != null)
            {
                _context.Entry(local).State = EntityState.Detached;
            }
        }
    }
}
