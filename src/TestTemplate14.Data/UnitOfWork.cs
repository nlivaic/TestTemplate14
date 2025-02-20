using System.Threading.Tasks;
using TestTemplate14.Common.Interfaces;

namespace TestTemplate14.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly TestTemplate14DbContext _dbContext;

        public UnitOfWork(TestTemplate14DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<int> SaveAsync()
        {
            if (_dbContext.ChangeTracker.HasChanges())
            {
                return await _dbContext.SaveChangesAsync();
            }
            return 0;
        }
    }
}