namespace CoreDemoApp.Core.DDD
{
  public interface IDomainEventDispatcher
  {
    void Dispatch(BaseDomainEvent domainEvent);
  }
}