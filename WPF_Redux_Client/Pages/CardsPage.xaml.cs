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
using WPF_Redux_Client.CustomControls;
using WPF_Redux_Client.ServiceReference1;

namespace WPF_Redux_Client.Pages
{
    /// <summary>
    /// Interaction logic for CardsPage.xaml
    /// </summary>
    public partial class CardsPage : Page
    {
        protected Point SwipeStart;

        private Service1Client client;

        private string email { get; set; }

        public CardsPage()
        {
            InitializeComponent();
        }

        public CardsPage(string email)
        {
            this.email = email;

            InitializeComponent();
        }

        private void Like_Click(object sender, RoutedEventArgs e)
        {
            items_control.Items.RemoveAt(items_control.Items.Count - 1);
        }

        private void One_MouseMove(object sender, MouseEventArgs e)
        {
            if(e.LeftButton == MouseButtonState.Pressed)
            {
                var swipe = e.GetPosition(this);

                if (SwipeStart != null && swipe.X > (SwipeStart.X + 200))
                {
                    MessageBox.Show("SwipeRight");
                    items_control.Items.Remove((UserCard)sender);
                }

                if (SwipeStart != null && swipe.X < (SwipeStart.X - 200))
                {
                    MessageBox.Show("SwipeLeft");

                }
            }
        }

        private void One_MouseDown(object sender, MouseButtonEventArgs e) => SwipeStart = e.GetPosition(this);


        private void MergeControls(string name, string lastname,
            double distance, List<ImageBrush> photos)
        {
            UserCard userCard = new UserCard();

            userCard.photos = photos;
            userCard.User_Name.Text = name;
            userCard.User_LastName.Text = lastname;
            userCard.User_Kilometer.Text = distance.ToString();


            userCard.UserControlLikeClicked += UserCard_UserControlLikeClicked;
            userCard.UserControlDLikeClicked += UserCard_UserControlDLikeClicked;
            userCard.UserControlFullClicked += UserCard_UserControlFullClicked;

            userCard.MouseDown += One_MouseDown;
            userCard.MouseMove += One_MouseMove;

            items_control.Items.Add(userCard);
        }

        private void UserCard_UserControlFullClicked(object sender, RoutedEventArgs e)
        {
            UserFull userFull = new UserFull();
            userFull.ShowDialog();
        }

        private void UserCard_UserControlDLikeClicked(object sender, EventArgs e)
        {
            MessageBox.Show("Dislicked");
        }

        private void UserCard_UserControlLikeClicked(object sender, EventArgs e)
        {
            MessageBox.Show("Like");
        }

        private void Page_MouseDown(object sender, MouseButtonEventArgs e)
        {
            SwipeStart = e.GetPosition(this);
        }

        private void Page_MouseMove(object sender, MouseEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            client = new Service1Client();

            var get_users = client.DefaultFilter(email);

            double user_lati = client.GetLatiTude(email);
            double user_long = client.GetLongiTude(email);

            List<ImageBrush> images = new List<ImageBrush>();

            foreach (var item in get_users)
            {
                if (item.Photos != null)
                {
                    foreach (var pictures in item.Photos)
                    {
                        images.Add(new ImageBrush(new BitmapImage(
                            new Uri(pictures, UriKind.Relative))));
                    }
                }

                    double lati_ = client.GetLatiTude(item.Email);
                    double long_ = client.GetLongiTude(item.Email);

                    double distance = client.GetDistanceBetweenPoints(user_lati, user_long, lati_, long_);

                    if (distance > 1000)
                        distance = distance / 1000;

                    MergeControls(item.Name, item.LastName, distance, images);

                    images.Clear();
            }
        }

        private void UserCard_UserControlLikeClicked_1(object sender, RoutedEventArgs e)
        {

        }
    }
}
