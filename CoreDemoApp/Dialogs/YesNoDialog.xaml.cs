using System.Windows;

namespace CoreDemoApp.Dialogs
{
  /// <summary>
  /// Interaction logic for InfoDialog.xaml
  /// </summary>
  public partial class YesNoDialog : Window
  {
    public YesNoDialog(string title, string message)
    {
      InitializeComponent();
      Title = title;
      textBlock.Text = message;
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
