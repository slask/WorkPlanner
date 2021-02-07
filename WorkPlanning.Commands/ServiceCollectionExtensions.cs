using Microsoft.Extensions.DependencyInjection;
using WorkPlanning.Commands.Workers;
using WorkPlanning.Infrastructure;

namespace WorkPlanning.Commands
{
    public static class ServiceCollectionExtensions
    {
        
        public static IServiceCollection AddCommands(this IServiceCollection services)
        {
            return services
                .AddScoped<ICommandHandler<AddWorkerCommand, CommandResult<int>>, AddWorkerCommandHandler>()
                .AddScoped<ICommandHandler<AddShiftCommand, CommandResult>, AddShiftCommandHandler>();

        }
    }
}