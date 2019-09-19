using Autofac;
using CoreDemoApp.Application.Interfaces;
using CoreDemoApp.Domain;

namespace CoreDemoApp.Infrastructure
{
  public class ServicesModule : Autofac.Module
  {
    protected override void Load(ContainerBuilder builder)
    {
      builder.RegisterType<DatabaseSeeder>().As<IDatabaseSeeder>();
      builder.RegisterType<OokiiMessageDialogService>().As<IMessageDialogService>();
    }
  }
}