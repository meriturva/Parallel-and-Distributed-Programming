using RedLockNet.SERedis;
using RedLockNet.SERedis.Configuration;
using System.Net;

namespace DistributedLockWithRedisA
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            var endPoints = new List<RedLockEndPoint> { new DnsEndPoint("localhost", 6379) };
            var redlockFactory = RedLockFactory.Create(endPoints);

            var resource = "my-order-id";
            var expiry = TimeSpan.FromSeconds(30);

            await using (var redLock = await redlockFactory.CreateLockAsync(resource, expiry)) // there are also non async Create() methods
            {
                // make sure we got the lock
                if (redLock.IsAcquired)
                {
                    // do stuff
                }
            }
        }
    }
}