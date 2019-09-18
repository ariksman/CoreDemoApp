using System.Threading.Tasks;
using CSharpFunctionalExtensions;

namespace CoreDemoApp.Common.CQS
{
  public interface ICommandHandler<TCommand, TResult>
    where TCommand : ICommand
    where TResult : IResult
  {
    TResult Handle(TCommand command);
    Task<TResult> HandleAsync(TCommand command);
  }
}