using System.Reflection;
using Autofac;
using Autofac.Core;
using Microsoft.EntityFrameworkCore;
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
      builder.RegisterType<DatabaseContext>()
        .WithParameter(
          new ResolvedParameter(
            (pi, ctx) => pi.ParameterType == typeof(DbContextOptions) && pi.Name == "options",
            (pi, ctx) =>
            {
              var dataSource = DatabaseConnectionFactory.GetInstallDirectory();
              return DatabaseConnectionFactory.CreateConnectionString(dataSource);
            }))
        .InstancePerLifetimeScope();
        

      //builder.RegisterGeneric(typeof(Repository<>)).As(typeof(IRepository<>));
      var repositoriesAssembly = Assembly.GetAssembly(typeof(EmployerRepository));
      builder.RegisterAssemblyTypes(repositoriesAssembly)
        .Where(t => t.Name.EndsWith("Repository"))
        .AsImplementedInterfaces();

      builder.RegisterType<EfUnitOfWork>().As<IUnitOfWork>();
    }
  }
}