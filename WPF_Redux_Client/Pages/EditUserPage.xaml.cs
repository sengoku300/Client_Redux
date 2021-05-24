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
using WPF_Redux_Client.ServiceReference1;

namespace WPF_Redux_Client.Pages
{
    /// <summary>
    /// Interaction logic for EditUserPage.xaml
    /// </summary>
    public partial class EditUserPage : Page
    {
        private User user;

        MainWindow main { get => Application.Current.MainWindow as MainWindow; }

        public EditUserPage()
        {
            InitializeComponent();
        }

        public EditUserPage(User user)
        {
            this.user = user;

            InitializeComponent();

            this.Resources["user1"] = user;
        }

        private void Button_Save_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
