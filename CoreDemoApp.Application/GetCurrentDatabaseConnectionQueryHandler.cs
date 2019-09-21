using System;
using System.Threading.Tasks;
using CoreDemoApp.Core.CQS;
using CoreDemoApp.Domain;
using CSharpFunctionalExtensions;
using Repository.Core;

namespace CoreDemoApp.Application
{
  public class
    GetCurrentDatabaseConnectionQueryHandler : IQueryHandler<GetCurrentDatabaseConnectionQuery, Result<string>>
  {
    private readonly IUnitOfWork _unitOfWork;

    public GetCurrentDatabaseConnectionQueryHandler(IUnitOfWork unitOfWork)
    {
      _unitOfWork = unitOfWork;
    }

    public Result<string> Handle(GetCurrentDatabaseConnectionQuery query)
    {
      try
      {
        var data = _unitOfWork.Connection;
        return Result.Ok(data);
      }
      catch (Exception e)
      {
        return Result.Fail<string>(e.Message);
      }
    }

    public Task<Result<string>> HandleAsync(GetCurrentDatabaseConnectionQuery query)
    {
      throw new NotImplementedException();
    }
  }
}