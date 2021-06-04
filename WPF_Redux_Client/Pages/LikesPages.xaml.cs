using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Interaction logic for LikesPages.xaml
    /// </summary>
    public partial class LikesPages : Page, IService1Callback
    {
        Service1Client service;
        User You;
        public LikesPages(User current_user)
        {
            service = new Service1Client(new InstanceContext(this as IService1Callback));
            You = current_user;
            InitializeComponent();

            //listLikes.Items.Add(new LikedUser());
            //listLikes.Items.Add(new LikedUser());
            //listLikes.Items.Add(new LikedUser());
            //listLikes.Items.Add(new LikedUser());
            //listLikes.Items.Add(new LikedUser());
            //listLikes.Items.Add(new LikedUser());

            Initialize();
        }

        public void Initialize()
        {
            foreach (User User in service.GetUsersWhoLikedYouAsync(You).Result)
            {
                LikedUser lu = new LikedUser();
                lu.User = User;
                lu.user_city.Text = User.City;
                lu.user_year.Text = GetAge(User.Birthday);
                lu.user_name.Text = User.Name + " " + User.LastName;
				lu.LikeProfileEvent += Lu_LikeProfileEvent;
				lu.DislikeProfileEvent += Lu_DislikeProfileEvent;
				lu.BanProfileEvent += Lu_BanProfileEvent;
				lu.OpenProfileEvent += Lu_OpenProfileEvent;
                lu.Number = listLikes.Children.Count;
                lu.ImagePath = service.GetImageAsync(User).Result;
                listLikes.Children.Add(lu);
            }
        }

        private BitmapImage ImageFromByte(byte[] image)
		{
            BitmapImage bitmap = new BitmapImage();
            bitmap.BeginInit();
            bitmap.StreamSource = new MemoryStream(image);
            bitmap.EndInit();
            return bitmap;
        }

		private void Lu_OpenProfileEvent(LikedUser sender)
		{
            if (!Dispatcher.CheckAccess()) Dispatcher.Invoke(() => Lu_OpenProfileEvent(sender));
            else
            {
                List<Image> images = new List<Image>();
                byte[] MainPhoto = service.GetImage(sender.User);
                images.Add(new Image { Source = ImageFromByte(MainPhoto) });
                byte[][] Photos = service.GetPhotosAsync(sender.User).Result;
                if (Photos?.Length > 0)
                    foreach (byte[] image in service.GetPhotos(sender.User))
                    {
                        if (image.SequenceEqual(MainPhoto)) break;
                        images.Add(new Image { Source = ImageFromByte(image) });
                    }
                List<Hobbies> hobbies = service.GetHobbies(sender.User)?.ToList();
                if (hobbies == null) hobbies = new List<Hobbies>();
                double distance = service.GetDistanceBetweenPoints(You.LatiTude, You.LongiTude, sender.User.LatiTude, sender.User.LongiTude);
                try
                {
                    FullProfile.PlaceAllItems(images, sender.User, distance, hobbies);
                    FullProfile.Opacity = 1;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

		private void Lu_BanProfileEvent(LikedUser sender)
		{
            if (!Dispatcher.CheckAccess()) Dispatcher.Invoke(() => Lu_BanProfileEvent(sender));
            else
            {
                if (You != null && sender.User != null)
                    service.BanUser(You.UserId, sender.User.UserId);
                listLikes.Children.Remove(sender);
            }

        }

		private void Lu_DislikeProfileEvent(LikedUser sender)
		{
            listLikes.Children.Remove(sender);
        }

        private void Lu_LikeProfileEvent(LikedUser sender)
        {
            if (!Dispatcher.CheckAccess()) Dispatcher.Invoke(() => Lu_LikeProfileEvent(sender));

            else
            {
                try
                {
                    if (You != null && sender?.User != null) service.AddLike(sender.User, You);
                }
                catch (Exception ex) { MessageBox.Show(ex.Message); }

                foreach (var item in listLikes.Children)
                {
                    if (item is LikedUser)
                    {
                        if ((item as LikedUser).User.UserId == sender.User.UserId)
                        {
                            listLikes.Children.Remove(item as LikedUser);
                            break;
                        }
                    }
                }
            }
        }

		public string GetAge(DateTime birthday)
		{
            if (birthday.DayOfYear < DateTime.Now.DayOfYear) return $"{DateTime.Now.Year - birthday.Year} лет";
            else return $"{DateTime.Now.Year - birthday.Year + 1} лет";
        }

		public void OnCallback()
		{
		}

		public void OnSendMessage(int chatid, Message message)
		{
		}

        public void OnSendMessage(string mes)
        {

        }
    }
}
