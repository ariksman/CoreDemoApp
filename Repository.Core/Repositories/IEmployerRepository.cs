using System.Collections.Generic;
using Repository.Core.Domain;

namespace Repository.Core.Repositories
{
    public interface IEmployerRepository : IRepository<Employer>
    {
        bool AddEmployer(Employer employer);

        bool RemoveEmployer(Employer employer);

        IEnumerable<Worker> GetAllWorkers(Employer employer);
    }
}