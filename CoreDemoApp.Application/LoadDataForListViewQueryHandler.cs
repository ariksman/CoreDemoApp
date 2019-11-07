using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CoreDemoApp.Core.CQS;
using CoreDemoApp.Domain;
using CSharpFunctionalExtensions;
using Repository.Core;
using Repository.Core.DataModel;

namespace CoreDemoApp.Application
{
  public class
    LoadDataForListViewQueryHandler : IQueryHandler<LoadDataForListViewQuery, Result<List<Worker>>>
  {
    private readonly IUnitOfWork _unitOfWork;

    public LoadDataForListViewQueryHandler(IUnitOfWork unitOfWork)
    {
      _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    }

    public Result<List<Worker>> Handle(LoadDataForListViewQuery query)
    {
      try
      {
        var data = _unitOfWork.Workers.GetAllWorkersWithEmployers();
        return Result.Ok(data);
      }
      catch (Exception e)
      {
        return Result.Fail<List<Worker>>(e.Message);
      }
    }

    public Task<Result<List<Worker>>> HandleAsync(LoadDataForListViewQuery query)
    {
      throw new NotImplementedException();
    }
  }
}