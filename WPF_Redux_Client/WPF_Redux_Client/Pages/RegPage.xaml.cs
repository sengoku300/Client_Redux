using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Text.RegularExpressions;
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
using WPF_Redux_Client.ServiceReference1;

namespace WPF_Redux_Client.Pages
{
    /// <summary>
    /// Interaction logic for RegPage.xaml
    /// </summary>
    public partial class RegPage : Page
    {
        private string gender;

        Service1Client client;

        Regex regex = new Regex("[^0-9]+");



        SolidColorBrush purple = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#ea90b5"));
        SolidColorBrush green = new SolidColorBrush(Colors.LightGreen);
        SolidColorBrush red = new SolidColorBrush(Colors.Red);
        SolidColorBrush transparent = new SolidColorBrush(Colors.Transparent);

        public RegPage()
        {
            InitializeComponent();

            city_name.Text = GetCity();
            country_name.Text = GetCountry();

            textBox_Birthday_DD.email.MaxLength = 2;
            textBox_Birthday_MM.email.MaxLength = 2;
            textBox_Birthday_Year.email.MaxLength = 4;
        }

        Authorization authoriz { get => Application.Current.MainWindow as Authorization; }
        private string GetCountry()
        {
            var client = new RestClient("https://ipapi.co/json/");
            var request = new RestRequest
            {
                Method = Method.GET
            };

            var response = client.Execute(request);

            var disc = JsonConvert.DeserializeObject<IDictionary>(response.Content);

            foreach (var item in disc.Keys)
            {
                if (item.ToString() == "country_name")
                    return disc[item].ToString();
            }

            return null;
        }

        private double GetLatiTude()
        {
            var client = new RestClient("https://ipapi.co/json/");
            var request = new RestRequest
            {
                Method = Method.GET
            };

            var response = client.Execute(request);

            var disc = JsonConvert.DeserializeObject<IDictionary>(response.Content);

            foreach (var item in disc.Keys)
            {
                if (item.ToString() == "latitude")
                    return Convert.ToDouble(disc[item]);
            }

            return 0;
        }

        private double GetLongiTude()
        {
            var client = new RestClient("https://ipapi.co/json/");
            var request = new RestRequest
            {
                Method = Method.GET
            };

            var response = client.Execute(request);

            var disc = JsonConvert.DeserializeObject<IDictionary>(response.Content);

            foreach (var item in disc.Keys)
            {
                if (item.ToString() == "longitude")
                    return Convert.ToDouble(disc[item]);
            }

            return 0;
        }

        private string GetCity()
        {
            var client = new RestClient("https://ipapi.co/json/");
            var request = new RestRequest
            {
                Method = Method.GET
            };

            var response = client.Execute(request);

            var disc = JsonConvert.DeserializeObject<IDictionary>(response.Content);

            foreach (var item in disc.Keys)
            {
                if (item.ToString() == "city")
                    return disc[item].ToString();
            }

            return null;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e) { gender = "Мужчина"; Button_Gender_Male.Background = green; Button_Gender_Female.Background = purple; }

        private void Button_Click_2(object sender, RoutedEventArgs e) { gender = "Женщина"; Button_Gender_Male.Background = purple; Button_Gender_Female.Background = green; }
        private void Button_Click_3(object sender, RoutedEventArgs e)
        {

            if (Int32.TryParse(textBox_Birthday_DD.Text, out int dd)
                && Int32.TryParse(textBox_Birthday_MM.Text, out int mm)
                && Int32.TryParse(textBox_Birthday_Year.Text, out int year)
                && textBox_Mail.Text.Contains("@")
                && !string.IsNullOrEmpty(textBox_Name_Family.Text)
                && !Int32.TryParse(textBox_Name_Family.Text, out int result)
                && passBox.passbox.Password != ""
                && gender != "")
            {
                IService1Callback callback = this as IService1Callback;

                InstanceContext context = new InstanceContext(callback);

                client = new Service1Client(context);

                string birthday = dd + "." + mm + "." + year;

                DateTime date = DateTime.Parse(birthday);

                int year_us = DateTime.Now.Year - date.Year;

                if (year_us >= 18)
                {
                    double latitude = GetLatiTude();

                    double longitude = GetLongiTude();

                    client.AddAccount(textBox_Mail.Text, passBox.passbox.Password,
                        textBox_Name_Family.Text, city_name.Text, country_name.Text,
                        date, gender, latitude, longitude);

                    authoriz.authMain.Navigate(new Uri("Pages/LoginPage.xaml", UriKind.RelativeOrAbsolute));
                }
                else
                    MessageBox.Show("Регистрация в приложении доступна с 18-ти лет!");
            }
        }


        private void textBox_Birthday_DD_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (regex.IsMatch(e.Text)) { textBox_Birthday_DD.BorderBrush = red; e.Handled = true; }
            else if (Convert.ToInt32(textBox_Birthday_DD.email.Text + e.Text) > 31) { textBox_Birthday_DD.BorderBrush = red; MessageBox.Show("В месяце не может быть больше 31-го дня!"); e.Handled = true; }
            else if (textBox_Birthday_MM.Text?.Length > 0 && textBox_Birthday_Year.Text?.Length == 4 && Convert.ToInt32(textBox_Birthday_DD.email.Text + e.Text) > DateTime.DaysInMonth(Convert.ToInt32(textBox_Birthday_Year.Text), Convert.ToInt32(textBox_Birthday_MM.Text))) { textBox_Birthday_DD.BorderBrush = red; MessageBox.Show($"В этом месяце может быть не больше {DateTime.DaysInMonth(Convert.ToInt32(textBox_Birthday_Year.Text), Convert.ToInt32(textBox_Birthday_MM.Text))} дней."); e.Handled = true; }
            else textBox_Birthday_DD.BorderBrush = transparent;
        }

        private void textBox_Birthday_MM_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (regex.IsMatch(e.Text)) { textBox_Birthday_MM.BorderBrush = red; e.Handled = true; }
            else if (Convert.ToInt32(textBox_Birthday_MM.email.Text + e.Text) > 12) { textBox_Birthday_MM.BorderBrush = red; MessageBox.Show("В месяце не может быть больше 12 месяцев!"); e.Handled = true; }
            else if (Convert.ToInt32(textBox_Birthday_MM.email.Text + e.Text) == 0) { textBox_Birthday_MM.BorderBrush = red; MessageBox.Show("Нулевой месяц не существует!"); e.Handled = true; }
            else textBox_Birthday_MM.BorderBrush = transparent;
        }

        private void textBox_Birthday_Year_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (regex.IsMatch(e.Text)) { textBox_Birthday_Year.BorderBrush = red; e.Handled = true; }
            else if (Convert.ToInt32(textBox_Birthday_Year.email.Text + e.Text) > DateTime.Now.Year) { textBox_Birthday_Year.BorderBrush = red; MessageBox.Show("Нельзя регистрироваться до своего рождения!"); e.Handled = true; }
            else if (Convert.ToInt32(textBox_Birthday_Year.email.Text + e.Text) + 122 < DateTime.Now.Year) { textBox_Birthday_Year.BorderBrush = red; MessageBox.Show("Хотя Вы, может, и похожи на Жанну Кальман, но вряд-ли Вам больше 122-х лет!"); e.Handled = true; }
            else textBox_Birthday_MM.BorderBrush = transparent;
        }
    }
}
