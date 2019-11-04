using System;
using System.Windows;

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
      _viewModel.Title = "Error happened!";
      _viewModel.Message = message + Environment.NewLine + details;

      new InfoDialog
      {
        WindowStartupLocation = WindowStartupLocation.CenterOwner,
        Owner = System.Windows.Application.Current.MainWindow,
        DataContext = _viewModel
      }.ShowDialog();
    }

    public void ShowWarningMessage(string sender, string message, string details)
    {
      throw new NotImplementedException();
    }

    public void ShowUserMessage(string sender, string message)
    {
      _viewModel.Title = "Status update";
      _viewModel.Message = message;

      new InfoDialog
      {
        WindowStartupLocation = WindowStartupLocation.CenterOwner,
        Owner = System.Windows.Application.Current.MainWindow,
        DataContext = _viewModel
      }.ShowDialog();
    }

    public bool Confirm(string sender, string message, string details)
    {
      _viewModel.Title = "Error happened!";
      _viewModel.Message = message;

      new YesNoDialog
      {
        WindowStartupLocation = WindowStartupLocation.CenterOwner,
        Owner = System.Windows.Application.Current.MainWindow,
        DataContext = _viewModel
      }.ShowDialog();

      return _viewModel.DialogResult;
    }

    public void ShowException(string sender, Exception ex)
    {
      _viewModel.Title = "Error";
      _viewModel.Message = ex.Message;

      new InfoDialog
      {
        WindowStartupLocation = WindowStartupLocation.CenterOwner,
        Owner = System.Windows.Application.Current.MainWindow,
        DataContext = _viewModel
      }.ShowDialog();
    }
  }
}