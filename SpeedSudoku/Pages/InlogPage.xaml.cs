﻿using SpeedSudoku;
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

namespace WPFTraining.Pages
{
    /// <summary>
    /// Interaction logic for InlogPage.xaml
    /// </summary>
    public partial class InlogPage : Page
    {
        public MainWindow main;
        public InlogPage(MainWindow main)
        {
            this.main = main;
            InitializeComponent();
        }

        public void SubmitUsernameButton_Click(object sender, RoutedEventArgs e)
        {
            this.main.switchToSudoku();
        }
    }
}