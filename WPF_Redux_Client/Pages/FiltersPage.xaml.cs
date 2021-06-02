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
using WPF_Redux_Client.CustomControls;
using WPF_Redux_Client.ServiceReference1;

namespace WPF_Redux_Client.Pages
{
    /// <summary>
    /// Interaction logic for FiltersPage.xaml
    /// </summary>
    public partial class FiltersPage : Page, IService1Callback
    {
        private User user;

        private Service1Client client;

        public FiltersPage(User user)
        {
            InitializeComponent();

            IService1Callback callback = this as IService1Callback;

            InstanceContext context = new InstanceContext(callback);

            client = new Service1Client(context);

            this.user = user;
        }

        public void OnCallback()
        {
            throw new NotImplementedException();
        }

        public void OnSendMessage(int chatid, Message message)
        {
            throw new NotImplementedException();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (filter1.comboBox_Eye.Text != ""
                && filter1.comboBox_Haircut.Text != ""
                && filter1.slider_distance.Value > 0
                && filter1.slider_Height.Value > 0
                && filter1.slider_MinAge.Value > 0
                && filter1.slider_MaxAge.Value > 0)
            {
                int dist = Convert.ToInt32(filter1.slider_distance.Value);
                int height = Convert.ToInt32(filter1.slider_Height.Value);
                int min_age = Convert.ToInt32(filter1.slider_MinAge.Value);
                int max_age = Convert.ToInt32(filter1.slider_MaxAge.Value);

                client.AddFilter(user, dist,
                    filter1.comboBox_Haircut.Text, filter1.comboBox_Eye.Text,
                    height, min_age, max_age);
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (client.IsExistsFilter(user))
                client.SetDefaultFilter(user);
        }
    }
}
