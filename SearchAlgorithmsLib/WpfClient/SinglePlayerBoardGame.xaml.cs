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

namespace WpfClient
{
    /// <summary>
    /// Interaction logic for SinglePlayerBoardGame.xaml
    /// </summary>
    public partial class SinglePlayerGameBoard : Window
    {
        public SinglePlayerGameBoard()
        {
            InitializeComponent();
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            //MazeBoard.gridMazeBoard_KeyDown(sender, e);
        }

        private void RestartGame_Click(object sender, RoutedEventArgs e)
        {

        }

        private void SolveMaze_Click(object sender, RoutedEventArgs e)
        {

        }

        private void MainMenu_Click(object sender, RoutedEventArgs e)
        {
            MainWindow win = (MainWindow)Application.Current.MainWindow;
            win.Show();
            this.Close();
        }
    }
}
