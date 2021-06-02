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
	public partial class PhotoGallery : Page, IService1Callback
    {
        private Service1Client client;

        int photo_number = 0;
        const int photo_max_number = 5;
        public User User { get; set; }

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
            Initialize();
        }

        private async void Initialize()
        {
            await Task.Run(() => { });

            IService1Callback callback = this as IService1Callback;

            InstanceContext context = new InstanceContext(callback);

            client = new Service1Client(context);

            var Photos = client.GetPhotos(User).ToList();

            byte[] MainPhoto = client.GetImage(User);

            if (Photos.Count > 1 && MainPhoto != null) 
            {
                for (int i = 0; i < Photos.Count(); i++)
                {
                    if (Photos[i].SequenceEqual(MainPhoto))
                    {
                        Photos.RemoveAt(i);
                        Photos.Insert(0, MainPhoto);
                    }
                }

                AddPhoto(new Image { Source= ImageFromByte(Photos[0]) }, true);

                for (int i = 1; i < Photos.Count; i++) AddPhoto(new Image { Source = ImageFromByte(Photos[i]) }, false);
            }
            else if (Photos.Count == 1) AddPhoto(new Image { Source = ImageFromByte(Photos[0]) }, true);
		}

        private BitmapImage ImageFromByte(byte[] array)
		{
            BitmapImage bitmap = new BitmapImage();
            bitmap.BeginInit();
            bitmap.StreamSource = new MemoryStream(array);
            bitmap.EndInit();
            return bitmap;
        }

        private void InitializeProgressBar()
		{
            progressBar_PhotosCount.Maximum = photo_max_number;
            progressBar_PhotosCount.Minimum = photo_number;
		}

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (photo_number < photo_max_number)
			{
                OpenFileDialog ofd = new OpenFileDialog { Filter = "Image Files(*.BMP;*.JPG;*.GIF)|*.BMP;*.JPG;*.GIF", Multiselect = false, Title = "Добавление фотографии в профиль" };
                if (ofd.ShowDialog() == true)
				{
                    string ext = System.IO.Path.GetExtension(ofd.FileName);

                    byte[] Photo = File.ReadAllBytes(ofd.FileName);

                    BitmapImage bitmapImage = new BitmapImage();
                    bitmapImage.BeginInit();
                    bitmapImage.StreamSource = new MemoryStream(Photo);
                    bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                    bitmapImage.EndInit();

                    client.AddPhoto(bitmapImage, User, ext);

                    photo_number++;
				}
                
			}
        }

        private void AddPhoto(Image Photo, bool IsMain)
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
            catch (Exception ex) { MessageBox.Show(ex?.Message, "Ошибка!"); }
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
            //client.Вуду(card.ImagePath, card.User);
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

        public void OnCallback()
        {
            throw new NotImplementedException();
        }

        public void OnSendMessage(int chatid, Message message)
        {
            throw new NotImplementedException();
        }
    }
}
