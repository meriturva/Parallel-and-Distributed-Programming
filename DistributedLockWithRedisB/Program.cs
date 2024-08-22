using RedLockNet.SERedis;
using RedLockNet.SERedis.Configuration;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace DistributedLockWithRedisB
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            var endPoints = new List<RedLockEndPoint> { new DnsEndPoint("localhost", 6379) };
            var redlockFactory = RedLockFactory.Create(endPoints);

            var resource = "my-order-id";
            var expiry = TimeSpan.FromSeconds(30);

            await using (var redLock = await redlockFactory.CreateLockAsync(resource, expiry))
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