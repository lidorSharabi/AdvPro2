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
        private MultiPlayerGameBoardViewModel vm;

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

        private void OpponentMazeBoard_YouWonEvent(object sender, EventArgs e)
        {
            this.Close();
            //TODO - replace to "you lose" window
            YouWon youWon = new YouWon();
            youWon.Show();
        }

        private void MyMazeBoard_YouWonEvent(object sender, EventArgs e)
        {
            this.Close();
            YouWon youWon = new YouWon();
            youWon.Show();
        }

        private void MultiPlayerGameBoard_Closing(object sender, EventArgs e)
        {
            vm.CloseGame();
        }

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

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            string move = e.Key.ToString().ToLower();
            if (move.Equals("up") || move.Equals("down") || move.Equals("left") || move.Equals("right"))
            {
                vm.Move(move);
                MyMazeBoard.gridMazeBoard_KeyDown(sender, e);
            }
        }

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
