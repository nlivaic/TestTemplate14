using System.Threading.Tasks;

namespace TestTemplate14.Common.Interfaces
{
    public interface IUnitOfWork
    {
        Task<int> SaveAsync();
    }
}