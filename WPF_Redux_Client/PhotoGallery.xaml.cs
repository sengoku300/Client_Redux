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
using System.Windows.Shapes;
using WPF_Redux_Client.ServiceReference1;

namespace WPF_Redux_Client
{
    /// <summary>
    /// Interaction logic for PhotoGallery.xaml
    /// </summary>
    public partial class PhotoGallery : Window
    {
        private Service1Client client;

        private User user;

        List<BitmapImage> images;

        public PhotoGallery()
        {
            InitializeComponent();
        }

        public PhotoGallery(User user)
        {
            InitializeComponent();

            this.user = user;

            IService1Callback callback = this as IService1Callback;

            InstanceContext context = new InstanceContext(callback);

            client = new Service1Client(context);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void MenuItemAvatar_Click(object sender, RoutedEventArgs e)
        {

        }

        private void MenuItemReplace_Click(object sender, RoutedEventArgs e)
        {

        }

        private void MenuItemDelete_Click(object sender, RoutedEventArgs e)
        {
            if(listBox_Photos.SelectedItem != null)
            {
              
            }
        }
    }
}
