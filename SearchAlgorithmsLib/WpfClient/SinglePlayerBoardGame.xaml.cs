using System;
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
        /// <summary>
        /// the single player game viewmodel
        /// </summary>
        SinglePlayerBoardGameViewModel vm;
        /// <summary>
        /// checking if the player won when closing
        /// </summary>
        bool winning;
        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="serverMessage"></param>
        /// <param name="client"></param>
        public SinglePlayerGameBoard(string serverMessage, TelnetSingaleClient client)
        {
            vm = new SinglePlayerBoardGameViewModel(serverMessage, client, this);
            this.DataContext = vm;
            InitializeComponent();
            this.MazeName.SetMazeBoardDatacontext(vm);
            this.Closing += ExitWindow;
            MazeName.YouWonEvent += MyMazeBoard_YouWonEvent;
            winning = false;
        }
        /// <summary>
        /// event for the winning of myself
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MyMazeBoard_YouWonEvent(object sender, EventArgs e)
        {
            winning = true;
            this.Close();
            YouWon youWon = new YouWon();
            youWon.Show();
        }
        /// <summary>
        /// event for pressing the keys
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            MazeName.GridMazeBoard_KeyDown(sender, e);
        }
        /// <summary>
        /// event when clicking the restart game button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RestartGame_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Are you sure you want to restart?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                MazeName.RestartGame(sender, e);
            }
        }

        /// <summary>
        /// event when clicking the solve maze button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SolveMaze_Click(object sender, RoutedEventArgs e)
        {
            MazeName.Solve = true;
            this.Stack.IsEnabled = false;
            //set the client image to start point (the point of the solve path)
            MazeName.RestartGame(sender, e);
            vm.SolveMaze();
        }
        /// <summary>
        /// event when clicking the main menu button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainMenu_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        /// <summary>
        /// the animation of the solve maze command
        /// </summary>
        /// <param name="mazeSolvePath"></param>
        internal void SolveMazeAnimation(string mazeSolvePath)
        {
            foreach (char ch in mazeSolvePath)
            {
                Thread.Sleep(150);
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

        /// if the user wants to exit it will go to the main menu
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ExitWindow(object sender, CancelEventArgs e)
        {
            if (!winning)
            {
                MessageBoxResult result = MessageBox.Show("Are you sure you want to go back to main menu?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    MainWindow win = (MainWindow)Application.Current.MainWindow;
                    win.Show();
                }
                else
                {
                    e.Cancel = true;
                }
            }
        }
    }
}
