using Microsoft.Extensions.DependencyInjection;
using WorkPlanning.Queries.Store;

namespace WorkPlanning.Queries
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddQueries(this IServiceCollection services)
        {
            return services.AddScoped<IWorkersPlanningQueryStore, WorkersPlanningQueryStore>();
        }
    }
}