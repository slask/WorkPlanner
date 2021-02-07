using System.Threading.Tasks;

namespace WorkPlanning.Infrastructure
{
    public interface ICommandHandler<in TCommand>
        where TCommand : class
    {
        Task Handle(TCommand command);
    }

    public interface ICommandHandler<in TCommand, TResult>
        where TCommand : class
    {
        Task<TResult> Handle(TCommand command);
    }
}