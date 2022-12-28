using Infoteks.DAL.Context.EFContext;
using Infoteks.DAL.Interfaces;
using Infoteks.Domain.Entities;
using Microsoft.EntityFrameworkCore;

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

        public async Task Remove(Results result)
        {
            Deteching(result.Id);
            _context.Remove(result);
            await _context.SaveChangesAsync();
        }

        public async Task Save(Results model)
        {
            await _context.Results.AddAsync(model);
            await _context.SaveChangesAsync();
        }

        public async Task<Results> GetOnFileName(string fileName)
        {
            return await _context.Results.FirstOrDefaultAsync(i => i.FileName.Equals(fileName));
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
