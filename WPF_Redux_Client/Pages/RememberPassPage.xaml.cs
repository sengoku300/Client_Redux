using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WPF_Redux_Client.Pages
{
    /// <summary>
    /// Interaction logic for RememberPassPage.xaml
    /// </summary>
    public partial class RememberPassPage : Page
    {
        Authorization authoriz { get => Application.Current.MainWindow as Authorization; }

        public RememberPassPage()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            authoriz.authMain.Navigate(new Uri("Pages/LoginPage.xaml", UriKind.RelativeOrAbsolute));
        }
    }
}
