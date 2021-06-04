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

namespace WPF_Redux_Client.CustomControls
{
    /// <summary>
    /// Логика взаимодействия для Chatlistitem.xaml
    /// </summary>
    public partial class Chatlistitem : UserControl
    {
        public Chatlistitem()
        {
            InitializeComponent();
        }
        public Chatlistitem(string name, string lastmes, byte[] imagePath)
        {
           
            InitializeComponent();
            NameUser = name;
            ImagePath = imagePath;
            last.Text = lastmes;
        }
        public byte[] ImagePath { get; set; }
        public string NameUser { get; set; }
       
    }
}
