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

        public PhotoGallery()
        {
            InitializeComponent();
        }

        public PhotoGallery(List<Image> images)
        {
            InitializeComponent();

            listBox_Photos.ItemsSource = images;

            IService1Callback callback = this as IService1Callback;

            InstanceContext context = new InstanceContext(callback);

            client = new Service1Client(context);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
