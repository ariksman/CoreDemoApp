using Autofac;
using CoreDemoApp.Dialogs;
using CoreDemoApp.Domain.ImpureServices;

namespace CoreDemoApp.Infrastructure
{
  public class ServicesModule : Module
  {
    protected override void Load(ContainerBuilder builder)
    {
      builder.RegisterType<DatabaseSeeder>().As<IDatabaseSeeder>();
      builder.RegisterType<CustomMessageDialogService>().As<IMessageDialogService>();
    }
  }
}