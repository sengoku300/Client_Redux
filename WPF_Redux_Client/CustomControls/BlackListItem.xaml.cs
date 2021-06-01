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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WPF_Redux_Client.CustomControls
{
    /// <summary>
    /// Логика взаимодействия для BlackListItem.xaml
    /// </summary>
    public partial class BlackListItem : UserControl
    {
        const int DefaultScale = 1;
        const int DefaultScaleIncreasement = -1;

        public event MegaClass.OpenProfile OpenProfileEvent;

        public ServiceReference1.User User { get; set; }
        public event MegaClass.UnbanAndLikeProfile UnbanEvent;
        bool WasUsed = false;
        public BlackListItem()
        {
            InitializeComponent();
        }
        public BlackListItem(int age,string city,byte[]imagepath,string Name)
        {
            InitializeComponent();
            name.Text = Name;
          this.ImagePath= imagepath;
            user_city.Text = city;
            user_year.Text = age.ToString();
        }
        public byte[] ImagePath { get; set; }
        private void UserControl_MouseEnter(object sender, MouseEventArgs e)
        {
            if (flag)
            {
                var bc = new BrushConverter();

                grid.Background = (Brush)bc.ConvertFrom("#DCDCDC");
                
                borderlike.Background = (Brush)bc.ConvertFrom("#DCDCDC");
            }
        }

        private void UserControl_MouseLeave(object sender, MouseEventArgs e)
        {
            grid.Background = new SolidColorBrush(Colors.WhiteSmoke);
            borderlike.Background = new SolidColorBrush(Colors.WhiteSmoke);
        
            flag = true;
        }
        bool flag = true;

        private void borderlike_MouseLeave(object sender, MouseEventArgs e)
        {
            var bc = new BrushConverter();
            borderlike.Background = (Brush)bc.ConvertFrom("#DCDCDC");
        }

        private void borderlike_MouseEnter(object sender, MouseEventArgs e)
        {
            var bc = new BrushConverter();

            grid.Background = (Brush)bc.ConvertFrom("#DCDCDC");

            borderlike.Background = new SolidColorBrush(Colors.LightPink);

            flag = false;
        }

        private void Border_MouseEnter(object sender, MouseEventArgs e)
        {

        }

        private async void Animation()
		{
            DoubleAnimationUsingKeyFrames growImageX = new DoubleAnimationUsingKeyFrames();
            DoubleAnimationUsingKeyFrames growImageY = new DoubleAnimationUsingKeyFrames();
            DoubleAnimationUsingKeyFrames animation = new DoubleAnimationUsingKeyFrames();
            animation.KeyFrames.Add(new LinearDoubleKeyFrame(0, KeyTime.FromPercent(0.2)));
            growImageY.Duration = growImageX.Duration = new Duration(TimeSpan.FromMilliseconds(20));

            double Scale = DefaultScale;

            await Task.Run(() => EmptyTask());
            growImageX.KeyFrames.Add(new LinearDoubleKeyFrame(Scale, KeyTime.FromPercent(0)));
            growImageY.KeyFrames.Add(new LinearDoubleKeyFrame(Scale, KeyTime.FromPercent(0)));

            Scale += DefaultScaleIncreasement / 2;

            growImageX.KeyFrames.Add(new LinearDoubleKeyFrame(Scale, KeyTime.FromPercent(0.1)));
            growImageY.KeyFrames.Add(new LinearDoubleKeyFrame(Scale, KeyTime.FromPercent(0.1)));

            Scale = DefaultScale + DefaultScaleIncreasement;

            growImageX.KeyFrames.Add(new LinearDoubleKeyFrame(Scale, KeyTime.FromPercent(0.2)));
            growImageY.KeyFrames.Add(new LinearDoubleKeyFrame(Scale, KeyTime.FromPercent(0.2)));

            ScaleTransform RectXForm = new ScaleTransform();
            user.RenderTransform = RectXForm;

            RectXForm.BeginAnimation(ScaleTransform.ScaleXProperty, growImageX);
            RectXForm.BeginAnimation(ScaleTransform.ScaleYProperty, growImageY);
            user.BeginAnimation(UserControl.OpacityProperty, animation);
        }

		private void EmptyTask()
		{
		}

		private async void borderlike_MouseDown(object sender, MouseButtonEventArgs e)
		{
            if (!WasUsed)
            {
                WasUsed = true;
                await Task.Run(() => EmptyTask());
                Animation();
                UnbanEvent?.BeginInvoke(this, null, null);
            }
		}

        private void user_MouseDown(object sender, MouseButtonEventArgs e)
        {
            OpenProfileEvent?.BeginInvoke(this, null, null);
        }
    }
}
