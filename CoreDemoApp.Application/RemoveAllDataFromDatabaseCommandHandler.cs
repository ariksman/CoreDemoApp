using System;
using System.Threading.Tasks;
using CoreDemoApp.Core.CQS;
using CoreDemoApp.Domain;
using CoreDemoApp.Domain.ImpureServices;
using CSharpFunctionalExtensions;
using Repository.Core;

namespace CoreDemoApp.Application
{
  public class RemoveAllDataFromDatabaseCommandHandler : ICommandHandler<RemoveAllDataFromDatabaseCommand, Result>
  {
    private readonly IUnitOfWork _unitOfWork;

    public RemoveAllDataFromDatabaseCommandHandler(IUnitOfWork unitOfWork)
    {
      _unitOfWork = unitOfWork;
    }

    public Result Handle(RemoveAllDataFromDatabaseCommand command)
    {
      try
      {
        _unitOfWork.Workers.Clear();
        _unitOfWork.Employers.Clear();
        _unitOfWork.Complete();
        return Result.Ok();
      }
      catch (Exception e)
      {
        return Result.Fail(e.Message);
      }
    }

    public Task<Result> HandleAsync(RemoveAllDataFromDatabaseCommand command)
    {
      throw new NotImplementedException();
    }
  }
}