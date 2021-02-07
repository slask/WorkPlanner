using System.Threading.Tasks;
using WorkPlanning.Domain;
using WorkPlanning.Infrastructure;
using WorkPlanning.Persistence;

namespace WorkPlanning.Commands.Workers
{
    public class AddWorkerCommandHandler: ICommandHandler<AddWorkerCommand, CommandResult<int>>
    {
        private readonly WorkPlanningContext _db;

        public AddWorkerCommandHandler(WorkPlanningContext db)
        {
            _db = db;
        }
        
        public async Task<CommandResult<int>> Handle(AddWorkerCommand command)
        {
            var entry = _db.Workers.Add(new Worker
            {
                Name = command.Name
            });
            await _db.SaveChangesAsync();
            
            return CommandResult<int>.Success(entry.Entity.Id);
        }
    }
}