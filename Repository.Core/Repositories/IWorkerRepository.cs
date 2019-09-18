using System.Collections.Generic;
using Repository.Core.Domain;

namespace Repository.Core.Repositories
{
    public interface IWorkerRepository : IRepository<Worker>
    {
        bool AddWorker(Worker worker);
        bool RemoveWorker(Worker worker);
        bool RemoveWorker(int workerId);
        Employer GetWorkerEmployer(Worker worker);
        List<Worker> GetAllWorkersWithEmployers();
    }
}