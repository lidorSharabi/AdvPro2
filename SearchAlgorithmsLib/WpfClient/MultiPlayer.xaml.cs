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
using System.ComponentModel;

namespace WpfClient
{
    /// <summary>
    /// Interaction logic for MultiPlayer.xaml
    /// </summary>
    /// 
    public partial class MultiPlayer : Window
    {
        MenuViewModel vm;
        TelnetMultiClient client = new TelnetMultiClient();
        bool gameStarted;

        public MultiPlayer()
        {
            client.connect(Properties.Settings.Default.ServerIP, Properties.Settings.Default.ServerPort);
            InitializeComponent();
            MultiMenu.btnStart.Click += BtnStart_Click;
            JoinMazeButton.Click += JoinMazeButton_Click;
            this.Closing += ExitWindow;
            vm = new MenuViewModel(new MenuModel(client));
            this.DataContext = vm;
            gameStarted = false;
        }

        private void OnComboboxOpen(object sender, EventArgs e)
        {
            vm.ListMaze();
        }

        private void JoinMazeButton_Click(object sender, RoutedEventArgs e)
        {
            //client.connect(Properties.Settings.Default.ServerIP, Properties.Settings.Default.ServerPort);
            client.Join(JoinMazesNames.SelectedValue.ToString());
            Task<string> t = Task.Factory.StartNew(() => { return client.read(); });
            t.ContinueWith(StartOrJoin_Read_OnComplited);
        }

        private void BtnStart_Click(object sender, RoutedEventArgs e)
        {
           // client.connect(Properties.Settings.Default.ServerIP, Properties.Settings.Default.ServerPort);
            client.Start(MultiMenu.txtMazeName.Text, MultiMenu.txtRows.Text, MultiMenu.txtCols.Text);
            Task<string> t = Task.Factory.StartNew(() => { return client.read(); });
            t.ContinueWith(StartOrJoin_Read_OnComplited);
        }

        private void StartOrJoin_Read_OnComplited(Task<string> obj)
        {
            this.Dispatcher.Invoke(() =>
            {
                if(obj.Result == "Waiting for other player to join...")
                {

                }
                MultiPlayerGameBoard multiPlayerGameBoard = new MultiPlayerGameBoard(obj.Result, client)
                {
                    Owner = this.Owner,
                    WindowStartupLocation = WindowStartupLocation.CenterOwner
                };
                //TODO add validation
                multiPlayerGameBoard.Show();
                gameStarted = true;
                this.Close();
            });
        }

        private void ExitWindow(object sender, CancelEventArgs e)
        {
            if (!gameStarted)
            {
                MainWindow win = (MainWindow)Application.Current.MainWindow;
                win.Show();
            }
        }

    }
}
