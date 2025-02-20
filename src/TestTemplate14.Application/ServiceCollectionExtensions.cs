using Microsoft.Extensions.DependencyInjection;
using TestTemplate14.Application.Pipelines;

namespace TestTemplate14.Application
{
    public static class ServiceCollectionExtensions
    {
        public static void AddTestTemplate14ApplicationHandlers(this IServiceCollection services)
        {
            services.AddMediatR(config => config.RegisterServicesFromAssemblyContaining(typeof(ServiceCollectionExtensions)));
            services.AddPipelines();
            services.AddAutoMapper(typeof(ServiceCollectionExtensions).Assembly);
        }
    }
}
