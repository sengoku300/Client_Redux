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
    /// Логика взаимодействия для Mess.xaml
    /// </summary>
    public partial class Mess : UserControl
    {
        public Mess()
        {
            InitializeComponent();
        }
<<<<<<< HEAD
=======
        public Mess(string mes, string time, byte[] imagePath)
        {
            Mes = mes;
            ImagePath = imagePath;
            TimeSending = time;
            InitializeComponent();

        }
        public byte[] ImagePath { get; set; }
        public string TimeSending { get; set; }
        public string Mes { get; set; }
>>>>>>> Anton
    }
}
