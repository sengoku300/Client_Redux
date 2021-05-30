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
using WPF_Redux_Client.Pages;
using System.Windows.Markup;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using WPF_Redux_Client.ServiceReference1;
using System.IO;

namespace WPF_Redux_Client.CustomControls
{
    /// <summary>
    /// Interaction logic for UserCard.xaml
    /// </summary>
    public partial class UserCard : UserControl
    {
        public event RoutedEventHandler UserControlLikeClicked;

        public event RoutedEventHandler UserControlDLikeClicked;

        public event RoutedEventHandler UserControlFullClicked;

        public List<BitmapImage> photos = new List<BitmapImage>();

        public User user;

        private int count = 1;

        public UserCard()
        {
            InitializeComponent();
        } 
        

        private void like_Click(object sender, RoutedEventArgs e)
        {
            if (UserControlLikeClicked != null)
            {
                UserControlLikeClicked(this, new RoutedEventArgs());
            }
        }

        private void close_Click(object sender, RoutedEventArgs e)
        {
            if (UserControlDLikeClicked != null)
            {
                UserControlDLikeClicked(this, new RoutedEventArgs());
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if(UserControlFullClicked != null)
            {
                UserControlFullClicked(this, new RoutedEventArgs());
            }
        }

        // вправо
        private void PackIcon_MouseDown(object sender, MouseButtonEventArgs e)
        {
            count++;

            if (count > photos.Count)
            {
                count = 1;
            }

            for (int i = 1; i < photos.Count; i++)
            {
                if (count == i)
                {
                    User_Image.ImageSource = photos[i];
                }
            }
        }

        // влево
        private void PackIcon_MouseDown_1(object sender, MouseButtonEventArgs e)
        {
            count--;

            if (count < 1)
            {
                count = photos.Count;
            }

            for (int i = 1; i < photos.Count; i++)
            {
                if(count == i)
                {
                    User_Image.ImageSource = photos[i];
                }
            }
        }
    }

   
}
