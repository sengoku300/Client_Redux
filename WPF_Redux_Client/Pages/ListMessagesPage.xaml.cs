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
using System.Windows.Threading;
using WPF_Redux_Client.CustomControls;
using WPF_Redux_Client.ServiceReference1;

namespace WPF_Redux_Client.Pages
{
    /// <summary>
    /// Interaction logic for ListMessagesPage.xaml
    /// </summary>
    public partial class ListMessagesPage : Page,IService1Callback
    {
       
        int id;
        Service1Client client;
        TmpChatItem[] chatItems;
        TmpChatItem current;
        public byte[] ImagePath { get; set; }
        public ListMessagesPage(int userid)
        {
            InitializeComponent();
            tmpborder.Visibility = Visibility.Collapsed;
            id = userid;
           
            

            IService1Callback callback = this as IService1Callback;

            InstanceContext context = new InstanceContext(callback);

            client = new Service1Client(context);
            chatItems = client.GetChatItems(userid);
          
            if (chatItems != null)
            {
                foreach (var item in chatItems)
                {
                 Chatlistitem chatlistitem1=  new Chatlistitem(item.Title, item.LastMessage, item.ImagePath);
                    chatlistitem1.MouseDown += Chatlistitem_MouseDown;
                    lst1.Items.Add(chatlistitem1);
                }

                foreach (var item in chatItems)
                {
                    current = item;


                   
                    ImagePath=item.ImagePath;
                    headername.Text = item.Title;
                    if (item.messages != null)
                    foreach (var m in item.messages)
                    {
                        if (m.UserId != userid)
                        {
                          //  messages.Items.Add(new Mess() { Mes = m.Message, ImagePath = item.ImagePath, TimeSending = m.SendingTime });
                        }
                        else
                        {
                           // messages.Items.Add(new Mymes() { Mes = m.Message, TimeSending = m.SendingTime });
                        }
                    }
                    break;
                }
            }
            else
            {
                tmpborder.Visibility = Visibility.Visible;
            }

            scroll.ScrollToEnd();
            DispatcherTimer dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
            dispatcherTimer.Tick += DispatcherTimer_Tick; ;
            dispatcherTimer.Interval = new TimeSpan(0, 0, 2);
            dispatcherTimer.Start();
        }
        public BitmapSource ByteToBitmapSource(byte[] image)
        {
            BitmapImage imageSource = new BitmapImage();

            using (MemoryStream stream = new MemoryStream(image))
            {
                stream.Seek(0, SeekOrigin.Begin);
                imageSource.BeginInit();
                imageSource.StreamSource = stream;
                imageSource.CacheOption = BitmapCacheOption.OnLoad;
                imageSource.EndInit();
            }

            return imageSource;
        }

        private BitmapImage ImageFromByte(byte[] array)
        {
            BitmapImage bitmap = new BitmapImage();
            bitmap.BeginInit();
            bitmap.StreamSource = new MemoryStream(array);
            bitmap.EndInit();
            return bitmap;
        }

        private void Chatlistitem_MouseDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                Chatlistitem chatlistitem = sender as Chatlistitem;
                messages.Items.Clear();

                chatItems = client.GetChatItems(id);
                current = chatItems.Where(x => x.Title == chatlistitem.NameUser).FirstOrDefault();
                headername.Text = current.Title;

                ImagePath = current.ImagePath;

                DispatcherTimer_Tick(null,null);
                if (current.messages != null)
                foreach (var m in current.messages)
                {

                    if (m.UserId != id)
                    {
                       // messages.Items.Add(new Mess() { Mes = m.Message, ImagePath = current.ImagePath, TimeSending = m.SendingTime});
                    }
                    else
                    {
                       // messages.Items.Add(new Mymes() { Mes = m.Message, TimeSending = m.SendingTime });
                    }

                }
              
            }
            catch
            {
            }
          
        }

        private void DispatcherTimer_Tick(object sender, EventArgs e)
        {
            //tmpMessage[] tmpMessages = client.GetnewMes(current.Chatid, current.messages.Where(x=>x.UserId!=id).Count(), id);
            //if (tmpMessages != null)
            //{
            //    chatItems = client.GetChatItems(id);
            //    int i = current.Chatid;
            //    current = chatItems.Where(x => x.Chatid == i).FirstOrDefault();
            //    foreach (var item in tmpMessages)
            //    {
            //        messages.Items.Add(new Mess() { Mes = item.Message, TimeSending = item.SendingTime, ImagePath = current.ImagePath });
            //    }
            //    current.LastMessage = tmpMessages.Last().Message;
            //    scroll.ScrollToEnd();
            //}

        }

        private void sendMsg_TextBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                // если одновременно нажаты Ctrl или Shift - перейти на следующую строку
                if (e.KeyboardDevice.Modifiers == ModifierKeys.Control || e.KeyboardDevice.Modifiers == ModifierKeys.Shift)
                {
                    sendMsg_TextBox.Text += "\n";
                    sendMsg_TextBox.SelectionStart = sendMsg_TextBox.Text.Length;
                }
                // иначе отправить сообщение
                else
                {
                    var r = client.GetBlackListsWithUserAsync(id, current).Result;
                    if (r.Length != 0)
                    {
                        if (r.Length == 1)
                            if (r[0].UserEnemyID == id)
                                MessageBox.Show("Пользователь добавил Вас в Чёрный Список, поэтому Вы не можете писать ему сообщения.");
                            else
                                MessageBox.Show("Вы добавили этого пользователя в Чёрный Список, поэтому не можете писать ему сообщения.");
                        else
                            MessageBox.Show("Вы и этот пользователь добавили друг друга в ЧС, поэтому не можете писать ему сообщения.");
                        return;
                    }
                    if (sendMsg_TextBox.Text != "")
                    {
                        Message message = new Message()
                        {
                            TimeSending = DateTime.Now,
                            Mes = sendMsg_TextBox.Text,
                            IsRecieved=false
                           
                        };
                        chatItems = client.GetChatItems(id);
                        int i = current.Chatid;
                        current = chatItems.Where(x=>x.Chatid==i).FirstOrDefault();

                        messages.Items.Add(new Mymes()
                        {
                            TimeSending = DateTime.Now.ToString("hh:mm tt"),
                            Mes = sendMsg_TextBox.Text
                        });
                        client.SendMes(message,current,id);

                        foreach (var item in lst1.Items)
                        {
                            if (item is Chatlistitem)
                            {
                                if ((item as Chatlistitem).ImagePath.SequenceEqual(current.ImagePath))
                                {
                                    (item as Chatlistitem).last.Text = message.Mes;
                                    break;
                                }
                            }

                        }
                        sendMsg_TextBox.Text = "";
                    }
                }

                e.Handled = true;
            }
        }

        public void OnCallback()
        {
          
        }
        void Getmessage(int chatid, Message message)
        {
            if (!Dispatcher.CheckAccess()) { Dispatcher.Invoke(() => Getmessage(chatid, message)); }
            else
            {
                if (chatid == current.Chatid)
                {
                    messages.Items.Add(new Mess() { TimeSending = message.TimeSending.ToString("hh:mm tt"), Mes = message.Mes, ImagePath = current.ImagePath });
                    foreach (var item in lst1.Items)
                    {
                        if (item is Chatlistitem)
                        {
                            if ((item as Chatlistitem).ImagePath.SequenceEqual(current.ImagePath))
                            {
                                (item as Chatlistitem).last.Text = message.Mes;
                            }
                        }

                    }

                    chatItems = client.GetChatItems(id);

                }
                else
                {
                    chatItems = client.GetChatItems(id);
                }
            }
        }
        public delegate void SendMessage(int chatid, Message tmpmes);
        public void OnSendMessage(int chatid, Message message)
        {
            SendMessage d = new SendMessage(Getmessage);

            d.BeginInvoke(chatid, message, null, null);
        }
    }
}
