using EventsOutOfProcessByDatabaseShared;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace EventsOutOfProcessByDatabaseProducer
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddDbContext<EventBusContext>(
                options => options.UseSqlServer(@"Server=localhost,1433;Database=EventBus;User Id=sa;Password=Unicam123!;TrustServerCertificate=true")
            );
            builder.Services.AddControllers();

            var app = builder.Build();

            // Create database
            using (var scope = app.Services.CreateScope())
            {
                var db = scope.ServiceProvider.GetRequiredService<EventBusContext>();
                db.Database.EnsureCreated();
            }

            // Configure the HTTP request pipeline.
            app.UseHttpsRedirection();

            app.MapControllers();

            app.Run();
        }
    }
}