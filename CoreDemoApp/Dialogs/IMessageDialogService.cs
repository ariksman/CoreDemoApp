using System;

namespace CoreDemoApp.Dialogs
{
  public interface IMessageDialogService
  {
    void ShowErrorMessage(string sender, string message, string details);
    void ShowWarningMessage(string sender, string message, string details);
    void ShowUserMessage(string sender, string message);
    bool Confirm(string sender, string message, string details);
    void ShowException(string sender, Exception ex);
  }
}
