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
using System.Windows.Shapes;

namespace WpfClient
{
    /// <summary>
    /// Interaction logic for MultiPlayer.xaml
    /// </summary>
    public partial class MultiPlayer : Window
    {
        public MultiPlayer()
        {
            InitializeComponent();
            SingleMenu.btnStart.Click += BtnStart_Click;
        }

        private void BtnStart_Click(object sender, RoutedEventArgs e)
        {
            MultiPlayerGameBoard multiPlayerGameBoard = new MultiPlayerGameBoard();
            multiPlayerGameBoard.Owner = this.Owner;
            multiPlayerGameBoard.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            //TODO add validation
            multiPlayerGameBoard.Show();
            this.Close();
        }
    }
}
