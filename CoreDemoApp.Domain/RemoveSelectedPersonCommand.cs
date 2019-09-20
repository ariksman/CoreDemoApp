using System;
using CoreDemoApp.Core.CQS;
using CSharpFunctionalExtensions;

namespace CoreDemoApp.Domain
{
  public class RemoveSelectedPersonCommand : ICommand
  {
    public int? PersonId { get; set; }

    public RemoveSelectedPersonCommand(int? personId)
    {
      PersonId = personId;
    }
  }
}