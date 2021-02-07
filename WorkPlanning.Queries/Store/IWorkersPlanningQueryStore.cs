using System.Linq;
using WorkPlanning.Domain;

namespace WorkPlanning.Queries.Store
{
    public interface IWorkersPlanningQueryStore
    {
        IQueryable<Worker> Workers { get; }
    }
}