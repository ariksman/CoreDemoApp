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
using CoreDemoApp.Application;

namespace CoreDemoApp.Dialogs
{
  /// <summary>
  /// Interaction logic for InfoDialog.xaml
  /// </summary>
  public partial class InfoDialog : Window, IClosable
  {
    public InfoDialog()
    {
      InitializeComponent();
    }
  }
}
