using Microsoft.AspNetCore.Mvc;
using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading;
using System.Threading.Tasks;

namespace MicroserviceA.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly HttpClient _client;

        public OrderController(HttpClient client)
        {
            _client = client;
        }

        [HttpGet]
        public async Task<long> NewOrder()
        {
            Console.WriteLine("Elaborating request");
            var orderResult = Random.Shared.Next(0, 100);
            Thread.Sleep(1000);
            Console.WriteLine($"Elaborated request with result: {orderResult}");

            Console.WriteLine("Sending request to MicroserviceB");
            var paymentResult = await _client.GetFromJsonAsync<long>("https://localhost:7165/payment");
            Console.WriteLine($"Sent request MicroserviceB with result {paymentResult}");

            var finalResult = orderResult + paymentResult;
            Console.WriteLine($"Final result {finalResult}");
            return finalResult;
        }
    }
}