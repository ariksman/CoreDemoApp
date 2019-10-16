using Autofac;
using CoreDemoApp.Dialogs;
using CoreDemoApp.Views.MainWindow;

namespace CoreDemoApp.Infrastructure
{
  public class ViewModelServicesModule : Autofac.Module
  {
    protected override void Load(ContainerBuilder builder)
    {
      //builder.RegisterType<DataCreator>().As<IDataCreator>();

      //builder.RegisterType<DataSetViewModel>().AsSelf().As<ISharedSettingsContext>().SingleInstance();
      builder.RegisterType<MainModel>().AsSelf();
      builder.RegisterType<PersonModel>().AsSelf();
      builder.RegisterType<PersonViewModel>().AsSelf();
      builder.RegisterType<MainViewModel>().AsSelf();
      builder.RegisterType<DialogViewModel>().AsSelf();
    }
  }
}