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
    /// Логика взаимодействия для Chatlistitem.xaml
    /// </summary>
    public partial class Chatlistitem : UserControl
    {
        public Chatlistitem()
        {
            InitializeComponent();
        }
        public Chatlistitem(string name, string lastmes, byte[] imagePath)
        {
<<<<<<< HEAD
            Nameuser = name;
=======
            NameUser = name;
>>>>>>> Anton
            ImagePath = imagePath;
            Lastmes= lastmes;
            InitializeComponent();

        }
        public byte[] ImagePath { get; set; }
<<<<<<< HEAD
        public string Nameuser { get; set; }
=======
        public string NameUser { get; set; }
>>>>>>> Anton
        public string Lastmes { get; set; }
    }
}
