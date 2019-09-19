namespace CoreDemoApp.Common.DDD
{
  public interface IHandle<T> where T : BaseDomainEvent
  {
    void Handle(T domainEvent);
  }
}