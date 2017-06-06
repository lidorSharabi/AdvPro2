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
        /// <summary>
        /// the multiplayer view model
        /// </summary>
        MenuViewModel vm;
        /// <summary>
        /// the client
        /// </summary>
        TelnetMultiClient client = new TelnetMultiClient();
        /// <summary>
        /// checking if the user started a game
        /// </summary>
        bool gameStarted;
        /// <summary>
        /// the waiting window after the start was pressed
        /// </summary>
        WaitingWindow waitingWindow;
        /// <summary>
        /// Ctor
        /// </summary>
        public MultiPlayer()
        {
            client.Connect(Properties.Settings.Default.ServerIP, Properties.Settings.Default.ServerPort);
            InitializeComponent();
            MultiMenu.btnStart.Click += BtnStart_Click;
            this.Closing += ExitWindow;
            vm = new MenuViewModel(new MenuModel(client));
            this.DataContext = vm;
            vm.ListMaze();
            gameStarted = false;
        }
       /// <summary>
       /// event for clicking on the join game button
       /// </summary>
       /// <param name="sender"></param>
       /// <param name="e"></param>
        private void JoinMazeButton_Click(object sender, RoutedEventArgs e)
        {
            if (this.JoinMazesNames.Text.Equals(""))
            {
                MessageBoxResult result = MessageBox.Show("Please choose game from the list", "Error", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                client.Join(JoinMazesNames.SelectedValue.ToString());
                Task<string> t = Task.Factory.StartNew(() => { return client.Read(); });
                t.ContinueWith(Join_Read_OnComplited);
            }
        }
        /// <summary>
        /// event for clicking on the start game button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnStart_Click(object sender, RoutedEventArgs e)
        {
            if (this.MultiMenu.txtCols.Text.Equals("") || this.MultiMenu.txtRows.Text.Equals(""))
            {
                MessageBoxResult result = MessageBox.Show("Please enter columns and rows", "Error", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                client.Start(MultiMenu.txtMazeName.Text, MultiMenu.txtRows.Text, MultiMenu.txtCols.Text);
                Task<string> t = Task.Factory.StartNew(() => { return client.Read(); });
                t.ContinueWith(Start_Read_OnComplited);
            }
        }
        /// <summary>
        /// after we press start we display a waiting screen
        /// </summary>
        /// <param name="obj"></param>
        private void Start_Read_OnComplited(Task<string> obj)
        {
            Task<string> t2 = Task.Factory.StartNew(() => { return client.WatingForJoin(); });
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
        /// <summary>
        /// after we get the maze we continue to display the multiplayer screen
        /// </summary>
        /// <param name="obj"></param>
        private void ClientJoined_OnComplited(Task<string> obj)
        {
            Join_Or_ClientJoined_OnComplited(obj.Result);
            this.Dispatcher.Invoke(() =>
            {
                waitingWindow.Close();
            });
        }
        /// <summary>
        /// after we get the maze we continue to display the multiplayer screen
        /// </summary>
        /// <param name="obj"></param>
        private void Join_Read_OnComplited(Task<string> obj)
        {
            Join_Or_ClientJoined_OnComplited(obj.Result);
        }
        /// <summary>
        /// after we get the maze we continue to display the multiplayer screen
        /// </summary>
        /// <param name="obj"></param>
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
                gameStarted = true;
                this.Close();
            });
        }
        /// <summary>
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
