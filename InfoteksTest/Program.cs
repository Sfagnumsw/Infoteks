using Infoteks.DAL.Context.EFContext;
using Infoteks.DAL.Interfaces;
using Infoteks.DAL.Repositories;
using Infoteks.Services.Interfaces;
using Infoteks.Services.RepositoryServices;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace InfoteksTest
{
    public class Program
    {
        public IConfiguration Configuration { get; }
        public Program(IConfiguration configuration) => Configuration = configuration;
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Configuration.Bind("Project", new Config());

            builder.Services.AddScoped<IResultsRepository, EF_ResultsRepository>();
            builder.Services.AddScoped<IValuesRepository, EF_ValuesRepository>();
            builder.Services.AddScoped<IFilteringRepositoryService, RepositoryService>();
            builder.Services.AddDbContext<AppDbContext>(i => i.UseSqlServer(Config.ConnectionString, b => b.MigrationsAssembly("InfoteksTest")));
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.MapControllers();

            app.Run();
        }
    }
}