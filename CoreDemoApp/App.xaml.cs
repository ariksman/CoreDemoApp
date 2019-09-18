using System;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;
using CoreDemoApp.Views;

namespace CoreDemoApp
{
  /// <summary>
  /// Interaction logic for App.xaml
  /// </summary>
  public partial class App : System.Windows.Application
  {
    private void App_OnStartup(object sender, StartupEventArgs e)
    {
      PresentationTraceSources.Refresh();
      PresentationTraceSources.DataBindingSource.Listeners.Add(new ConsoleTraceListener());
      PresentationTraceSources.DataBindingSource.Listeners.Add(new DebugTraceListener());

      AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
      App.Current.DispatcherUnhandledException += Current_DispatcherUnhandledException;
      TaskScheduler.UnobservedTaskException += TaskScheduler_UnobservedTaskException;

      var mainWindow = new MainWindow();
      mainWindow.Show();
    }

    private void TaskScheduler_UnobservedTaskException(object sender, UnobservedTaskExceptionEventArgs e)
    {
      throw new NotImplementedException();
    }

    private void Current_DispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
    {
      throw new NotImplementedException();
    }

    private void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
    {
      throw new NotImplementedException();
    }

  }
}
