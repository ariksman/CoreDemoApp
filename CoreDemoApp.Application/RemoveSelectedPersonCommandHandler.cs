using System;
using System.Threading.Tasks;
using CoreDemoApp.Core.CQS;
using CoreDemoApp.Domain;
using CSharpFunctionalExtensions;
using Repository.Core;

namespace CoreDemoApp.Application
{
  public class RemoveSelectedPersonCommandHandler : ICommandHandler<RemoveSelectedPersonCommand, Result>
  {
    private readonly IUnitOfWork _unitOfWork;

    public RemoveSelectedPersonCommandHandler(IUnitOfWork unitOfWork)
    {
      _unitOfWork = unitOfWork;
    }

    public Result Handle(RemoveSelectedPersonCommand command)
    {
      try
      {
        if (!command.PersonId.HasValue) return Result.Fail("Null argument given");

        var selectedWorker = _unitOfWork.Workers.GetById(command.PersonId.Value);
        _unitOfWork.Workers.RemoveWorker(selectedWorker);
        _unitOfWork.Complete();
        return Result.Ok();
      }
      catch (Exception e)
      {
        return Result.Fail(e.Message);
      }
    }

    public Task<Result> HandleAsync(RemoveSelectedPersonCommand command)
    {
      throw new NotImplementedException();
    }
  }
}