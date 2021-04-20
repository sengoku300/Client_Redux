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
using System.Text.RegularExpressions;
using WPF_Redux_Client.ServiceReference1;

namespace WPF_Redux_Client.Pages
{
    /// <summary>
    /// Interaction logic for RememberPassPage.xaml
    /// </summary>
    public partial class RememberPassPage : Page
    {
        Authorization authoriz { get => Application.Current.MainWindow as Authorization; }

        private int code;

        Service1Client client = new Service1Client();

        public RememberPassPage()
        {
            InitializeComponent();

            textBox_Code.email.MaxLength = 4;

            textBox_Password.IsEnabled = false;
            textBox_AcceptPassword.IsEnabled = false;

            textBox_Code.IsEnabled = false;
            Button_Accept_Code.IsEnabled = false;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (textBox_Password.passbox.Password == textBox_AcceptPassword.passbox.Password)
            {
                client.ChangePassword(textBox_Email.Text, textBox_Password.passbox.Password);

                MessageBox.Show("Пароль успешно изменён!");

                authoriz.authMain.Navigate(new Uri("Pages/LoginPage.xaml", UriKind.RelativeOrAbsolute));
                authoriz.Width = 700;
                authoriz.Height = 900;
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(textBox_Email.Text))
            {
                code = client.GetCode(textBox_Email.Text);

                if (code != 0)
                {
                    textBox_Email.IsEnabled = false;
                    Button_Code.IsEnabled = false;

                    textBox_Code.IsEnabled = true;
                    Button_Accept_Code.IsEnabled = true;

                    MessageBox.Show("Секретный код был отправлен вам на почту!");
                }
                else
                    MessageBox.Show("Данного аккаунта не существует.");
            }
        }
        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            if(textBox_Code.Text == code.ToString())
            {
                textBox_Code.IsEnabled = false;
                Button_Accept_Code.IsEnabled = false;

                textBox_Password.IsEnabled = true;
                textBox_AcceptPassword.IsEnabled = true;

                MessageBox.Show("Код верный! Подтверждения аккаунта успешно выполненно.");
            }
        }

        private void textBox_AcceptPassword_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if(textBox_AcceptPassword.passbox.Password != textBox_Password.passbox.Password)
            {
                textBlock_Warnings.Text = "Пароли не совпадают.";

                textBlock_Warnings.Foreground = Brushes.Red;

                Button_Change.IsEnabled = false;
            }
            else
            {
                textBlock_Warnings.Text = "Пароли совпадают!";

                textBlock_Warnings.Foreground = Brushes.Green;

                Button_Change.IsEnabled = true;

                e.Handled = true;
            }
        }

        private void textBox_Code_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
    }
}
