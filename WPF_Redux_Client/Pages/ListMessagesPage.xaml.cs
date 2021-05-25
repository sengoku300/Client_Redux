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
    /// Interaction logic for ListMessagesPage.xaml
    /// </summary>
    public partial class ListMessagesPage : Page
    {
        public ListMessagesPage()
        {
            InitializeComponent();
           
           
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
                       // Send(sendMsg_TextBox.Text);
                        sendMsg_TextBox.Text = "";
                    }
                }

                e.Handled = true;
            }
        }
    }
}
