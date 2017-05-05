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
    /// Interaction logic for SinglePlayer.xaml
    /// </summary>
    public partial class SinglePlayer : Window
    {
        public SinglePlayer()
        {
            InitializeComponent();
            SingleMenu.btnStart.Click += BtnStart_Click;
        }

        private void BtnStart_Click(object sender, RoutedEventArgs e)
        {
            SinglePlayerGameBoard singlePlayerGameBoard = new SinglePlayerGameBoard();
            singlePlayerGameBoard.Owner = this.Owner;
            singlePlayerGameBoard.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            //TODO add validation
            singlePlayerGameBoard.Show();
            this.Close();

        }
    }
}
