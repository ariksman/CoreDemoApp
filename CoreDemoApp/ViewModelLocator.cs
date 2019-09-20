using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Autofac;
using CoreDemoApp.Infrastructure;
using CoreDemoApp.Views.MainWindow;

namespace CoreDemoApp
{
  /// <summary>
  /// This class contains static references to all the view models in the
  /// application and provides an entry point for the bindings.
  /// </summary>
  public class ViewModelLocator
  {

    private static ViewModelLocator _instance;
    private IContainer _container;

    private ILifetimeScope _mainViewModelLifetimeScope;

    /// <summary>
    /// Initializes a new instance of the ViewModelLocator class and register all classes for DI-container.
    /// Additionally, registers all profiles for Auto mapper.
    /// </summary>
    public ViewModelLocator()
    {
      RegisterServices();
    }

    public static ViewModelLocator Instance => _instance ??= new ViewModelLocator();

    #region autofac registration

    private void RegisterServices()
    {
      var builder = new ContainerBuilder();
      var assemblies = GetAllProgramAssemblies().ToList();

      builder.RegisterModule(new HandlerAutoFacModule(assemblies));
      builder.RegisterModule(new AutoMapperModule(assemblies));
      builder.RegisterModule<RepositoryModule>();
      builder.RegisterModule<ServicesModule>();
      builder.RegisterModule<ViewModelServicesModule>();

      _container = builder.Build();
    }

    private static IEnumerable<Assembly> GetAllProgramAssemblies()
    {
      return new List<Assembly>()
      {
        Assembly.Load("CoreDemoApp.Application"),
        Assembly.Load("CoreDemoApp.Domain"),
        Assembly.Load("CoreDemoApp"),
      };
    }

    #endregion

    #region ViewModel properties

    public MainViewModel MainViewModel
    {
      get
      {
        _mainViewModelLifetimeScope = _container.BeginLifetimeScope();
        return _mainViewModelLifetimeScope.Resolve<MainViewModel>();
      }
    }

    #endregion
  }
}