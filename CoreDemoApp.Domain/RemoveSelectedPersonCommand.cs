using System;
using CoreDemoApp.Core.CQS;

namespace CoreDemoApp.Domain
{
  public class RemoveSelectedPersonCommand : ICommand
  {
    public RemoveSelectedPersonCommand(Guid? personId)
    {
      PersonId = personId;
    }

    public Guid? PersonId { get; }
  }
}