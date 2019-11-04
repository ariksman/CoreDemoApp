using System.Windows;
using CoreDemoApp.Application;

namespace CoreDemoApp.Dialogs
{
  /// <summary>
  ///   Interaction logic for InfoDialog.xaml
  /// </summary>
  public partial class YesNoDialog : Window, IClosable
  {
    public YesNoDialog()
    {
      InitializeComponent();
    }

    private void ButtonYes_Click(object sender, RoutedEventArgs e)
    {
      DialogResult = true;
    }

    private void ButtonNo_Click(object sender, RoutedEventArgs e)
    {
      DialogResult = false;
    }
  }
}