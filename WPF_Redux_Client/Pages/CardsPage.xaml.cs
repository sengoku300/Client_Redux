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
    /// Interaction logic for CardsPage.xaml
    /// </summary>
    public partial class CardsPage : Page
    {
        protected Point SwipeStart;

        UserCard one = new UserCard();
        UserCard two = new UserCard();
        UserCard three = new UserCard();

        public CardsPage()
        {
            InitializeComponent();

            Panel.SetZIndex(one, 0);
            Panel.SetZIndex(two, 1);
            Panel.SetZIndex(three, 2);

            //Style style = new Style(typeof(UserCard));
            //style.Setters.Add(new Setter(UserCard.ForegroundProperty, Brushes.Green));
            //style.Setters.Add(new Setter(UserCard.BackgroundProperty, Brushes.Red));
            //Resources.Add(typeof(UserCard), style);

            one.Margin = new Thickness(56, 0, 0, 30);
            two.Margin = new Thickness(45, 0, 0, 20);
            three.Margin = new Thickness(20, 10, 0, 20);

            one.MouseDown += One_MouseDown;
            one.MouseMove += One_MouseMove;
            
            two.MouseDown += One_MouseDown;
            two.MouseMove += One_MouseMove;
            
            three.MouseDown += One_MouseDown;
            three.MouseMove += One_MouseMove;


            grid.Children.Add(one);
            grid.Children.Add(two);
            grid.Children.Add(three);
        }

        private void One_MouseMove(object sender, MouseEventArgs e)
        {
            if(e.LeftButton == MouseButtonState.Pressed)
            {
                var swipe = e.GetPosition(this);

                if (SwipeStart != null && swipe.X > (SwipeStart.X + 200))
                {
                    MessageBox.Show("SwipeRight");
                    grid.Children.Remove((UserCard)sender);
                }

                if (SwipeStart != null && swipe.X < (SwipeStart.X - 200))
                {
                    MessageBox.Show("SwipeLeft");
                }
            }
        }

        private void One_MouseDown(object sender, MouseButtonEventArgs e) => SwipeStart = e.GetPosition(this);



        private void Page_MouseDown(object sender, MouseButtonEventArgs e)
        {
            SwipeStart = e.GetPosition(this);
        }

        private void Page_MouseMove(object sender, MouseEventArgs e)
        {

        }
    }
}
