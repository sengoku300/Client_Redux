using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.ServiceModel;
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
using WPF_Redux_Client.ServiceReference1;

namespace WPF_Redux_Client.Pages
{
	/// <summary>
	/// Interaction logic for PhotoGallery.xaml
	/// </summary>
	public partial class PhotoGallery : Page
    {
        private Service1Client client;
        int photo_number = 0;
        const int photo_max_number = 5;
        private List<BitmapImage> images;

        SolidColorBrush Red = new SolidColorBrush(Colors.Red);
        SolidColorBrush DarkRed = new SolidColorBrush(Colors.DarkRed);
        SolidColorBrush Yellow = new SolidColorBrush(Colors.Yellow);
        SolidColorBrush YellowGreen = new SolidColorBrush(Colors.YellowGreen);
        SolidColorBrush Green = new SolidColorBrush(Colors.LightGreen);
        SolidColorBrush ForestGreen = new SolidColorBrush(Colors.ForestGreen);


        public PhotoGallery()
        {
            InitializeComponent();
            InitializeProgressBar();
            UpdateLabelAndButton();
        }

        public PhotoGallery(User user)
        {
            InitializeComponent();
            InitializeProgressBar();
            UpdateLabelAndButton();


        }

        private void InitializeProgressBar()
		{
            progressBar_PhotosCount.Maximum = photo_max_number;
            progressBar_PhotosCount.Minimum = photo_number;
		}

        public PhotoGallery(User user)
        {
            InitializeComponent();

            listBox_Photos.ItemsSource = images;

            IService1Callback callback = this as IService1Callback;

            InstanceContext context = new InstanceContext(callback);

            client = new Service1Client(context);

            var bytes = client.GetPhotos(user);

            foreach (var item in bytes)
            {
                BitmapImage bitmapImage = new BitmapImage();
                bitmapImage.BeginInit();
                bitmapImage.StreamSource = new MemoryStream(item);
                bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapImage.EndInit();

                images.Add(bitmapImage);
            }

            if (images.Count > 0)
                listBox_Photos.ItemsSource = images;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (photo_number < photo_max_number)
			{
                OpenFileDialog ofd = new OpenFileDialog { Filter = "Image Files(*.BMP;*.JPG;*.GIF)|*.BMP;*.JPG;*.GIF", Multiselect = false, Title = "Добавление фотографии в профиль" };
                
                if (ofd.ShowDialog() == true)
				{
                    byte[] Photo = File.ReadAllBytes(ofd.FileName);
                    photo_number++;
				}
                
			}
        }

        private void AddPhoto(Image Photo)
        {
            try
            {
                PhotoCard card = new PhotoCard() { Photo = Photo };
                card.DeletingEvent += Card_DeletingEvent;
                card.SelectionEvent += Card_SelectionEvent;
                listBox_Photos.Items.Add(card);
                progressBar_PhotosCount.Value = ++photo_number;
                UpdateLabelAndButton();
            }
            catch (Exception ex)
            { 
                MessageBox.Show(ex?.Message, "Ошибка!");
            }
        }

        private void UpdateLabelAndButton()
		{
            textBox_PhotosCount.Text = $"{photo_number}/{5}";
            double value = (double) photo_number / (double)photo_max_number;
            if (value < 0.4 || value > 1)
            {
                textBox_PhotosCount.Foreground = Red;
                Button_add.Background = Green;
                Button_add.BorderBrush = ForestGreen;
                Button_add.ToolTip = "Рекомендуем добавить ещё фотографий.";
            }
            else if (value < 0.9)
            {
                textBox_PhotosCount.Foreground = Yellow;
                Button_add.Background = YellowGreen;
                Button_add.BorderBrush = Yellow;
                Button_add.ToolTip = "Можно добавить ещё фотографии.";

            }
            else if (value == 1)
            {
                textBox_PhotosCount.Foreground = Green;
                Button_add.Background = Red;
                Button_add.BorderBrush = DarkRed;
                Button_add.ToolTip = "Достигнуто максимальное число фотографий профиля!";
            }
		}

        private void Card_SelectionEvent(PhotoCard card)
        {
            for (int i = 0; i < listBox_Photos.Items.Count; i++)
            {
                PhotoCard item = (PhotoCard)listBox_Photos.Items.GetItemAt(i);
                if (item != card) item.Unselect();
            }
        }

        private void DeletePhotoOnServer(PhotoCard card)
        {
            //TODO: Здесб удалять как-та
        }

        private void Card_DeletingEvent(PhotoCard caller)
        {
            if (!Dispatcher.CheckAccess()) Dispatcher.Invoke(() => Card_DeletingEvent(caller));
            else
			{
                try
                {
                    listBox_Photos.Items.Remove(caller);
                    DeletePhotoOnServer(caller);
                    progressBar_PhotosCount.Value = --photo_number;
                    UpdateLabelAndButton();
                }
                catch { }
			}
        }
    }
}
