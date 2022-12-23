using Infoteks.DAL.Context.EFContext;
using Infoteks.DAL.Interfaces;
using Infoteks.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infoteks.DAL.Repositories
{
    public class EF_ValuesRepository : IValuesRepository
    {
        private readonly AppDbContext _context;
        public EF_ValuesRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Values>> Get()
        {
            return await _context.Values.ToListAsync();
        }

        public async Task Save(Values model)
        {
            await _context.AddAsync(model);
            await _context.SaveChangesAsync();
        }

        public async Task Remove(Guid id)
        {
            Deteching(id);
            _context.Remove(new Values { Id = id});
            await _context.SaveChangesAsync();
        }

        private void Deteching(Guid id)
        {
            var local = _context.Set<Values>().Local.FirstOrDefault(i => i.Id == id);
            if (local != null)
            {
                _context.Entry(local).State = EntityState.Detached;
            }
        }
    }
}
