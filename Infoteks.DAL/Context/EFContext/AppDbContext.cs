using Microsoft.EntityFrameworkCore;
using Infoteks.Domain.Entities;

namespace Infoteks.DAL.Context.EFContext
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Values> Values { get; set; }
        public DbSet<Results> Results { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Values>().HasIndex(i => new { i.FileName });
        }
    }
}
