using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace WorkPlanning.Infrastructure
{
    public static class CommandResultExtensions
    {
        public static IActionResult ToActionResult(this CommandResult result) => result.ToActionResult(() => new OkObjectResult(result));

        public static IActionResult ToActionResult(this CommandResult result, Func<IActionResult> onSuccess)
        {
            return result.IsFailure
                ? new BadRequestObjectResult(result)
                : onSuccess();
        }

        public static IActionResult ToActionResult<T>(this CommandResult<T> result, Func<CommandResult<T>, IActionResult> onSuccess)
        {
            return result.IsFailure
                ? new BadRequestObjectResult(result)
                : onSuccess(result);
        }
        
        public static CommandResult OnSuccess(this CommandResult result, Action act)
        {
            if (result.IsSuccess)
            {
                act();
            }

            return result;
        }

        public static Task<CommandResult<T>> Then<T>(this CommandResult result, Func<Task<CommandResult<T>>> func)
        {
            if (result.IsSuccess)
            {
                return func();
            }

            return Task.FromResult(CommandResult<T>.Failure(result.Error));
        }

        public static Task<CommandResult> Then(this CommandResult result, Func<Task<CommandResult>> func)
        {
            if (result.IsSuccess)
            {
                return func();
            }

            return Task.FromResult(CommandResult.Failure(result.Error));
        }
    }
}
