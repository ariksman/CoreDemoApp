using Autofac;
using Repository.Core;
using Repository.Core.Repositories;
using Repository.EFCore;
using Repository.EFCore.Repositories;

namespace CoreDemoApp.Infrastructure
{
  public class RepositoryModule : Autofac.Module
  {
    protected override void Load(ContainerBuilder builder)
    {
      builder.RegisterType<EfUnitOfWork>().As<IUnitOfWork>();
      builder.RegisterGeneric(typeof(Repository<>)).As(typeof(IRepository<>));
      builder.RegisterType<EmployerRepository>().As<IEmployerRepository>();
      builder.RegisterType<WorkerRepository>().As<IWorkerRepository>();
    }
  }
}