using System;
using System.Collections.Generic;
using System.Linq;
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

namespace WPF_Redux_Client.CustomControls
{
    /// <summary>
    /// Interaction logic for FilterItem.xaml
    /// </summary>
    public partial class FilterItem : UserControl
    {
        Regex regex = new Regex("[^0-9]+");

        Regex regex_alph = new Regex(@"^[a-zA-Z]+$");

        public FilterItem()
        {
            InitializeComponent();

            //NameHeight.email.MaxLength = 3;
        }

        private void NameAge_PreviewTextInput(object sender, TextCompositionEventArgs e) => e.Handled = regex.IsMatch(e.Text);

        private void NameCountry_PreviewTextInput(object sender, TextCompositionEventArgs e) => e.Handled = regex_alph.IsMatch(e.Text);

        private void slider_distance_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if(e.NewValue == 100)
                slider_distance.Maximum = 500;
        }
    }
}
