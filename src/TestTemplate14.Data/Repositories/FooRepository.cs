using TestTemplate14.Core.Entities;
using TestTemplate14.Core.Interfaces;

namespace TestTemplate14.Data.Repositories
{
    public class FooRepository : Repository<Foo>, IFooRepository
    {
        public FooRepository(TestTemplate14DbContext context)
            : base(context)
        {
        }
    }
}
