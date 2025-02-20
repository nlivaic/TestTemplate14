using Microsoft.Extensions.DependencyInjection;
using TestTemplate14.Common.Interfaces;
using TestTemplate14.Core.Interfaces;
using TestTemplate14.Data.Repositories;

namespace TestTemplate14.Data
{
    public static class ServiceCollectionExtensions
    {
        public static void AddSpecificRepositories(this IServiceCollection services) =>
            services.AddScoped<IFooRepository, FooRepository>();

        public static void AddGenericRepository(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        }
    }
}
