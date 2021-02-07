using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace WorkPlanning.Persistence
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddRWorkPlanningPersistence(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString(nameof(WorkPlanningContext));
            return services.AddDbContext<WorkPlanningContext>(o => o.UseSqlServer(connectionString));
        }
    }
}
