using System;

namespace CoreDemoApp.Domain.DDD
{
  public abstract class BaseDomainEvent
  {
    public DateTime DateOccurred { get; protected set; } = DateTime.UtcNow;
  }
}