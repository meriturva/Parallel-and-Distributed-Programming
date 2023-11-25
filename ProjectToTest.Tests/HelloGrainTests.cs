using System.Threading.Tasks;
using Xunit;

namespace ProjectToTest.Tests
{
    public class HelloGrainTests
    {
        [Fact]
        public async Task TestSayHello()
        {
            // ARRANGE
            var helloGrain = new HelloGrain();

            // ACT
            var result = await helloGrain.SayHello("Diego");

            // ASSERT
            Assert.Equal("Hello, Diego!", result);
        }
    }
}