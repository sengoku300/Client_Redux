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

namespace WPF_Redux_Client.CustomControls
{
	/// <summary>
	/// Interaction logic for ProfileControl.xaml
	/// </summary>
	public partial class ProfileControl : UserControl
	{
		List<Image> Images = new List<Image>();
		int CurrentPhoto = 0;

		public ProfileControl()
		{
			InitializeComponent();
		}
		public ProfileControl(List<Image> ImageList, ServiceReference1.User User, double Distance, List<ServiceReference1.Hobbies> Hobbies)
		{
			Images = ImageList;
			if (Images.Count < 2)
            {
				Arrow1.Visibility = Visibility.Collapsed;
				Arrow2.Visibility = Visibility.Collapsed;
				Button_right.Visibility = Visibility.Collapsed;
				Button_left.Visibility = Visibility.Collapsed;	
            }
			InitializeComponent();
			textBox_Name.Text = User.Name + " " + User.LastName;
			run_distance.Text = GetDistance(Distance);
			if (User.Job != null) run_job.Text = User.Job;
			if (User.ColorEye != null) run_ColorEye.Text += User.ColorEye; else run_ColorEye.Text += " не указан";
			if (User.ColorHairCut != null) run_ColorHaircut.Text += User.ColorHairCut; else run_ColorHaircut.Text += " не указан";
			if (User.Gender != null) run_gender.Text += User.Gender; else run_gender.Text += " не указан";
			if (User.Height > 40) run_height.Text += User.Height.ToString() + " см"; else run_height.Text += " не указан";
			if (User.Birthday != null) { Age.Text += GetAge(User.Birthday); Birthday.Text += User.Birthday.ToShortDateString(); }
			                      else { Birthday.Text += " не указана"; Age.Text += " не указан"; }
			if (User.Education != null) Education.Text += User.Education; else Education.Text += " не указано";
			if (User.Faith != null) Faith.Text += User.Faith; else Faith.Text += " не указана";
			if (User.Country != null && User.City != null)
			CountryCity.Text = $"({User.Country}, {User.City})";
			if (User.Country != null)
				CountryCity.Text = $"({User.Country})";
			if (Hobbies.Count > 0)
			{
				for (int i = 0; i < Hobbies.Count - 1; i++)
					this.Hobbies.Text += Hobbies[i].Hobbie + ", ";
				this.Hobbies.Text += Hobbies[Hobbies.Count - 1].Hobbie;
			}
			About.Text += User.Description;
			textBox_email.Text = User.Email;
			if (User.Weight > 0)
			run_weight.Text += User.Weight + " кг";
			else run_weight.Text += " не указан";
		}

		public void PlaceAllItems(List<Image> ImageList, ServiceReference1.User User, double Distance, List<ServiceReference1.Hobbies> Hobbies)
		{
			Images = ImageList;
			CurrentPhoto = 0;
			if (Images.Count == 1)
			{
				Arrow1.Visibility = Visibility.Collapsed;
				Arrow2.Visibility = Visibility.Collapsed;
				Button_right.Visibility = Visibility.Collapsed;
				Button_left.Visibility = Visibility.Collapsed;
			}
			user_photo.Source = Images[0].Source;
			textBox_Name.Text = User.Name + " " + User.LastName;
			run_distance.Text = GetDistance(Distance);
			if (User.Job != null) run_job.Text = User.Job;
			if (User.ColorEye != null) run_ColorEye.Text = User.ColorEye; else run_ColorEye.Text = " не указан";
			if (User.ColorHairCut != null) run_ColorHaircut.Text = User.ColorHairCut; else run_ColorHaircut.Text = " не указан";
			if (User.Gender != null) run_gender.Text = User.Gender; else run_gender.Text = " не указан";
			if (User.Height > 40) run_height.Text = User.Height.ToString() + " см"; else run_height.Text = " не указан";
			Age.Text = "Возраст: "; Birthday.Text = "Дата рождения: ";
			if (User.Birthday != null) { Age.Text += GetAge(User.Birthday); Birthday.Text += User.Birthday.ToShortDateString(); }
			else { Birthday.Text += " не указана"; Age.Text += " не указан"; }
			Education.Text = "Образование: ";
			if (User.Education != null) Education.Text += User.Education; else Education.Text += " не указано";
			Faith.Text = "Вера: ";
			if (User.Faith != null) Faith.Text += User.Faith; else Faith.Text += " не указана";
			if (User.Country != null && User.City != null) CountryCity.Text = $"({User.Country}, {User.City})";
			else if (User.Country != null) CountryCity.Text = $"({User.Country})";
			this.Hobbies.Text = "Хобби: ";
			if (Hobbies.Count > 0)
			{
				for (int i = 0; i < Hobbies.Count - 1; i++)
					this.Hobbies.Text += Hobbies[i].Hobbie + ", ";
				this.Hobbies.Text += Hobbies[Hobbies.Count - 1].Hobbie;
			}
			About.Text = "О себе:\n" + User.Description;
			textBox_email.Text = User.Email;
			run_weight.Text = "";
			if (User.Weight > 0) run_weight.Text = User.Weight + " кг";
			else run_weight.Text = " не указан";
		}

		public string GetAge(DateTime birthday)
		{
			if (birthday.DayOfYear < DateTime.Now.DayOfYear) return $"{DateTime.Now.Year - birthday.Year} лет";
			else return $"{DateTime.Now.Year - birthday.Year + 1} лет";
		}

		private string GetDistance(double Distance)
		{
			string DistanceStr;
			if (Distance > 1000)
			{
				if (Distance > 10000) DistanceStr = Math.Floor(Distance / 1000).ToString();
				else DistanceStr = (Distance / 1000).ToString();
				DistanceStr += " км";
			}
			else if (Distance > 800) DistanceStr = $"1 км";
			else if (Distance > 250) DistanceStr = ((int)Distance).ToString() + " м";
			else if (Distance < 50) DistanceStr = "очень близко";
			else DistanceStr = Math.Round(Distance).ToString() + " м";
			return DistanceStr;
		}

		private void Button_Click(object sender, RoutedEventArgs e)
		{
			Clipboard.SetText(textBox_email.Text);
		}

		private void Button_Click_1(object sender, RoutedEventArgs e)
		{
			if (++CurrentPhoto >= Images.Count) CurrentPhoto = 0; UpdatePhoto();
		}

		private void UpdatePhoto()
		{
			if (Images.Count > 0)
			user_photo.Source = Images[CurrentPhoto].Source;
		}

		private void Button_Click_2(object sender, RoutedEventArgs e)
		{
			if (--CurrentPhoto < 0) CurrentPhoto = Images.Count - 1; UpdatePhoto();
		}
	}
}
