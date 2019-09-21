using System;
using System.Threading.Tasks;
using CoreDemoApp.Core.CQS;
using CoreDemoApp.Domain;
using CSharpFunctionalExtensions;
using Repository.Core;

namespace CoreDemoApp.Application
{
  public class AddPersonWithEmployerCommandHandler : ICommandHandler<AddPersonWithEmployer, Result<int>>
  {
    private readonly IUnitOfWork _unitOfWork;

    public AddPersonWithEmployerCommandHandler(IUnitOfWork unitOfWork)
    {
      _unitOfWork = unitOfWork;
    }

    public Result<int> Handle(AddPersonWithEmployer command)
    {
      try
      {
        // TODO: add employee logic
        _unitOfWork.Workers.AddWorker(command.Worker);
        _unitOfWork.Complete();
        return Result.Ok(command.Worker.WorkerId); //TODO: not the correct way to return id on command
      }
      catch (Exception e)
      {
        return Result.Fail<int>(e.Message);
      }
    }

    public Task<Result<int>> HandleAsync(AddPersonWithEmployer command)
    {
      throw new NotImplementedException();
    }
  }
}