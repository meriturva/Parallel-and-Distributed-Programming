using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace MicrosoftRateLimit
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();

            builder.Services.AddRateLimiter(rateLimitOptions => rateLimitOptions
                .AddFixedWindowLimiter(policyName: "fixed-rate-limit", fixWindowOptions =>
                {
                    fixWindowOptions.PermitLimit = 100;
                    fixWindowOptions.Window = TimeSpan.FromSeconds(5);
                }));

            var app = builder.Build();

            // Configure the HTTP request pipeline.

            app.UseHttpsRedirection();

            app.UseAuthorization();
            app.UseRateLimiter();

            app.MapControllers();

            app.Run();
        }
    }
}