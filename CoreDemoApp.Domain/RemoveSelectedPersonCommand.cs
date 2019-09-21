using System;
using CoreDemoApp.Core.CQS;
using CSharpFunctionalExtensions;

namespace CoreDemoApp.Domain
{
  public class RemoveSelectedPersonCommand : ICommand
  {
    public int? PersonId { get; }

    public RemoveSelectedPersonCommand(int? personId)
    {
      PersonId = personId;
    }
  }
}