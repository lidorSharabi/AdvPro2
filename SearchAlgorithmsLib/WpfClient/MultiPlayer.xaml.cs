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
    /// 
    public partial class MultiPlayer : Window
    {

        TelnetMultiClient client = new TelnetMultiClient();

        public MultiPlayer()
        {
            InitializeComponent();
            MultiMenu.btnStart.Click += BtnStart_Click;
            JoinMazeButton.Click += JoinMazeButton_Click;
        }

        private void JoinMazeButton_Click(object sender, RoutedEventArgs e)
        {
            client.connect(Properties.Settings.Default.ServerIP, Int32.Parse(ConfigurationManager.AppSettings["PortNumber"]));
            client.Join(JoinMazesNames.SelectedValue.ToString());
            Task<string> t = Task.Factory.StartNew(() => { return client.read(); });
            t.ContinueWith(StartOrJoin_Read_OnComplited);
        }

        private void BtnStart_Click(object sender, RoutedEventArgs e)
        {
            client.connect(Properties.Settings.Default.ServerIP, Int32.Parse(ConfigurationManager.AppSettings["PortNumber"]));
            client.Start(MultiMenu.txtMazeName.Text, MultiMenu.txtRows.Text, MultiMenu.txtCols.Text);
            Task<string> t = Task.Factory.StartNew(() => { return client.read(); });
            t.ContinueWith(StartOrJoin_Read_OnComplited);
        }

        private void StartOrJoin_Read_OnComplited(Task<string> obj)
        {
            this.Dispatcher.Invoke(() =>
            {
                MultiPlayerGameBoard multiPlayerGameBoard = new MultiPlayerGameBoard(obj.Result, client)
                {
                    Owner = this.Owner,
                    WindowStartupLocation = WindowStartupLocation.CenterOwner
                };
                //TODO add validation
                multiPlayerGameBoard.Show();
                this.Close();
            });
        }
    }
}
