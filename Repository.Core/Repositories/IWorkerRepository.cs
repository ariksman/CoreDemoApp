﻿using System;
using System.Collections.Generic;
using Repository.Core.DataModel;

namespace Repository.Core.Repositories
{
  public interface IWorkerRepository : IRepository<Worker>
  {
    bool AddWorker(Worker worker);
    bool RemoveWorker(Worker worker);
    bool RemoveWorker(Guid workerId);
    Employer GetWorkerEmployer(Worker worker);
    List<Worker> GetAllWorkersWithEmployers();
  }
}