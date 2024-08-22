using System.Threading.Tasks;
using Xunit;

namespace ProjectToTest.Tests
{
    public class HelloGrainTests
    {
        [Theory]
        [InlineData("Diego", "Hello, Diego!")]
        [InlineData("", "Hello, !")]
        public async Task TestSayHello(string name, string expectedValue)
        {
            // ARRANGE
            var helloGrain = new HelloGrain();

            // ACT
            var result = await helloGrain.SayHello(name);

            // ASSERT
            Assert.Equal(expectedValue, result);
        }
    }
}