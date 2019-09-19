using CoreDemoApp.Common.DDD;

namespace CoreDemoApp.Common.CQS
{
  public interface IHandle<T> where T : BaseDomainEvent
  {
    void Handle(T domainEvent);
  }
}