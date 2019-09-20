using CoreDemoApp.Application;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;

namespace CoreDemoApp.Dialogs
{
  public class DialogViewModel : ViewModelBase
  {
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

    public DialogViewModel()
    {
    }

    private RelayCommand<IClosable> _closeWindowCommand;
    public RelayCommand<IClosable> CloseWindowCommand => _closeWindowCommand ??= new RelayCommand<IClosable>(CloseWindowCommandExecute);

    private void CloseWindowCommandExecute(IClosable window)
    {
      window?.Close();
    }
  }
}