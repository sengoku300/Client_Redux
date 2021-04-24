﻿using System;
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
using WPF_Redux_Client.Pages;

namespace WPF_Redux_Client
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var itemTab = (TabItem)TabControls.SelectedItem;

            switch (itemTab.Name)
            {
                case "chat":
                    frame.Navigate(new ListMessagesPage());
                    break;
                case "exit":
                    Authorization authorization = new Authorization();
                    authorization.Show();
                    this.Close();
                    break;
                case "my_account":
                    break;
                case "feed":
                    break;
                case "filters":
                    frame.Navigate(new FiltersPage());
                    break;
                case "likes":
                    frame.Navigate(new LikesPages());
                    break;
                default:
                    break;
            }
        }
    }
}
