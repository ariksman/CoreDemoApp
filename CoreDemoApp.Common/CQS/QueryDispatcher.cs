using System.Threading.Tasks;
using Autofac;

namespace CoreDemoApp.Common.CQS
{
  // https://gist.github.com/jbogard/54d6569e883f63afebc7
  // http://lostechies.com/jimmybogard/2014/05/13/a-better-domain-events-pattern/
  public class QueryDispatcher : IQueryDispatcher
  {
    private readonly ILifetimeScope _scope;

    public QueryDispatcher(ILifetimeScope scope)
    {
      _scope = scope;
    }

    public TResult Dispatch<TQuery, TResult>(TQuery query)
      where TQuery : IQuery
      where TResult : new()
    {
      var handler = _scope.Resolve<IQueryHandler<TQuery, TResult>>();
      return handler.Handle(query);
    }

    public Task<TResult> DispatchAsync<TQuery, TResult>(TQuery query)
      where TQuery : IQuery
      where TResult : new()
    {
      var handler = _scope.Resolve<IQueryHandler<TQuery, TResult>>();
      return handler.HandleAsync(query);
    }
  }
}
