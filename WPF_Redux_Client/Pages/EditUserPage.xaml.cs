using System;
using System.Collections.Generic;
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
    /// Interaction logic for EditUserPage.xaml
    /// </summary>
    public partial class EditUserPage : Page, IService1Callback
    {
        private User user;
        
        private Service1Client client;

        private MainWindow main;

        public EditUserPage()
        {
            InitializeComponent();

            IService1Callback callback = this as IService1Callback;

            InstanceContext context = new InstanceContext(callback);

            client = new Service1Client(context);
        }

        public EditUserPage(User user, MainWindow mainWindow)
        {
            InitializeComponent();

            IService1Callback callback = this as IService1Callback;

            InstanceContext context = new InstanceContext(callback);

            client = new Service1Client(context);

            this.user = user;

            main = mainWindow;

            this.DataContext = this.user;

            if (client.IsExistsHobbies(user))
            {
                var hbs = client.GetHobbies(user)
                    .Select(t => t.Hobbie);

                foreach (var item in hbs)
                    textBox_hobbies.Text += item + ",";
            }
        }

        private void Button_Save_Click(object sender, RoutedEventArgs e)
        {
            if(textBox_FirstName.Text != ""
                && textBox_LastName.Text != ""
                && DatePicker_Birthday.Text != "")
            {
                client.UpdateUser(textBox_FirstName.Text, textBox_LastName.Text,
                    DatePicker_Birthday.DisplayDate,
                    comboBox_ColorEye.Text, combobox_colorhaircut.Text,
                    combobox_faith.Text, combobox_gender.Text,
                    textBox_job.Text,
                    textBox_Descriptions.Text,
                    textBox_Education.Text, textBox_hobbies.Text.Split(','), user);

                MessageBox.Show("Аккаунт успешно изменнён!");

                main.frame.GoBack();
            }
        }

        public void OnCallback()
        {
            throw new NotImplementedException();
        }

        public void OnSendMessage(string mes)
        {
            throw new NotImplementedException();
        }

        public void OnSendMessage(int chatid, Message message)
        {
            throw new NotImplementedException();
        }
    }
}
