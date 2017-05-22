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
using System.Configuration;

namespace WpfClient
{
    /// <summary>
    /// Interaction logic for MultiPlayer.xaml
    /// </summary>
    public partial class MultiPlayer : Window
    {

        TelnetMultiClient client = new TelnetMultiClient();

        public MultiPlayer()
        {
            InitializeComponent();
            MultiMenu.btnStart.Click += BtnStart_Click;
        }

        private void BtnStart_Click(object sender, RoutedEventArgs e)
        {
            client.connect(Properties.Settings.Default.ServerIP, Properties.Settings.Default.ServerPort);
            client.Start(MultiMenu.txtMazeName.Text, MultiMenu.txtRows.Text, MultiMenu.txtCols.Text);
            //MultiPlayerGameBoard multiPlayerGameBoard = new MultiPlayerGameBoard();
            //multiPlayerGameBoard.Owner = this.Owner;
            //multiPlayerGameBoard.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            //TODO add validation
            //multiPlayerGameBoard.Show();
            this.Close();
        }
    }
}
