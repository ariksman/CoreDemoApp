using System;

namespace CoreDemoApp.Core.DDD
{
  public abstract class BaseDomainEvent
  {
    public DateTime DateOccurred { get; protected set; } = DateTime.UtcNow;
  }
}