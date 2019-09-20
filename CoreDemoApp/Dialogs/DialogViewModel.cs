using System;
using CoreDemoApp.Application;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;

namespace CoreDemoApp.Dialogs
{
  public class DialogViewModel : ViewModelBase
  {
    private string _title;
    public string Title
    {
      get => _title;
      set
      {
        _title = value;
        RaisePropertyChanged();
      }
    }

    private string _message;
    public string Message
    {
      get => _message;
      set
      {
        _message = value;
        RaisePropertyChanged();
      }
    }

    private bool _dialogResult;
    public bool DialogResult
    {
      get => _dialogResult;
      set
      {
        _dialogResult = value;
        RaisePropertyChanged();
      }
    }


    public DialogViewModel()
    {
    }

    private RelayCommand<IClosable> _closeWindowCommand;
    public RelayCommand<IClosable> CloseWindowCommand => _closeWindowCommand ??= new RelayCommand<IClosable>(CloseWindowCommandExecute);

    private void CloseWindowCommandExecute(IClosable window)
    {
      window?.Close();
    }

    private RelayCommand<IClosable> _clickYesCommand;
    public RelayCommand<IClosable> ClickYesCommand => _clickYesCommand ??= new RelayCommand<IClosable>(ClickYesCommandExecute);

    private void ClickYesCommandExecute(IClosable window)
    {
      DialogResult = true;
      window?.Close();
    }

    private RelayCommand<IClosable> _clickNoCommand;
    public RelayCommand<IClosable> ClickNoCommand => _clickNoCommand ??= new RelayCommand<IClosable>(ClickNoCommandExecute);

    private void ClickNoCommandExecute(IClosable window)
    {
      DialogResult = false;
      window?.Close();
    }
  }
}