using System.Threading.Tasks;

namespace ProjectToTest
{
    public interface IAService
    {
        Task<int> GetCoundFromDataBase();
    }
}