using System;
using System.Collections.Generic;
using System.Text;

namespace CoreDemoApp.Application.Interfaces
{
  public interface IMessageDialogService
  {
    void ShowErrorMessage(string sender, string message, string details);
    void ShowWarningMessage(string sender, string message, string details);
    void ShowUserMessage(string sender, string message, Exception ex);
    bool Confirm(string sender, string message, string details);
    void ShowException(string sender, Exception ex);
  }
}
