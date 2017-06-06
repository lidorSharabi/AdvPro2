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
        /// <summary>
        /// event restarter
        /// </summary>
        ManualResetEvent d = new ManualResetEvent(false);
        /// <summary>
        /// the client
        /// </summary>
        TelnetSingaleClient client = new TelnetSingaleClient();
        /// <summary>
        /// checking if the user started a game
        /// </summary>
        bool gameStarted;
        /// <summary>
        /// Ctor
        /// </summary>
        public SinglePlayer()
        {
            InitializeComponent();
            SingleMenu.btnStart.Click += BtnStart_Click;
            this.Closing += ExitWindow;
            gameStarted = false;
        }
        /// <summary>
        /// event for clicking on the start game button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnStart_Click(object sender, RoutedEventArgs e)
        {
            if (this.SingleMenu.txtCols.Text.Equals("") || this.SingleMenu.txtRows.Text.Equals(""))
            {
                MessageBoxResult result = MessageBox.Show("Please enter columns and rows", "Error", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                client.Connect(Properties.Settings.Default.ServerIP, Properties.Settings.Default.ServerPort);
                client.Generate(SingleMenu.txtMazeName.Text, SingleMenu.txtRows.Text, SingleMenu.txtCols.Text);
                Task<string> t = Task.Factory.StartNew(() => { return client.Read(); });
                t.ContinueWith(Generate_Raed_OnComplited);
            }
        }
        /// <summary>
        /// after we press start we display a waiting screen
        /// </summary>
        /// <param name="obj"></param>
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
                gameStarted = true;
                this.Close();
            });
        }
        /// if the user wants to exit it will go to the main menu
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
