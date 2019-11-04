using CoreDemoApp.Application;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;

namespace CoreDemoApp.Dialogs
{
  public class DialogViewModel : ViewModelBase
  {
    private RelayCommand<IClosable> _clickNoCommand;

    private RelayCommand<IClosable> _clickYesCommand;

    private RelayCommand<IClosable> _closeWindowCommand;

    private bool _dialogResult;

    private string _message;
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

    public string Message
    {
      get => _message;
      set
      {
        _message = value;
        RaisePropertyChanged();
      }
    }

    public bool DialogResult
    {
      get => _dialogResult;
      set
      {
        _dialogResult = value;
        RaisePropertyChanged();
      }
    }

    public RelayCommand<IClosable> CloseWindowCommand =>
      _closeWindowCommand ??= new RelayCommand<IClosable>(CloseWindowCommandExecute);

    public RelayCommand<IClosable> ClickYesCommand =>
      _clickYesCommand ??= new RelayCommand<IClosable>(ClickYesCommandExecute);

    public RelayCommand<IClosable> ClickNoCommand =>
      _clickNoCommand ??= new RelayCommand<IClosable>(ClickNoCommandExecute);

    private void CloseWindowCommandExecute(IClosable window)
    {
      window?.Close();
    }

    private void ClickYesCommandExecute(IClosable window)
    {
      DialogResult = true;
      window?.Close();
    }

    private void ClickNoCommandExecute(IClosable window)
    {
      DialogResult = false;
      window?.Close();
    }
  }
}