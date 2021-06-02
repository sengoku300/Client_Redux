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
using WPF_Redux_Client.ServiceReference1;

namespace WPF_Redux_Client.CustomControls
{
    /// <summary>
    /// Interaction logic for LikedUser.xaml
    /// </summary>
    public partial class LikedUser : UserControl
    {
        public User User { get; set; }
        public event MegaClass.BanProfile BanProfileEvent;
        public event MegaClass.LikeProfile LikeProfileEvent;
        public event MegaClass.DislikeProfile DislikeProfileEvent;
        public int Number { get; set; }
        public byte[] ImagePath { get; set; }
        public LikedUser()
        {
            InitializeComponent();
        }

        private void UserControl_MouseEnter(object sender, MouseEventArgs e)
        {


            if (flag) {
                var bc = new BrushConverter();

                grid.Background = (Brush)bc.ConvertFrom("#DCDCDC");
                borderlike.Background = (Brush)bc.ConvertFrom("#DCDCDC");
                borderblock.Background = (Brush)bc.ConvertFrom("#DCDCDC");

            }

        }

        private void UserControl_MouseLeave(object sender, MouseEventArgs e)
        {
            grid.Background = new SolidColorBrush(Colors.WhiteSmoke);
            borderlike.Background = new SolidColorBrush(Colors.WhiteSmoke);
            borderblock.Background = new SolidColorBrush(Colors.WhiteSmoke);
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

            borderblock.Background = (Brush)bc.ConvertFrom("#DCDCDC");
            grid.Background = (Brush)bc.ConvertFrom("#DCDCDC");
            borderlike.Background = (Brush)bc.ConvertFrom("#DCDCDC");
            borderlike.Background = new SolidColorBrush(Colors.LimeGreen);
           
            flag = false;
        }

        private void borderremove_MouseEnter(object sender, MouseEventArgs e)
        {
            var bc = new BrushConverter();

            grid.Background = (Brush)bc.ConvertFrom("#DCDCDC");
            borderlike.Background = (Brush)bc.ConvertFrom("#DCDCDC");
            borderblock.Background = (Brush)bc.ConvertFrom("#DCDCDC");
            flag = false;
        }

        private void borderlike_MouseLeave(object sender, MouseEventArgs e)
        {
            var bc = new BrushConverter();
            borderlike.Background = (Brush)bc.ConvertFrom("#DCDCDC");
           
        }

        private void borderremove_MouseLeave(object sender, MouseEventArgs e)
        {
            var bc = new BrushConverter();
           
        }

        private void borderblock_MouseLeave(object sender, MouseEventArgs e)
        {
            var bc = new BrushConverter();
            borderblock.Background = (Brush)bc.ConvertFrom("#DCDCDC");
        }

        private void borderblock_MouseEnter(object sender, MouseEventArgs e)
        {

            var bc = new BrushConverter();
            grid.Background = (Brush)bc.ConvertFrom("#DCDCDC");
            borderlike.Background = (Brush)bc.ConvertFrom("#DCDCDC");
            borderblock.Background = (Brush)bc.ConvertFrom("#52565e");
            flag = false;
        }

		private void borderlike_MouseDown(object sender, MouseButtonEventArgs e)
		{
            LikeProfileEvent?.BeginInvoke(this, null, null);
		}

		private void borderremove_MouseDown(object sender, MouseButtonEventArgs e)
		{
            DislikeProfileEvent?.BeginInvoke(this, null, null);
        }

		private void borderblock_MouseDown(object sender, MouseButtonEventArgs e)
		{
            BanProfileEvent?.BeginInvoke(this, null, null);
        }
	}
}
