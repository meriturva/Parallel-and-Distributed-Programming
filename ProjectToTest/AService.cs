using System.Threading.Tasks;

namespace ProjectToTest
{
    public class AService : IAService
    {
        public async Task<int> GetCountFromDataBase()
        {
            await Task.CompletedTask;
            return 10;
        }
    }
}
