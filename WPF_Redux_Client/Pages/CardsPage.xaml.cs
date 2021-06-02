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
using WPF_Redux_Client.CustomControls;
using WPF_Redux_Client.ServiceReference1;

namespace WPF_Redux_Client.Pages
{
    /// <summary>
    /// Interaction logic for CardsPage.xaml
    /// </summary>
    public partial class CardsPage : Page, IService1Callback
    {
        protected Point SwipeStart;

        private Service1Client client;

        private List<User> users = new List<User>();

        private User user1;

        public CardsPage()
        {
            InitializeComponent();

            IService1Callback callback = this as IService1Callback;

            InstanceContext context = new InstanceContext(callback);

            client = new Service1Client(context);
        }

        public CardsPage(User user)
        {
            this.user1 = user;

            InitializeComponent();

            IService1Callback callback = this as IService1Callback;

            InstanceContext context = new InstanceContext(callback);

            client = new Service1Client(context);
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

                    var who = ((UserCard)sender).user;

                    client.AddLike(user1, who);

                    items_control.Items.Remove((UserCard)sender);
                }

                if (SwipeStart != null && swipe.X < (SwipeStart.X - 200))
                {
                    MessageBox.Show("SwipeLeft");

                    items_control.Items.Remove((UserCard)sender);
                }
            }
        }

        private void One_MouseDown(object sender, MouseButtonEventArgs e) => SwipeStart = e.GetPosition(this);


        private void MergeControls(User user,
            double distance, List<BitmapImage> photos)
        {
            UserCard userCard = new UserCard();

            userCard.user = user;

            userCard.User_Name.Text = user.Name;
            userCard.User_LastName.Text = user.LastName;

            byte[] arr = client.GetImage(user);

            BitmapImage bitmapImage = new BitmapImage();
            bitmapImage.BeginInit();
            bitmapImage.StreamSource = new MemoryStream(arr);
            bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
            bitmapImage.EndInit();

            userCard.User_Image.ImageSource = bitmapImage;

            if (photos.Count != 0)
                userCard.photos = photos;

            userCard.User_Year.Text = GetAge(user.Birthday).ToString();
            userCard.User_Kilometer.Text = distance.ToString();

            userCard.UserControlLikeClicked += UserCard_UserControlLikeClicked;
            userCard.UserControlDLikeClicked += UserCard_UserControlDLikeClicked;
            userCard.UserControlFullClicked += UserCard_UserControlFullClicked;

            userCard.MouseDown += One_MouseDown;
            userCard.MouseMove += One_MouseMove;

            items_control.Items.Add(userCard);
        }

        private int GetAge(DateTime birthday)
        {
            // Save today date.
            var today = DateTime.Today;

            // Calculate the age.
            var age = today.Year - birthday.Year;

            // Go back to the year the person was born in case of a leap year
            if (birthday > today.AddYears(-age)) age--;

            return age;
        }

        private void UserCard_UserControlFullClicked(object sender, RoutedEventArgs e)
        {
            UserCard userCard = ((UserCard)sender);

            foreach (var item in users)
            {
                if (item.Name == userCard.User_Name.Text
                    && item.LastName == userCard.User_LastName.Text)
                {
                    UserFull userFull = new UserFull();

                    userFull.Width = 300;
                    userFull.User_Card.Image_User.Source = userCard.User_Image.ImageSource; 
                    userFull.VerticalAlignment = VerticalAlignment.Center;
                    userFull.HorizontalAlignment = HorizontalAlignment.Center;
                    userFull.Title = item.Name + " " + item.LastName + ", Город: " + item.City;
                    userFull.User_Card.User_Name.Text = item.Name;
                    userFull.User_Card.User_LastName.Text = item.LastName;
                    userFull.User_Card.user_city.Text += item.City;
                    userFull.User_Card.user_country.Text += item.Country;
                    userFull.User_Card.text_distance.Text = userCard.User_Kilometer.Text;

                    if (!string.IsNullOrEmpty(item.Description))
                        userFull.User_Card.user_description.Text = item.Description;

                    userFull.User_Card.User_Age.Text = GetAge(item.Birthday).ToString();

                    var hobbies = client.GetHobbies(item);

                    if (hobbies != null)
                    {
                        if (hobbies.Count() > 0)
                        {
                            foreach (var hobbie in hobbies)
                            {
                                InterestedBox interestedBox = new InterestedBox();

                                interestedBox.textBlock_Hobbies.Text = hobbie.Hobbie;

                                userFull.User_Card.user_hobbies.Children.Add(interestedBox);
                            }
                        }
                    }

                    userFull.ShowDialog();
                }
            }
        }

        private void UserCard_UserControlDLikeClicked(object sender, EventArgs e)
        {
            MessageBox.Show("Dislicked");

            items_control.Items.Remove((UserCard)sender);
        }

        private void UserCard_UserControlLikeClicked(object sender, EventArgs e)
        {
            MessageBox.Show("Like");

            UserCard card = (UserCard)sender;

            client.AddLike(user1, card.user);
            items_control.Items.Remove((UserCard)sender);
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
            if (items_control.Items.Count > 0) items_control.Items.Clear();

            if(users.Count() > 0) users.Clear();

            var get_users = client.FeedFilterUser(user1);

            if (client.FeedFilterUser(user1).Count() <= 0)
                get_users = client.DefaultFilter(user1);

            double user_lati = client.GetLatiTude(user1.Email);
            double user_long = client.GetLongiTude(user1.Email);

            List<BitmapImage> images = new List<BitmapImage>();

            foreach (var item in get_users)
            {
                var photos = client.GetPhotos(item);

                if (photos != null)
                {
                    foreach (var item2 in photos)
                    {
                        BitmapImage bitmap = new BitmapImage();
                        bitmap.BeginInit();
                        bitmap.StreamSource = new MemoryStream(item2);
                        bitmap.EndInit();
                        images.Add(bitmap);
                    }
                }


                double lati_ = client.GetLatiTude(item.Email);
                double long_ = client.GetLongiTude(item.Email);

                double distance = client.GetDistanceBetweenPoints(user_lati, user_long, lati_, long_);

                if (distance > 1000)
                    distance = distance / 1000;

                MergeControls(item, distance, images);

                users.Add(item);

                images.Clear();
            }
        }

        private void UserCard_UserControlLikeClicked_1(object sender, RoutedEventArgs e)
        {

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
