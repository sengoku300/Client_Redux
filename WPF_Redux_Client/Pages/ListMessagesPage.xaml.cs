using System;
using System.Collections.Generic;
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
    /// Interaction logic for ListMessagesPage.xaml
    /// </summary>
    public partial class ListMessagesPage : Page,IService1Callback
    {
        //class Header
        //{
        //    public int MyProperty { get; set; }
        //    public int MyProperty { get; set; }
        //    public int MyProperty { get; set; }
        //}
        int id;
        Service1Client client;
        TmpChatItem[] chatItems;
        TmpChatItem current;
        public ListMessagesPage(int userid)
        {
            InitializeComponent();
            tmpborder.Visibility = Visibility.Collapsed;
            id = userid;
            Chatlistitem chatlistitem=   new Chatlistitem("Bogdan", "hello", null);
            chatlistitem.MouseDown += Chatlistitem_MouseDown;
            lst1.Items.Add(chatlistitem);


            IService1Callback callback = this as IService1Callback;

            InstanceContext context = new InstanceContext(callback);

            client = new Service1Client(context);
            id = userid;
            chatItems = client.GetChatItems(userid);
            //client.Getcall("dsafdsfsd@dfdfs.dsa");
            if (chatItems != null)
            {
                foreach (var item in chatItems)
                {
                    lst1.Items.Add(new Chatlistitem() { Name = item.Title, Lastmes = item.LastMessage, ImagePath = item.ImagePath });
                }

                foreach (var item in chatItems)
                {
                    current = item;
                    foreach (var m in item.messages)
                    {
                        if (m.user.UserId != userid)
                        {
                            messages.Items.Add(new Mess() { Mes = m.Mes, ImagePath = item.ImagePath, TimeSending = m.TimeSending.ToString("hh:mm tt") });
                        }
                        else
                        {
                            messages.Items.Add(new Mymes() { Mes = m.Mes, TimeSending = m.TimeSending.ToString("hh:mm tt") });
                        }
                    }
                    break;
                }
            }
            else
            {
                tmpborder.Visibility = Visibility.Visible;
            }

        }

        private void Chatlistitem_MouseDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                Chatlistitem chatlistitem = sender as Chatlistitem;
                messages.Items.Clear();
                current = chatItems.Where(x => x.LastMessage == chatlistitem.Lastmes && x.Title == chatlistitem.NameUser).FirstOrDefault();
                foreach (var m in current.messages)
                {

                    if (m.user.UserId != id)
                    {
                        messages.Items.Add(new Mess() { Mes = m.Mes, ImagePath = current.ImagePath, TimeSending = m.TimeSending.ToString("hh:mm tt") });
                    }
                    else
                    {
                        messages.Items.Add(new Mymes() { Mes = m.Mes, TimeSending = m.TimeSending.ToString("hh:mm tt") });
                    }

                }
            }
            catch
            {

            }


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
                    if (sendMsg_TextBox.Text != "")
                    {
                        Message message = new Message()
                        {
                            TimeSending = DateTime.Now,
                            Mes = sendMsg_TextBox.Text,
                            IsRecieved=false
                           
                        };
                        client.SendMes(message,current,id);

                        messages.Items.Add(new Mymes() { 
                            TimeSending=DateTime.Now.ToString("hh:mm tt"),
                            Mes=sendMsg_TextBox.Text
                        });
                        sendMsg_TextBox.Text = "";
                        
                        
                    }
                }

                e.Handled = true;
            }
        }

       

        private void lst1_MouseDown(object sender, MouseButtonEventArgs e)
        {
            current = (TmpChatItem)(ItemsControl.ContainerFromElement(sender as ItemsControl, e.OriginalSource as DependencyObject) as Chatlistitem).Content;
            messages.Items.Clear();
        }

        public void OnCallback()
        {
          
        }

        public void OnSendMessage(int chatid, Message message)
        {
            if (!Dispatcher.CheckAccess()) Dispatcher.Invoke(() => OnSendMessage(chatid, message));
            else MessageBox.Show("chat123");
        }
    }
}
