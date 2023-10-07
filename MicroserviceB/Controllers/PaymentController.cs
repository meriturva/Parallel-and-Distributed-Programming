using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading;

namespace MicroserviceB.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PaymentController : ControllerBase
    {
        [HttpGet]
        public long Get()
        {
            var result = Random.Shared.Next(0, 100);
            Thread.Sleep(1000);
            Console.WriteLine(result);
            return result;
        }
    }
}