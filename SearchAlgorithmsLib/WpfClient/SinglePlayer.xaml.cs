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
using System.Configuration;

namespace WpfClient
{
    /// <summary>
    /// Interaction logic for SinglePlayer.xaml
    /// </summary>
    public partial class SinglePlayer : Window
    {

        ManualResetEvent d = new ManualResetEvent(false);

        TelnetSingaleClient client = new TelnetSingaleClient();

        public SinglePlayer()
        {
            InitializeComponent();
            SingleMenu.btnStart.Click += BtnStart_Click;
        }

        private void BtnStart_Click(object sender, RoutedEventArgs e)
        {
            client.connect("127.0.0.1", Int32.Parse(ConfigurationManager.AppSettings["PortNumber"]));
            client.ServerMessageArrivedEvent += SinglePlayer_ServerMessageArrived;
            client.write(String.Format("generate {0} {1} {2}",SingleMenu.txtMazeName.Text, SingleMenu.txtRows.Text, SingleMenu.txtCols.Text));
            //client.ServerMessageArrivedEvent(3, new EventArgs());
        }

        private void SinglePlayer_ServerMessageArrived(object sender, EventArgs e)
        {
            this.Dispatcher.Invoke(() =>
            {
                SinglePlayerGameBoard singlePlayerGameBoard = new SinglePlayerGameBoard(client.ServerMessage, SingleMenu.txtMazeName.Text, SingleMenu.txtRows.Text, SingleMenu.txtCols.Text)
                {
                    Owner = this.Owner,
                    WindowStartupLocation = WindowStartupLocation.CenterOwner
                };

                //TODO add validation
                singlePlayerGameBoard.Show();
                this.Close();
            });
        }
        
    }
}
