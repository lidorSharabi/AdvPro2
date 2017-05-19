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
using System.Windows.Shapes;
using WpfClient.Controls;

namespace WpfClient
{
    /// <summary>
    /// Interaction logic for SinglePlayerBoardGame.xaml
    /// </summary>
    public partial class SinglePlayerGameBoard : Window
    {
        private string serverMessage;
        SinglePlayerBoardGameViewModel vm;

        public SinglePlayerGameBoard(string serverMessage, string name , string rows, string columns)
        {
            this.serverMessage = serverMessage;
            vm = new SinglePlayerBoardGameViewModel(serverMessage, name, rows, columns);
            this.DataContext = vm;
            InitializeComponent();
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            MazeName.gridMazeBoard_KeyDown(sender, e);
        }

        private void RestartGame_Click(object sender, RoutedEventArgs e)
        {
            vm.RestartGame();
        }

        private void SolveMaze_Click(object sender, RoutedEventArgs e)
        {
            vm.SolveMaze();
            string maze = MazeName.Maze;
            foreach (char c in maze)
            {
                //switch (c)
                //{
                //    case '1':
                //        this.MazeName.gridMazeBoard_KeyDown(sender, );
                //        break;
                //}
            }
        }

        private void MainMenu_Click(object sender, RoutedEventArgs e)
        {
            MainWindow win = (MainWindow)Application.Current.MainWindow;
            win.Show();
            this.Close();
        }
    }
}
