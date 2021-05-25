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
    /// Interaction logic for LikedUser.xaml
    /// </summary>
    public partial class LikedUser : UserControl
    {
        public LikedUser()
        {
            InitializeComponent();
        }

        private void UserControl_MouseEnter(object sender, MouseEventArgs e)
        {


            if (flag) {
                var bc = new BrushConverter();

                grid.Background = (Brush)bc.ConvertFrom("#DCDCDC");
                borderremove.Background = (Brush)bc.ConvertFrom("#DCDCDC");
                borderlike.Background = (Brush)bc.ConvertFrom("#DCDCDC");

            }

        }

        private void UserControl_MouseLeave(object sender, MouseEventArgs e)
        {
            grid.Background = new SolidColorBrush(Colors.WhiteSmoke);
            borderlike.Background = new SolidColorBrush(Colors.WhiteSmoke);
            borderremove.Background = new SolidColorBrush(Colors.WhiteSmoke);

            flag = true;
        }

        private void Border_MouseEnter(object sender, MouseEventArgs e)
        {
            image.Cursor = Cursors.Hand;
        }
      
        private void PackIcon_MouseEnter(object sender, MouseEventArgs e)
        {
           
        }
        bool flag = true;
       

        private void borderlike_MouseEnter(object sender, MouseEventArgs e)
        {
            var bc = new BrushConverter();

            grid.Background = (Brush)bc.ConvertFrom("#DCDCDC");
            borderremove.Background = (Brush)bc.ConvertFrom("#DCDCDC");
            borderlike.Background = (Brush)bc.ConvertFrom("#DCDCDC");
            borderlike.Background = new SolidColorBrush(Colors.LimeGreen);
           
            flag = false;
        }

        private void borderremove_MouseEnter(object sender, MouseEventArgs e)
        {
            var bc = new BrushConverter();

            grid.Background = (Brush)bc.ConvertFrom("#DCDCDC");
            borderremove.Background = (Brush)bc.ConvertFrom("#DCDCDC");
            borderlike.Background = (Brush)bc.ConvertFrom("#DCDCDC");
            borderremove.Background = new SolidColorBrush(Colors.Red);

            flag = false;
        }

        private void borderlike_MouseLeave(object sender, MouseEventArgs e)
        {
            borderlike.Background = new SolidColorBrush(Colors.WhiteSmoke);
        }

        private void borderremove_MouseLeave(object sender, MouseEventArgs e)
        {
            borderremove.Background = new SolidColorBrush(Colors.WhiteSmoke);
        }
    }
}
