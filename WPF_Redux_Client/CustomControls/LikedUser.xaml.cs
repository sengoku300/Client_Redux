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
            var bc = new BrushConverter();

            user.Background = (Brush)bc.ConvertFrom("#DCDCDC");
           


        }

        private void UserControl_MouseLeave(object sender, MouseEventArgs e)
        {
            user.Background = new SolidColorBrush(Colors.WhiteSmoke);
        }

        private void Border_MouseEnter(object sender, MouseEventArgs e)
        {
            image.Cursor = Cursors.Hand;
        }
      
        private void PackIcon_MouseEnter(object sender, MouseEventArgs e)
        {
           
        }

        private void PackIcon_MouseEnter_1(object sender, MouseEventArgs e)
        {
           
        }
    }
}
