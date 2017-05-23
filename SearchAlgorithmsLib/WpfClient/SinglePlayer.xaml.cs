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
using System.ComponentModel;

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
            client.connect(Properties.Settings.Default.ServerIP, Properties.Settings.Default.ServerPort);
            client.Generate(SingleMenu.txtMazeName.Text, SingleMenu.txtRows.Text, SingleMenu.txtCols.Text);
            Task<string> t = Task.Factory.StartNew(() => { return client.read(); });
            t.ContinueWith(Generate_Raed_OnComplited);
        }

        private void Generate_Raed_OnComplited(Task<string> obj)
        {
            this.Dispatcher.Invoke(() =>
            {
                SinglePlayerGameBoard singlePlayerGameBoard = new SinglePlayerGameBoard(obj.Result, client)
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
