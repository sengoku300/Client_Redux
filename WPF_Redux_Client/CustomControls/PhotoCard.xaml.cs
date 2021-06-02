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
	/// Interaction logic for Photo.xaml
	/// </summary>
	public partial class PhotoCard : UserControl
	{
		public event MegaClass.DeletePhotography DeletingEvent;
		public event MegaClass.SelectionPhotography SelectionEvent;
		const double DefaultScale = 1;
		const double DefaultScaleIncreasement = 0.08;
		bool IsMainPhotography = false;

		public ServiceReference1.User User { get; set; } 
		public byte[] ImagePath { get; set; }

		SolidColorBrush pink = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#ea90b5"));
		SolidColorBrush gray = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#dedede"));
		SolidColorBrush green = new SolidColorBrush(Colors.LightGreen);

		public PhotoCard()
		{
			InitializeComponent();
		}

		private void ChangeButton()
		{
			switch (IsMainPhotography)
			{
				case true:
					Button_main.Background = green;
					Button_main.FontSize = 18;
					Button_main.Content = "✓";
					break;
				case false:
					Button_main.Background = gray;
					Button_main.FontSize = 16;
					Button_main.Content = "Сделать аватаркой";
					break;
			}
		}

		private void Button_main_Click(object sender, RoutedEventArgs e)
		{
			IsMainPhotography = !IsMainPhotography;
			ChangeButton();
			SelectionEvent?.BeginInvoke(UC, null, null);
		}

		private void UC_MouseEnter(object sender, MouseEventArgs e)
		{	
			TryToChangeOpacityForMainButton(); 
			if (!IsMainPhotography)	
				TryIncreaseScale();	
		}

		private void TryToChangeOpacityForMainButton()
        {	
			DoubleAnimationUsingKeyFrames opacityMainButton = new DoubleAnimationUsingKeyFrames();
			int Value = 0;
			if (IsMainPhotography || IsMouseOver) Value = 1;
			opacityMainButton.KeyFrames.Add(new LinearDoubleKeyFrame(Value, KeyTime.FromPercent(0.5)));
			Button_main.BeginAnimation(Button.OpacityProperty, opacityMainButton);	
        }

		private async void TryIncreaseScale()
		{
			//double Scale = DefaultScale; 
			//if (IncreasedInScale) Scale += 0.2;
			//UC.RenderTransform = new ScaleTransform(Scale, Scale);


			//TimeSpan ts = new TimeSpan(0, 0, 0, 0, 3);
			//UC.RenderTransform.BeginAnimation(ScaleTransform.ScaleXProperty, new DoubleAnimation(Scale, ts), HandoffBehavior.SnapshotAndReplace);
			//UC.RenderTransform.BeginAnimation(ScaleTransform.ScaleYProperty, new DoubleAnimation(Scale, ts), HandoffBehavior.Compose);



			DoubleAnimationUsingKeyFrames growImageX = new DoubleAnimationUsingKeyFrames();
			DoubleAnimationUsingKeyFrames growImageY = new DoubleAnimationUsingKeyFrames();

			double Scale = DefaultScale + DefaultScaleIncreasement / 2;

			await Task.Run(() =>EmptyTask());
			growImageX.KeyFrames.Add(new LinearDoubleKeyFrame(Scale, KeyTime.FromPercent(0)));
			growImageY.KeyFrames.Add(new LinearDoubleKeyFrame(Scale, KeyTime.FromPercent(0)));
			if (IsMouseOver) Scale += DefaultScaleIncreasement / 2;
			else Scale -= DefaultScaleIncreasement / 2;
			growImageX.KeyFrames.Add(new LinearDoubleKeyFrame(Scale, KeyTime.FromPercent(0.1)));
			growImageY.KeyFrames.Add(new LinearDoubleKeyFrame(Scale, KeyTime.FromPercent(0.1)));


			growImageX.Duration = new Duration(TimeSpan.FromMilliseconds(20));
			ScaleTransform RectXForm = new ScaleTransform();
			switch (IsMouseOver)
			{
				case true:
					Scale = DefaultScale;
					break;
				case false:
					Scale = DefaultScale + DefaultScaleIncreasement / 2;
					break;
			}
		    UC.RenderTransform = RectXForm;

			RectXForm.BeginAnimation(ScaleTransform.ScaleXProperty, growImageX);
			RectXForm.BeginAnimation(ScaleTransform.ScaleYProperty, growImageY);
		}
		void EmptyTask() {; }

        private void Button_delete_Click(object sender, RoutedEventArgs e)
        {
			DoubleAnimationUsingKeyFrames animation = new DoubleAnimationUsingKeyFrames();
			animation.KeyFrames.Add(new LinearDoubleKeyFrame(0, KeyTime.FromPercent(0.5)));
			UC.BeginAnimation(UserControl.OpacityProperty, animation);
			DeletingEvent?.BeginInvoke(this, null, null);
        }

		public void Unselect()
        {
			if (!Dispatcher.CheckAccess()) { Dispatcher.Invoke(() =>Unselect()); }
			else
			if (IsMainPhotography)
            {
				IsMainPhotography = false; 
				ChangeButton();
				TryToChangeOpacityForMainButton();
				TryIncreaseScale();
            }
        }
    }
}
