using Orleans;
using System.Threading.Tasks;

namespace ProjectToTest
{
    public sealed class HelloGrainUsingAService : Grain, IHelloGrainUsingAService
    {
        private readonly IAService _service;

        public HelloGrainUsingAService(IAService service)
        {
            _service = service;
        }

        public async Task<int> Count()
        {
            return await _service.GetCoundFromDataBase();
        }
    }
}
