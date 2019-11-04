using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Autofac;

namespace CoreDemoApp.Core.DDD
{
  public class DomainEventDispatcher : IDomainEventDispatcher
  {
    private readonly ILifetimeScope _container;

    public DomainEventDispatcher(ILifetimeScope container)
    {
      _container = container;
    }

    public void Dispatch(BaseDomainEvent domainEvent)
    {
      var handlerType = typeof(IHandle<>).MakeGenericType(domainEvent.GetType());
      var wrapperType = typeof(DomainEventHandler<>).MakeGenericType(domainEvent.GetType());
      var handlers = (IEnumerable) _container.Resolve(typeof(IEnumerable<>).MakeGenericType(handlerType));
      var wrappedHandlers = handlers.Cast<object>()
        .Select(handler => (DomainEventHandler) Activator.CreateInstance(wrapperType, handler));

      foreach (var handler in wrappedHandlers) handler.Handle(domainEvent);
    }

    private abstract class DomainEventHandler
    {
      public abstract void Handle(BaseDomainEvent domainEvent);
    }

    private class DomainEventHandler<T> : DomainEventHandler
      where T : BaseDomainEvent
    {
      private readonly IHandle<T> _handler;

      public DomainEventHandler(IHandle<T> handler)
      {
        _handler = handler;
      }

      public override void Handle(BaseDomainEvent domainEvent)
      {
        _handler.Handle((T) domainEvent);
      }
    }
  }
}