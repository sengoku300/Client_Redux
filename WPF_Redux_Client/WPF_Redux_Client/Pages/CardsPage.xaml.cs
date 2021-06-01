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
    public partial class CardsPage : Page,IService1Callback
    {
        protected Point SwipeStart;

        private Service1Client client;

        private List<User> users = new List<User>();

        private string email { get; set; }

        public CardsPage()
        {
            InitializeComponent();

            IService1Callback callback = this as IService1Callback;

            InstanceContext context = new InstanceContext(callback);

            client = new Service1Client(context);
        }

        public CardsPage(string email)
        {
            this.email = email;

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
                var user = (UserCard)sender;

                if (SwipeStart != null && swipe.X > (SwipeStart.X + 200))
                {
                    MessageBox.Show("SwipeRight");

                    var u = client.GetUser(email);

                    var who = ((UserCard)sender).user;

                    client.AddLike(u, who);

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
            double distance, List<ImageBrush> photos)
        {
            UserCard userCard = new UserCard();

            userCard.user = user;

            userCard.User_Name.Text = user.Name;
            userCard.User_LastName.Text = user.LastName;

            if(File.Exists(user.Avatarka))
            {
                userCard.User_Image.ImageSource =
                    new BitmapImage(new Uri(user.Avatarka, UriKind.RelativeOrAbsolute));
            }

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
            IService1Callback callback = this as IService1Callback;

            InstanceContext context = new InstanceContext(callback);

            client = new Service1Client(context);

            if (items_control.Items.Count > 0) items_control.Items.Clear();

            if(users.Count() > 0) users.Clear();

            var get_users = client.DefaultFilter(email);

            double user_lati = client.GetLatiTude(email);
            double user_long = client.GetLongiTude(email);

            List<ImageBrush> images = new List<ImageBrush>();

            foreach (var item in get_users)
            {
                var photos = client.GetPhotos(item);

                if(photos != null)
                {
                    foreach (var pic in photos)
                    {
                        ImageBrush image = new ImageBrush(new BitmapImage
                            (new Uri(pic.Photo, UriKind.RelativeOrAbsolute)));

                           images.Add(image);
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

        public void OnSendMessage(int chatid, Message message)
        {
            throw new NotImplementedException();
        }
    }
}
