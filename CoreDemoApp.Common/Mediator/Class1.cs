using System;
using System.Collections.Generic;
using System.Text;

namespace CoreDemoApp.Common.Mediator
{
  public interface INotifierMediatorService
  {
    void Notify(string notifyText);
  }

  public class NotifierMediatorService : INotifierMediatorService
  {
    private readonly IMediator _mediator;

    public NotifierMediatorService(IMediator mediator)
    {
      _mediator = mediator;
    }

    public void Notify(string notifyText)
    {
      _mediator.Publish(new NotificationMessage { NotifyText = notifyText });
    }
  }
}
