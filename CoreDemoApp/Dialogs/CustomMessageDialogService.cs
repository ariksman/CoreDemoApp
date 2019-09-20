using System;
using System.Windows;

namespace CoreDemoApp.Dialogs
{
  public class CustomMessageDialogService : IMessageDialogService
  {
    public void ShowErrorMessage(string sender, string message, string details)
    {
      new YesNoDialog(message, details)
      {
        WindowStartupLocation = WindowStartupLocation.CenterOwner,
        Owner = App.Current.MainWindow
      }.ShowDialog();
    }

    public void ShowWarningMessage(string sender, string message, string details)
    {
      throw new NotImplementedException();
    }

    public void ShowUserMessage(string sender, string message)
    {
      throw new NotImplementedException();
    }

    public bool Confirm(string sender, string message, string details)
    {
      throw new NotImplementedException();
    }

    public void ShowException(string sender, Exception ex)
    {
      throw new NotImplementedException();
    }
  }
}