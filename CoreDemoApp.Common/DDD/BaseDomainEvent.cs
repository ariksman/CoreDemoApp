using System;

namespace CoreDemoApp.Common.DDD
{
  public abstract class BaseDomainEvent
  {
    public DateTime DateOccurred { get; protected set; } = DateTime.UtcNow;
  }
}