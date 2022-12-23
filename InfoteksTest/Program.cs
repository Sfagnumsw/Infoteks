using Infoteks.DAL.Context.EFContext;
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