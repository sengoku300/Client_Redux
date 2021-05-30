using Microsoft.Win32;
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

            this.DataContext = user;

            var hobbies = client.GetHobbies(user)?.Select(t => t.Hobbie);

            if (hobbies != null)
            {
                foreach (var item in hobbies)
                    textBlock_Hobbies.Text += item + ",";
            }

            GetAvatarka();
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
            PhotoGallery photoGallery = new PhotoGallery();

            photoGallery.ShowDialog();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e) => mainW.frame.Navigate(new EditUserPage(user, mainW));

        public void OnSendMessage(int chatid, Message message)
        {
            throw new NotImplementedException();
        }

        private void GetAvatarka()
        {
            byte[] arr = client.GetImage(user);

            BitmapImage bitmapImage = new BitmapImage();
            bitmapImage.BeginInit();
            bitmapImage.StreamSource = new MemoryStream(arr);
            bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
            bitmapImage.EndInit();

            user_photo.Source = bitmapImage;
        }

        private void user_photo_MouseDown(object sender, MouseButtonEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();

            openFileDialog.ShowDialog();

            if(openFileDialog.FileName != "")
            {
                byte[] bytes = File.ReadAllBytes(openFileDialog.FileName);
              
                client.SetAvatar(user, bytes);
                GetAvatarka();
            }
        }
    }
}
