using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace WorkPlanning.Persistence
{
    internal sealed class ReferenceDataContextFactory : IDesignTimeDbContextFactory<WorkPlanningContext>
    {
        public WorkPlanningContext CreateDbContext(string[] args)
        {
            var options = new DbContextOptionsBuilder<WorkPlanningContext>()
                .UseSqlServer("Server=localhost,1433;Database=WorkPlanningDb;User=sa;Password=WorkPlanning1!;Connect Timeout=300;")
                .Options;

            return new WorkPlanningContext(options);
        }
    }
}