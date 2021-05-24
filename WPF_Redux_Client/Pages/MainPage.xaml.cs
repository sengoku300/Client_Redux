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
    /// Interaction logic for MainPage.xaml
    /// </summary>
    public partial class MainPage : Page, IService1Callback
    {
        private User user;

        private Service1Client client;

        public MainWindow mainW;

        public MainPage()
        {
            InitializeComponent();

            IService1Callback callback = this as IService1Callback;

            InstanceContext context = new InstanceContext(callback);

            client = new Service1Client(context);
        }
        
        public MainPage(User user, MainWindow main)
        {
            InitializeComponent();

            IService1Callback callback = this as IService1Callback;

            InstanceContext context = new InstanceContext(callback);

            client = new Service1Client(context);

            mainW = main;

            this.user = user;

            textBox_Name.Text = user.Name + " " + user.LastName;
            run_descriptions.Text = user.Description;
            run_birthday.Text = user.Birthday.ToString("dd/MM/yyyy");
            run_eduction.Text = user.Education;
            run_city.Text = user.City;
            run_country.Text = user.Country;

            if (client.GetImage(user) != null)
                user_photo.Source = client.GetImage(user).ImageSource;
        }

        public void OnCallback()
        {
            throw new NotImplementedException();
        }

        public void OnSendMessage(string mes)
        {
            throw new NotImplementedException();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click_1(object sender, RoutedEventArgs e) => mainW.frame.Navigate(new EditUserPage(user));
    }
}
