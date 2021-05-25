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
using WPF_Redux_Client.CustomControls;

namespace WPF_Redux_Client.Pages
{
    /// <summary>
    /// Interaction logic for ListMessagesPage.xaml
    /// </summary>
    public partial class ListMessagesPage : Page
    {
        public ListMessagesPage()
        {
            InitializeComponent();
            lst1.Items.Add(new CustomControls. UserControl1());
            lst1.Items.Add(new UserControl1());
            lst1.Items.Add(new UserControl1());
            lst1.Items.Add(new UserControl1());
            lst1.Items.Add(new UserControl1());
            lst1.Items.Add(new UserControl1());
            lst1.Items.Add(new UserControl1());
            lst1.Items.Add(new UserControl1());
           
        }
    }
}
