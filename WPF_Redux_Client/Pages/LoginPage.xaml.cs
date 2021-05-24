using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.ServiceModel;
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
    public partial class LoginPage : Page, IService1Callback
    {
        Service1Client client;

        public LoginPage()
        {
            InitializeComponent();

            if (File.Exists("log.txt"))
            {
                string[] account = File.ReadAllLines("log.txt");

                textBox_Email.Text = account[0];

                textBox_Email.FontSize = 20;

                textBox_Email.PlaceHolder = "";

                PassBox.passbox.Password = account[1];
                
                PassBox.FontSize = 20;

                PassBox.PlaceHolder = "";

                IService1Callback callback = this as IService1Callback;

                InstanceContext context = new InstanceContext(callback);

                client = new Service1Client(context);

                string login = client.GetName(account[0]);

                userName.Text = login;
            }
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
                if (client == null)
                {
                    IService1Callback callback = this as IService1Callback;

                    InstanceContext context = new InstanceContext(callback);

                    client = new Service1Client(context);
                }

                if (client.GetAccount(textBox_Email.Text, PassBox.passbox.Password, false))
                {
                    if (File.Exists("log.txt"))
                    {
                        if (!File.ReadAllText("log.txt").Contains(textBox_Email.Text))
                        {
                            File.WriteAllLines("log.txt", new string[] { textBox_Email.Text,
                            PassBox.passbox.Password});
                        }
                    }
                    else
                    {
                        File.WriteAllLines("log.txt", new string[] { textBox_Email.Text,
                            PassBox.passbox.Password});
                    }
                  
                    MainWindow mainWindow = new MainWindow();

                    mainWindow.email = textBox_Email.Text;

                    mainWindow.Show();

                    authoriz.Close();
                }
                else
                    MessageBox.Show("Ошибка! Неверный логин или пароль!");
            }
            else
                MessageBox.Show("Вы оставили какое-то из полей ввода пустым");
        }

        public void OnCallback()
        {
            throw new NotImplementedException();
        }

        public void OnSendMessage(string mes)
        {
            throw new NotImplementedException();
        }
    }
}
