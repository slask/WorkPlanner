using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using WorkPlanning.Commands;
using WorkPlanning.Persistence;
using WorkPlanning.Queries;
using WorkPlanningServiceApp;

[assembly: FunctionsStartup(typeof(Startup))]

namespace WorkPlanningServiceApp
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            builder.Services
                .AddRWorkPlanningPersistence(builder.GetContext().Configuration)
                .AddCommands()
                .AddQueries();
        }
   }
}