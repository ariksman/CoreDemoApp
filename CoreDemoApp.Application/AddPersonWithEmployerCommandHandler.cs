using System;
using System.Threading.Tasks;
using CoreDemoApp.Core.CQS;
using CoreDemoApp.Domain;
using CSharpFunctionalExtensions;
using Repository.Core;

namespace CoreDemoApp.Application
{
  public class AddPersonWithEmployerCommandHandler : ICommandHandler<AddPersonWithEmployer, Result>
  {
    private readonly IUnitOfWork _unitOfWork;

    public AddPersonWithEmployerCommandHandler(IUnitOfWork unitOfWork)
    {
      _unitOfWork = unitOfWork;
    }

    public Result Handle(AddPersonWithEmployer command)
    {
      try
      {
        // TODO: add employee logic https://blogs.cuttingedge.it/steven/posts/2012/returning-data-from-command-handlers/
        _unitOfWork.Workers.AddWorker(command.Worker);
        _unitOfWork.Complete();
        return Result.Ok();
      }
      catch (Exception e)
      {
        return Result.Fail(e.Message);
      }
    }

    public Task<Result> HandleAsync(AddPersonWithEmployer command)
    {
      throw new NotImplementedException();
    }
  }
}