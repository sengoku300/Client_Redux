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
    /// Логика взаимодействия для Mymes.xaml
    /// </summary>
    public partial class Mymes : UserControl
    {
       
        public Mymes()
        {
            InitializeComponent();
        }
        public Mymes(string mes, string time)
        {
            Mes = mes;

            TimeSending = time;
            InitializeComponent();

        }

        public string TimeSending { get; set; }
        public string Mes { get; set; }
    }
}
