using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections;
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

namespace WPF_Redux_Client.Pages
{
    /// <summary>
    /// Interaction logic for RegPage.xaml
    /// </summary>
    public partial class RegPage : Page
    {
        public RegPage()
        {
            InitializeComponent();

            city_name.Text = GetCity();
            country_name.Text = GetCountry();
        }

        private string GetCountry()
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
                if (item.ToString() == "country_name")
                    return ipLoc = disc[item].ToString();
            }

            return ipLoc;
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
                    return ipLoc = disc[item].ToString();
            }

            return ipLoc;
        }  
        
        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {

        }
    }
}
