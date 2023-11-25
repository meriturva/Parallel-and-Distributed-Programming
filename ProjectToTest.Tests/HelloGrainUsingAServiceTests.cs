using NSubstitute;
using System.Threading.Tasks;
using Xunit;

namespace ProjectToTest.Tests
{
    public class HelloGrainUsingAServiceTests
    {
        [Fact]
        public async Task TestCount()
        {
            // ARRANGE
            var service = Substitute.For<IAService>();
            service.GetCoundFromDataBase().Returns(5);

            var helloGrain = new HelloGrainUsingAService(service);

            // ACT
            var result = await helloGrain.Count();

            // ASSERT
            Assert.Equal(5, result);
        }
    }
}