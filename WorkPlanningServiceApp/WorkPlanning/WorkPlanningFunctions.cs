using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using WorkPlanning.Commands.Validations;
using WorkPlanning.Commands.Workers;
using WorkPlanning.Infrastructure;
using WorkPlanning.Queries.Models;
using WorkPlanning.Queries.Store;

namespace WorkPlanningServiceApp
{
    public class WorkPlanning
    {
        private readonly ICommandHandler<AddWorkerCommand, CommandResult<int>> _addWorkerCommandHandler;
        private readonly ICommandHandler<AddShiftCommand, CommandResult> _addShiftCommandHanfdler;
        private readonly IWorkersPlanningQueryStore _workersPlanningQueryStore;

        public WorkPlanning(ICommandHandler<AddWorkerCommand, CommandResult<int>> addWorkerCommandHandler,
            ICommandHandler<AddShiftCommand, CommandResult> addShiftCommandHanfdler,
            IWorkersPlanningQueryStore workersPlanningQueryStore)
        {
            _workersPlanningQueryStore = workersPlanningQueryStore;
            _addShiftCommandHanfdler = addShiftCommandHanfdler;
            _addWorkerCommandHandler = addWorkerCommandHandler;
        }

        [FunctionName(nameof(AddWorker))]
        public async Task<IActionResult> AddWorker([HttpTrigger(AuthorizationLevel.Anonymous, "POST", Route = "worker")]
            HttpRequest req, ILogger log)
        {
            log.LogInformation("adding a new worker");

            var (command, validationResult) = await req.ValidateBody<AddWorkerCommand, AddWorkerValidator>();

            return (await validationResult
                    .Then(() => _addWorkerCommandHandler.Handle(command)))
                .ToActionResult((r) => new ObjectResult(r) { StatusCode = (int) HttpStatusCode.Created });
        }

        [FunctionName(nameof(AddShift))]
        public async Task<IActionResult> AddShift(
            [HttpTrigger(AuthorizationLevel.Anonymous, "POST", Route = "worker/{workerId:int}/shifts")]
            HttpRequest req, ILogger log, int? workerId)
        {
            var (command, validationResult) = await req.ValidateBody<AddShiftCommand, AddShiftValidator>();
            command.WorkerId = workerId.GetValueOrDefault();

            return (await validationResult
                    .Then(() => _addShiftCommandHanfdler.Handle(command)))
                .ToActionResult();
        }

        [FunctionName(nameof(GetWorkerDetails))]
        public async Task<IActionResult> GetWorkerDetails(
            [HttpTrigger(AuthorizationLevel.Anonymous, "GET", Route = "worker/{workerId:int}")]
            HttpRequest req, ILogger log, int? workerId)
        {
            var worker = _workersPlanningQueryStore.Workers.Include(w => w.Shifts).FirstOrDefault(w => w.Id == workerId);

            var model = new WorkerModel
            {
                Name = worker.Name,
                Shifts = string.Join(',', worker.Shifts.Select(s => $"{s.Start}-{s.End}"))
            };
            return new OkObjectResult(model);
        }
    }
}