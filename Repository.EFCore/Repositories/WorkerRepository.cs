using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Repository.Core.DataModel;
using Repository.Core.Repositories;

namespace Repository.EFCore.Repositories
{
  public class WorkerRepository : Repository<Worker>, IWorkerRepository
  {
    public WorkerRepository(DbContext context) : base(context)
    {
    }

    public bool AddWorker(Worker worker)
    {
      DatabaseContext.Workers.Add(worker);
      return true;
    }

    public bool RemoveWorker(Worker worker)
    {
      DatabaseContext.Workers.Remove(worker ?? throw new InvalidOperationException());
      return true;
    }

    public bool RemoveWorker(Guid workerId)
    {
      var localWorker = DatabaseContext.Workers.FirstOrDefault(w => w.WorkerId == workerId);
      DatabaseContext.Workers.Remove(localWorker ?? throw new InvalidOperationException());
      return true;
    }

    public Employer GetWorkerEmployer(Worker worker)
    {
      throw new NotImplementedException();
    }

    public List<Worker> GetAllWorkersWithEmployers()
    {
      var workers = DatabaseContext.Workers
        .Include(worker => worker.Employer1)
        .ToList();

      return workers;
    }

    public DatabaseContext DatabaseContext => Context as DatabaseContext;
  }
}