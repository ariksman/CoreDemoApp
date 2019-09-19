namespace CoreDemoApp.Common.DDD
{
  public interface IDomainEventDispatcher
  {
    void Dispatch(BaseDomainEvent domainEvent);
  }
}