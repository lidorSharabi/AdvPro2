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
        WaitingWindow waitingWindow;
        public MultiPlayer()
        {
            client.connect(Properties.Settings.Default.ServerIP, Properties.Settings.Default.ServerPort);
            InitializeComponent();
            MultiMenu.btnStart.Click += BtnStart_Click;
            MenuViewModel vm = new MenuViewModel(new MenuModel(client));
            this.DataContext = vm;
            vm.ListMaze();
        }

        private void JoinMazeButton_Click(object sender, RoutedEventArgs e)
        {
            //client.connect(Properties.Settings.Default.ServerIP, Properties.Settings.Default.ServerPort);
            string selectedMazeName = JoinMazesNames.SelectedValue.ToString();
            client.Join(selectedMazeName);
            Task<string> t = Task.Factory.StartNew(() => { return client.read(); });
            t.ContinueWith(Join_Read_OnComplited);
        }

        private void BtnStart_Click(object sender, RoutedEventArgs e)
        {
            // client.connect(Properties.Settings.Default.ServerIP, Properties.Settings.Default.ServerPort);
            client.Start(MultiMenu.txtMazeName.Text, MultiMenu.txtRows.Text, MultiMenu.txtCols.Text);
            Task<string> t = Task.Factory.StartNew(() => { return client.read(); });
            t.ContinueWith(Start_Read_OnComplited);
        }
        private void Start_Read_OnComplited(Task<string> obj)
        {
            Task<string> t2 = Task.Factory.StartNew(() => { return client.read1(); });
            t2.ContinueWith(ClientJoined_OnComplited);

            this.Dispatcher.Invoke(() =>
            {
                if (obj.Result == "Waiting for other player to join...")
                {
                    this.Hide();
                    waitingWindow = new WaitingWindow()
                    {
                        Owner = this.Owner,
                        WindowStartupLocation = WindowStartupLocation.CenterOwner
                    };
                    waitingWindow.Show();
                }
            });

        }

        private void ClientJoined_OnComplited(Task<string> obj)
        {
            Join_Or_ClientJoined_OnComplited(obj.Result);
            this.Dispatcher.Invoke(() =>
            {
                waitingWindow.Close();
            });
        }

        private void Join_Read_OnComplited(Task<string> obj)
        {
            Join_Or_ClientJoined_OnComplited(obj.Result);
        }

        void Join_Or_ClientJoined_OnComplited(string serverResponse)
        {
            this.Dispatcher.Invoke(() =>
            {
                MultiPlayerGameBoard multiPlayerGameBoard = new MultiPlayerGameBoard(serverResponse, client)
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
