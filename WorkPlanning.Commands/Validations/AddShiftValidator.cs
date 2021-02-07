using FluentValidation;
using WorkPlanning.Commands.Workers;

namespace WorkPlanning.Commands.Validations
{
    public class AddShiftValidator: AbstractValidator<AddShiftCommand>
    {
        public AddShiftValidator()
        {
            RuleFor(x => x.EndDate).GreaterThan(cmd => cmd.StartDate);
        }
    }
}