using FluentValidation;
using WorkPlanning.Commands.Workers;

namespace WorkPlanning.Commands.Validations
{
    public class AddWorkerValidator: AbstractValidator<AddWorkerCommand>
    {
        public AddWorkerValidator()
        {
            RuleFor(x => x.Name).NotNull().NotEmpty();
        }
    }
}