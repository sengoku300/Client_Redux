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
		public ProfileControl(List<Image> ImageList, WPF_Redux_Client.ServiceReference1.User User, double Distance, List<ServiceReference1.Hobbies> Hobbies)
		{
			Images = ImageList;
			InitializeComponent();
			textBox_Name.Text = User.Name + " " + User.LastName;
			run_distance.Text = GetDistance(Distance);
			run_job.Text = User.Job;
			run_ColorEye.Text += User.ColorEye;
			run_ColorHaircut.Text += User.ColorHairCut;
			run_gender.Text += User.Gender;
			run_height.Text += User.Height.ToString() + " см";
			Birthday.Text += User.Birthday.ToShortDateString();
			Age.Text += GetAge(User.Birthday);
			Education.Text += User.Education;
			Faith.Text += User.Faith;
			CountryCity.Text = $"({User.Country}, {User.City})";
			for (int i = 0; i < Hobbies.Count - 1; i++) 
				this.Hobbies.Text += Hobbies[i].Hobbie + ", ";
			this.Hobbies.Text += Hobbies[Hobbies.Count - 1].Hobbie + ".";
			About.Text += User.Description;
			textBox_email.Text = User.Email;
			run_weight.Text += User.Weight + " кг";
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
			else DistanceStr = "очень близко";
			return DistanceStr;
		}

		private void Button_Click(object sender, RoutedEventArgs e)
		{
			Clipboard.SetText(textBox_email.Text);
		}

		private void Button_Click_1(object sender, RoutedEventArgs e)
		{
			if (CurrentPhoto >= Images.Count) CurrentPhoto = 0; UpdatePhoto();
		}

		private void UpdatePhoto()
		{
			user_photo = Images[CurrentPhoto];
		}

		private void Button_Click_2(object sender, RoutedEventArgs e)
		{
			if (CurrentPhoto <= 0) CurrentPhoto = Images.Count - 1; UpdatePhoto();
		}
	}
}
