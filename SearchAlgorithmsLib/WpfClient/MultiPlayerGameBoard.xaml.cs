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
        private string result;
        private MultiPlayerGameBoardViewModel vm;

        public MultiPlayerGameBoard(string result, TelnetMultiClient client)
        {
            this.result = result;
            this.DataContext = vm;
            vm = new MultiPlayerGameBoardViewModel(result, client, this);
            InitializeComponent();
            this.MyMazeBoard.setMazeBoardDatacontext(vm);
            this.OpponentMazeBoard.setMazeBoardDatacontext(vm);
            KeepConnectionOpen();
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
            OpponentMoveAnimation(serverMessageMove);
        }

        private void RestartGame_Click(object sender, RoutedEventArgs e)
        {

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
            MyMazeBoard.gridMazeBoard_KeyDown(sender, e);
            vm.Move(e.Key.ToString().ToLower());
        }

        internal void OpponentMoveAnimation(string move)
        {
            Thread.Sleep(200);
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
