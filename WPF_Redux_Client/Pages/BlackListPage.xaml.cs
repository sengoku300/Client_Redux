using System;
using System.Collections.Generic;
using System.IO;
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
using System.Windows.Threading;
using WPF_Redux_Client.CustomControls;
using WPF_Redux_Client.ServiceReference1;

namespace WPF_Redux_Client.Pages
{
    /// <summary>
    /// Interaction logic for BlackListPage.xaml
    /// </summary>
    public partial class BlackListPage : Page, ServiceReference1.IService1Callback
    {
        ServiceReference1.Service1Client service;
        User You;
        public BlackListPage(User user)
        {
                      InitializeComponent(); service = new ServiceReference1.Service1Client(new System.ServiceModel.InstanceContext(this as ServiceReference1.IService1Callback));
            You = user;


            //blacklist.Items.Add(new BlackListItem());
            //blacklist.Items.Add(new BlackListItem());
            //blacklist.Items.Add(new BlackListItem());
            //blacklist.Items.Add(new BlackListItem());

            //blacklist.Items.Add(new BlackListItem());
            //blacklist.Items.Add(new BlackListItem());
            //blacklist.Items.Add(new BlackListItem());
            //blacklist.Items.Add(new BlackListItem());
            //blacklist.Items.Add(new BlackListItem());
            //blacklist.Items.Add(new BlackListItem());
            //blacklist.Items.Add(new BlackListItem());
            //blacklist.Items.Add(new BlackListItem());
            //blacklist.Items.Add(new BlackListItem());
            //blacklist.Items.Add(new BlackListItem());
            //blacklist.Items.Add(new BlackListItem());
            //blacklist.Items.Add(new BlackListItem());
            //blacklist.Items.Add(new BlackListItem());
            //blacklist.Items.Add(new BlackListItem());
            //blacklist.Items.Add(new BlackListItem());
            //blacklist.Items.Add(new BlackListItem());
            //blacklist.Items.Add(new BlackListItem());
            //blacklist.Items.Add(new BlackListItem());
            //blacklist.Items.Add(new BlackListItem());
            //blacklist.Items.Add(new BlackListItem());
            //blacklist.Items.Add(new BlackListItem());
            //blacklist.Items.Add(new BlackListItem());
            //blacklist.Items.Add(new BlackListItem());
            //blacklist.Items.Add(new BlackListItem());


        }

        public void Initialize()
		{
            foreach (var user in service.GetUsersWhoWasBannedByYou(You))
			{
                var bi = new BlackListItem();
                bi.User = user;
                bi.user_city.Text = user.City;
                bi.user_year.Text = GetAge(user.Birthday);
				bi.UnbanEvent += Bi_UnbanEvent;
                bi.ImagePath = service.GetImage(user);
                bi.OpenProfileEvent += Bi_OpenProfileEvent;
                blacklist.Items.Add(bi);
            }
		}

        private void Bi_OpenProfileEvent(BlackListItem sender)
        {
            List<Image> images = new List<Image>();
            foreach (byte[] image in service.GetPhotos(sender.User))
            {
                BitmapImage bitmap = new BitmapImage();
                bitmap.BeginInit();
                bitmap.StreamSource = new MemoryStream(image);
                bitmap.EndInit();
                images.Add(new Image { Source = bitmap });
            }

            FullProfile = new ProfileControl(images, sender.User, service.GetDistanceBetweenPoints(You.LatiTude, You.LongiTude, sender.User.LatiTude, sender.User.LongiTude));
            FullProfile.Opacity = 0;
        }

        private async void Bi_UnbanEvent(BlackListItem sender)
		{
            if (!Dispatcher.CheckAccess()) Dispatcher.Invoke(() => Bi_UnbanEvent(sender));
            else
			{
                await service.UnbanUserAsync(You.UserId, sender.User.UserId);
                await service.AddLikeAsync(You, sender.User);
                var Timer = new DispatcherTimer();
                Timer.Interval = new TimeSpan(0, 0, 0, 0, 20);
				Timer.Tick += Timer_Tick;
                Timer.Tag = sender;
                Timer.Start();
			}
		}

		private void Timer_Tick(object sender, EventArgs e)
		{
            blacklist.Items.Remove(((DispatcherTimer)sender).Tag as BlackListItem);
            ((DispatcherTimer)sender).Stop();
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
	}
}
