using Microsoft.EntityFrameworkCore;
using WorkPlanning.Domain;

namespace WorkPlanning.Persistence
{
    public sealed class WorkPlanningContext : DbContext
    {
        public WorkPlanningContext(DbContextOptions<WorkPlanningContext> options)
            : base(options)
        {
        }

        public DbSet<Worker> Workers { get; private set; }

        public DbSet<Shift> Shifts { get; private set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.ApplyConfigurationsFromAssembly(typeof().Assembly);
        }
    }
}