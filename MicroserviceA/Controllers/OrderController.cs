using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Net.Http.Json;
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
            var paymentResult = await _client.GetFromJsonAsync<long>("https://localhost:7165/payment");
            return paymentResult - 10;
        }
    }
}