using System;
using CoreDemoApp.Core.CQS;

namespace CoreDemoApp.Domain.Model.Person
{
  public class RemoveSelectedPersonCommand : ICommand
  {
    public Guid? PersonId { get; }

    public RemoveSelectedPersonCommand(Guid? personId)
    {
      PersonId = personId;
    }
  }
}