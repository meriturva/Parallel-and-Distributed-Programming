using Orleans.TestingHost;
using System;
using System.Threading.Tasks;
using Xunit;

namespace ProjectToTest.Tests
{
    public class HelloGrainTestsTestCluster
    {
        [Fact]
        public async Task TestSayHello()
        {
            // ARRANGE
            var builder = new TestClusterBuilder();
            var cluster = builder.Build();
            cluster.Deploy();

            // ACT
            var hello = cluster.GrainFactory.GetGrain<IHelloGrain>("my-id");
            var result = await hello.SayHello("Diego");
            cluster.StopAllSilos();

            // ASSERT
            Assert.Equal("Hello, Diego!", result);
        }
    }
}