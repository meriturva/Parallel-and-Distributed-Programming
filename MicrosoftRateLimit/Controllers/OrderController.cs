using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace MicrosoftRateLimit.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrderController : ControllerBase
    {
        [HttpGet]
        [EnableRateLimiting("fixed-rate-limit")]
        public async Task<long> NewOrderAsync()
        {
            Console.WriteLine("Elaborating request");
            var orderResult = Random.Shared.Next(0, 100);
            await Task.Delay(100);
            Console.WriteLine($"Elaborated request with result: {orderResult}");

            return orderResult;
        }
    }
}