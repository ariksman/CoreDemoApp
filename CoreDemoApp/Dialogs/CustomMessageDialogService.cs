using System;
using System.Windows;
using System.Windows.Input;

namespace CoreDemoApp.Dialogs
{
  public class CustomMessageDialogService : IMessageDialogService
  {
    private readonly DialogViewModel _viewModel;

    public CustomMessageDialogService(DialogViewModel viewModel)
    {
      _viewModel = viewModel;
    }

    public void ShowErrorMessage(string sender, string message, string details)
    {
      new YesNoDialog(message, details)
      {
        WindowStartupLocation = WindowStartupLocation.CenterOwner,
        Owner = System.Windows.Application.Current.MainWindow,
        DataContext = _viewModel,
      }.ShowDialog();
    }

    public void ShowWarningMessage(string sender, string message, string details)
    {
      throw new NotImplementedException();
    }

    public void ShowUserMessage(string sender, string message)
    {
      _viewModel.Message = message;
      new InfoDialog("Status update")
      {
        WindowStartupLocation = WindowStartupLocation.CenterOwner,
        Owner = System.Windows.Application.Current.MainWindow,
        DataContext = _viewModel,
      }.ShowDialog();
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

  public interface IDialogViewModel
  {
    ICommand CloseWindowCommand();
  }
}