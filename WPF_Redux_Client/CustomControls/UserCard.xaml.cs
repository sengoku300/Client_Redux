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

namespace WPF_Redux_Client.CustomControls
{
    /// <summary>
    /// Interaction logic for UserCard.xaml
    /// </summary>
    public partial class UserCard : UserControl
    {
        public event RoutedEventHandler UserControlLikeClicked;

        public event RoutedEventHandler UserControlDLikeClicked;

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
    }

   
}
