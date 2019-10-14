using System;
using CoreDemoApp.Core.CQS;
using CSharpFunctionalExtensions;

namespace CoreDemoApp.Domain
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