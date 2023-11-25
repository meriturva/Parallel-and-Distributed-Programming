using System.Threading.Tasks;

namespace ProjectToTest
{
    public class AService : IAService
    {
        public async Task<int> GetCoundFromDataBase()
        {
            await Task.CompletedTask;
            return 10;
        }
    }
}
