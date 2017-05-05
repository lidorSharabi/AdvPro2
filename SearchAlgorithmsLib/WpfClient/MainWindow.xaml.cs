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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfClient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void SingleButtonClick(object sender, RoutedEventArgs e)
        {
            SinglePlayer singlePlayer = new SinglePlayer();
            singlePlayer.Owner = this;
            singlePlayer.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            singlePlayer.ShowDialog();
        }

        private void MultiButtonClick(object sender, RoutedEventArgs e)
        {
            MultiPlayer multiPlayer = new MultiPlayer();
            multiPlayer.Owner = this;
            multiPlayer.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            multiPlayer.ShowDialog();

        }

        private void Settings_Click(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
            //SettingsWindow settingsWindow = new SettingsWindow();
            //settingsWindow.Owner = this;
            //settingsWindow.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            //settingsWindow.ShowDialog();
        }
    }
}
