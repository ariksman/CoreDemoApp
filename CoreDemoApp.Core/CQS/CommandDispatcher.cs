using System.Threading.Tasks;
using Autofac;
using Autofac.Core.Lifetime;
using CSharpFunctionalExtensions;

namespace CoreDemoApp.Core.CQS
{
  public class CommandDispatcher : ICommandDispatcher
  {
    private readonly LifetimeScope _scope;

    public CommandDispatcher(ILifetimeScope scope)
    {
      _scope = scope as LifetimeScope;
    }

    public TResult Dispatch<TCommand, TResult>(TCommand command)
      where TCommand : ICommand
      where TResult : IResult
    {
      using var childScope = _scope.RootLifetimeScope.BeginLifetimeScope();
      var handler = childScope.Resolve<ICommandHandler<TCommand, TResult>>();
      return handler.Handle(command);
    }

    public Task<TResult> DispatchAsync<TCommand, TResult>(TCommand command)
      where TCommand : ICommand
      where TResult : IResult
    {
      using var childScope = _scope.RootLifetimeScope.BeginLifetimeScope();
      var handler = childScope.Resolve<ICommandHandler<TCommand, TResult>>();
      return handler.HandleAsync(command);
    }
  }
}