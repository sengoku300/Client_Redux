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

namespace WPF_Redux_Client.Pages
{
    /// <summary>
    /// Interaction logic for CardsPage.xaml
    /// </summary>
    public partial class CardsPage : Page
    {
        protected Point SwipeStart;

        public CardsPage()
        {
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


        private void MergeControls(string name, string lastname, string city,
            int distance)
        {
            UserCard userCard = new UserCard();

            userCard.User_Name.Text = name;
            userCard.User_LastName.Text = lastname;
            userCard.User_City.Text = city;
            userCard.User_Kilometer.Text = distance.ToString();


            userCard.UserControlLikeClicked += UserCard_UserControlLikeClicked;
            userCard.UserControlDLikeClicked += UserCard_UserControlDLikeClicked;

            userCard.MouseDown += One_MouseDown;
            userCard.MouseMove += One_MouseMove;

            items_control.Items.Add(userCard);
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

        private void Button_Click(object sender, RoutedEventArgs e) => MergeControls("Playboi","Carti", "NY", 10);

        private void UserCard_UserControlLikeClicked_1(object sender, RoutedEventArgs e)
        {

        }
    }
}
