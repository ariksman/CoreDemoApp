using Autofac;
using CoreDemoApp.Dialogs;
using CoreDemoApp.ViewModels;

namespace CoreDemoApp.Infrastructure
{
  public class ViewModelServicesModule : Autofac.Module
  {
    protected override void Load(ContainerBuilder builder)
    {
      //builder.RegisterType<FileDialogProvider>().As<IFileDialogProvider>();
      //builder.RegisterType<DataFileReader>().As<IDataFileReader>();
      //builder.RegisterType<OokiiMessageDialogService>().As<IMessageDialogService>();
      //builder.RegisterType<DataCreator>().As<IDataCreator>();

      //builder.RegisterType<DataSetViewModel>().AsSelf().As<ISharedSettingsContext>().SingleInstance();
      builder.RegisterType<MainViewModel>().AsSelf();
      builder.RegisterType<DialogViewModel>().AsSelf();
    }
  }
}