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
    /// Interaction logic for Settings.xaml
    /// </summary>
    public partial class Settings : Window
    {
        /// <summary>
        /// the settings viewmodel
        /// </summary>
        private SettingsViewModel vm;
        /// <summary>
        /// Ctor
        /// </summary>
        public Settings()
        {
            InitializeComponent();
            vm = new SettingsViewModel(new ApplicationSettingsModel());
            this.DataContext = vm;
        }
        /// <summary>
        /// event for clicking the ok button in the settings window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            vm.SaveSettings();
            MainWindow win = (MainWindow)Application.Current.MainWindow;
            win.Show();
            this.Close();
        }
        /// <summary>
        /// event for clicking the cancel button in the settings window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            MainWindow win = (MainWindow)Application.Current.MainWindow;
            win.Show();
            this.Close();
        }
    }
}
