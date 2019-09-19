using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace CoreDemoApp.Views
{
  /// <summary>
  /// Interaction logic for InfoDialog.xaml
  /// </summary>
  public partial class InfoDialog : Window
  {
    public InfoDialog(string title, string message)
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
