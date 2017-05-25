using System;
using System.Collections.Generic;
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
    /// Interaction logic for MultiPlayerGameBoard.xaml
    /// </summary>
    public partial class MultiPlayerGameBoard : Window
    {
        /// <summary>
        /// the viewmodel of the multiplayer
        /// </summary>
        private MultiPlayerGameBoardViewModel vm;
        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="result"></param>
        /// <param name="client"></param>
        public MultiPlayerGameBoard(string result, TelnetMultiClient client)
        {
            this.Closing += MultiPlayerGameBoard_Closing;
            vm = new MultiPlayerGameBoardViewModel(result, client, this);
            this.DataContext = vm;
            InitializeComponent();
            MyMazeBoard.YouWonEvent += MyMazeBoard_YouWonEvent;
            OpponentMazeBoard.YouWonEvent += OpponentMazeBoard_YouWonEvent;
                
            this.MyMazeBoard.setMazeBoardDatacontext(vm);
            this.OpponentMazeBoard.setMazeBoardDatacontext(vm);
            KeepConnectionOpen();
        }
        /// <summary>
        /// event for the winnig of the opponent
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OpponentMazeBoard_YouWonEvent(object sender, EventArgs e)
        {
            this.Close();
            //TODO - replace to "you lose" window
            YouWon youWon = new YouWon();
            youWon.Show();
        }
        /// <summary>
        /// event for the winning of myself
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MyMazeBoard_YouWonEvent(object sender, EventArgs e)
        {
            this.Close();
            YouWon youWon = new YouWon();
            youWon.Show();
        }
        /// <summary>
        /// closing the game
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MultiPlayerGameBoard_Closing(object sender, EventArgs e)
        {
            vm.CloseGame();
        }
        /// <summary>
        /// keep reading while playing
        /// </summary>
        private void KeepConnectionOpen()
        {
            new Task(() =>
            {
                while (vm.Continue())
                {
                    OpponentMoved_OnComplited(vm.Read());
                }
            }).Start();
        }
        /// <summary>
        /// the move of the opponent
        /// </summary>
        /// <param name="serverMessageMove"></param>
        private void OpponentMoved_OnComplited(string serverMessageMove)
        {
            if (serverMessageMove.Equals("closed"))
            {
                vm.CloseGame();
                MainWindow win = (MainWindow)Application.Current.MainWindow;
                win.Show();
            }
            else
            {
                OpponentMoveAnimation(serverMessageMove);
            }
        }
        /// <summary>
        /// event for clicking the back to main menu button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BackToMainMenu_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Are you sure you want to go back to main menu?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                MainWindow win = (MainWindow)Application.Current.MainWindow;
                win.Show();
                this.Close();
            }
        }
        /// <summary>
        /// event for pressing the keys
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            string move = e.Key.ToString().ToLower();
            if (move.Equals("up") || move.Equals("down") || move.Equals("left") || move.Equals("right"))
            {
                vm.Move(move);
                MyMazeBoard.gridMazeBoard_KeyDown(sender, e);
            }
        }
        /// <summary>
        /// moving the player in the opponent board
        /// </summary>
        /// <param name="move"></param>
        internal void OpponentMoveAnimation(string move)
        {
            MazeBoard.Moves keyMove = MazeBoard.Moves.Default;
            switch (move)
            {
                case "left":
                    keyMove = MazeBoard.Moves.Left;
                    break;
                case "right":
                    keyMove = MazeBoard.Moves.Right;
                    break;
                case "up":
                    keyMove = MazeBoard.Moves.Up;
                    break;
                case "down":
                    keyMove = MazeBoard.Moves.Down;
                    break;
            }
            OpponentMazeBoard.MoveAnimation(keyMove);
        }

    }
}
