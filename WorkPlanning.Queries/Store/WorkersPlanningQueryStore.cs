using System.Linq;
using Microsoft.EntityFrameworkCore;
using WorkPlanning.Domain;
using WorkPlanning.Persistence;

namespace WorkPlanning.Queries.Store
{
    internal sealed class WorkersPlanningQueryStore : IWorkersPlanningQueryStore
    {
        private readonly WorkPlanningContext _context;

        public WorkersPlanningQueryStore(WorkPlanningContext context)
        {
            _context = context;
        }

        public IQueryable<Worker> Workers => _context.Workers.AsNoTracking();

    }
}
