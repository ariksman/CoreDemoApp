using System;
using Repository.Core.Repositories;

namespace Repository.Core
{
    public interface IUnitOfWork : IDisposable
    {
        IEmployerRepository Employers { get; }
        IWorkerRepository Workers { get; }
        int Complete();
        void Migrate();
    }
}
