using System;
using System.Threading.Tasks;
using CoreDemoApp.Core.CQS;
using CoreDemoApp.Domain;
using CoreDemoApp.Domain.ImpureServices;
using CSharpFunctionalExtensions;
using Repository.Core;

namespace CoreDemoApp.Application
{
  public class SeedDatabaseCommandHandler : ICommandHandler<SeedDatabaseCommand, Result>
  {
    private readonly IDatabaseSeeder _databaseSeeder;
    private readonly IUnitOfWork _unitOfWork;

    public SeedDatabaseCommandHandler(IUnitOfWork unitOfWork, IDatabaseSeeder databaseSeeder)
    {
      _unitOfWork = unitOfWork;
      _databaseSeeder = databaseSeeder;
    }

    public Result Handle(SeedDatabaseCommand command)
    {
      try
      {
        _databaseSeeder.SeedDatabase(_unitOfWork);
        _unitOfWork.Complete();
        return Result.Ok();
      }
      catch (Exception e)
      {
        return Result.Fail(e.Message);
      }
    }

    public Task<Result> HandleAsync(SeedDatabaseCommand command)
    {
      throw new NotImplementedException();
    }
  }
}