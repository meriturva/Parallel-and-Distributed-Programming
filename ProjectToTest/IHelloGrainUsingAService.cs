using System.Threading.Tasks;

namespace ProjectToTest
{
    public interface IHelloGrainUsingAService
    {
        Task<int> Count();
    }
}