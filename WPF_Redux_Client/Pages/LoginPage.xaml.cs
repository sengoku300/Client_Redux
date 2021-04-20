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
    /// Interaction logic for LoginPage.xaml
    /// </summary>
    public partial class LoginPage : Page
    {
        Service1Client client;

        public LoginPage()
        {
            InitializeComponent();
        }

        Authorization authoriz { get => Application.Current.MainWindow as Authorization; }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            authoriz.authMain.Navigate(new Uri("Pages/RegPage.xaml", UriKind.RelativeOrAbsolute));
            authoriz.Width = 500;
            authoriz.Height = 830;
        }

        private void TextBlock_MouseDown_1(object sender, MouseButtonEventArgs e)
        {
            if(e.ClickCount == 2)
            {
                authoriz.authMain.Navigate(new Uri("Pages/RememberPassPage.xaml", UriKind.RelativeOrAbsolute));
                authoriz.Height = 600;
                authoriz.Width = 500;
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (textBox_Email.Text != "" && PassBox.passbox.Password != "")
            {
                client = new Service1Client();

                if (client.GetAccount(textBox_Email.Text, PassBox.passbox.Password, false))
                {
                    MainWindow mainWindow = new MainWindow();

                    mainWindow.Show();

                    authoriz.Close();
                }
                else
                    MessageBox.Show("Ошибка! Неверный логин или пароль!");
            }
            else
                MessageBox.Show("Вы оставили какое-то из полей ввода пустым");
        }
    }
}
