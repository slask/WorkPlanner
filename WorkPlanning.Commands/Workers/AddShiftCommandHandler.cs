using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WorkPlanning.Infrastructure;
using WorkPlanning.Persistence;

namespace WorkPlanning.Commands.Workers
{
    public class AddShiftCommandHandler: ICommandHandler<AddShiftCommand, CommandResult>
    {
        private readonly WorkPlanningContext _db;

        public AddShiftCommandHandler(WorkPlanningContext db)
        {
            _db = db;
        }
        
        public async Task<CommandResult> Handle(AddShiftCommand command)
        {
            var worker = await _db.Workers.Include(w => w.Shifts)
                .FirstOrDefaultAsync(w => w.Id == command.WorkerId);

            var result = worker.AddShift(command.StartDate, command.EndDate);
            if (result.IsFailure)
            {
                return result;
            }
            
            await _db.SaveChangesAsync();
            
            return CommandResult.Success();
        }
    }
}