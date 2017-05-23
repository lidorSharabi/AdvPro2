﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using WpfClient.Controls;

namespace WpfClient
{
    /// <summary>
    /// Interaction logic for SinglePlayerBoardGame.xaml
    /// </summary>
    public partial class SinglePlayerGameBoard : Window
    {
        SinglePlayerBoardGameViewModel vm;

        public SinglePlayerGameBoard(string serverMessage, TelnetSingaleClient client)
        {
            vm = new SinglePlayerBoardGameViewModel(serverMessage, client, this);
            this.DataContext = vm;
            InitializeComponent();
            this.MazeName.setMazeBoardDatacontext(vm);
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            MazeName.gridMazeBoard_KeyDown(sender, e);
        }

        private void RestartGame_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Are you sure you want to restart?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                MazeName.RestartGame(sender, e);
            }
        }


        private void SolveMaze_Click(object sender, RoutedEventArgs e)
        {

            //set the client image to start point (the point of the solve path)
            this.Stack.IsEnabled = false;
            MazeName.RestartGame(sender, e);
            vm.SolveMaze();
        }

        private void MainMenu_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Are you sure you want to go back to main menu?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                MainWindow win = (MainWindow)Application.Current.MainWindow;
                win.Show();
                this.Close();
            }
        }

        internal void SolveMazeAnimation(string mazeSolvePath)
        {
            foreach (char ch in mazeSolvePath)
            {
                Thread.Sleep(300);
                MazeBoard.Moves move = MazeBoard.Moves.Default;
                switch (ch)
                {
                    case '0': move = MazeBoard.Moves.Left;
                        break;
                    case '1':
                        move = MazeBoard.Moves.Right;
                        break;
                    case '2':
                        move = MazeBoard.Moves.Up;
                        break;
                    case '3':
                        move = MazeBoard.Moves.Down;
                        break;
                }
                MazeName.MoveAnimation(move);
            }
            this.Dispatcher.Invoke((Action)(() =>
            {
                this.Stack.IsEnabled = true;
            }));
        }
    }
}
