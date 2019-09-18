using System.Threading.Tasks;

namespace CoreDemoApp.Common.CQS
{
  public interface IQueryHandler<TQuery, TResult>
    where TQuery : IQuery
  {
    TResult Handle(TQuery query);

    Task<TResult> HandleAsync(TQuery query);
  }
}