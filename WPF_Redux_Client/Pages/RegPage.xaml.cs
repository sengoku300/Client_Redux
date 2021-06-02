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
    public partial class RegPage : Page, IService1Callback
    {
        private string gender;

        Service1Client client;

        Regex regex = new Regex("[^0-9]+");

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
            string ipLoc = string.Empty;

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
            string ipLoc = string.Empty;

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
            string ipLoc = string.Empty;

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
        
        private void Button_Click_1(object sender, RoutedEventArgs e) => gender = "Мужчина";

        private void Button_Click_2(object sender, RoutedEventArgs e) => gender = "Женщина";

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
            => e.Handled = regex.IsMatch(e.Text);

        private void textBox_Birthday_MM_PreviewTextInput(object sender, TextCompositionEventArgs e)
            => e.Handled = regex.IsMatch(e.Text);

        private void textBox_Birthday_Year_PreviewTextInput(object sender, TextCompositionEventArgs e)
            => e.Handled = regex.IsMatch(e.Text);

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
