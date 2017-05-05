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

namespace WpfClient.Controls
{
    /// <summary>
    /// Interaction logic for MazeBoardd.xaml
    /// </summary>
    public partial class MazeBoard : UserControl
    {
        Image clientImage = new Image();

        public int Rows
        {
            get { return (int)GetValue(RowsProperty); }
            set { SetValue(RowsProperty, value); }
        }
        // Using a DependencyProperty as the backing store for Rows. This enables animation, styling,
        //binding, etc...
        public static readonly DependencyProperty RowsProperty =
         DependencyProperty.Register("Rows", typeof(int), typeof(MazeBoard), new
        PropertyMetadata(1));

        public int Columns
        {
            get { return (int)GetValue(ColumnsProperty); }
            set { SetValue(ColumnsProperty, value); }
        }

        public static readonly DependencyProperty ColumnsProperty =
         DependencyProperty.Register("Columns", typeof(int), typeof(MazeBoard), new
        PropertyMetadata(1));

        public ImageSource ImageSource
        {
            get { return (ImageSource)GetValue(ImageSourceProperty); }
            set { SetValue(ImageSourceProperty, value); }
        }

        public static readonly DependencyProperty ImageSourceProperty =
         DependencyProperty.Register("ImageSource", typeof(ImageSource), typeof(MazeBoard));
        //, new PropertyMetadata(new BitmapImage(new Uri(@"Images/girl.jpg", UriKind.Relative)))

        public MazeBoard()
        {
            InitializeComponent();
            this.Loaded += MazeBoard_Loaded;
        }

        private void MazeBoard_Loaded(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < Rows; i++)
            {
                gridMazeBoard.RowDefinitions.Add(new RowDefinition());
                gridMazeBoard.RowDefinitions[i].Height = new GridLength(1, GridUnitType.Star);
                gridMazeBoard.RowDefinitions[i].Focusable = true;
            }

            for (int i = 0; i < Columns; i++)
            {
                gridMazeBoard.ColumnDefinitions.Add(new ColumnDefinition());
            }

            setStartUpPoint();
        }

        private void setStartUpPoint()
        {
            clientImage.Height = 150 / Rows;
            clientImage.Width = 150 / Columns;
            if (this.ImageSource == null)
            {
                var uriSource = new Uri(@"Images/girl.jpg", UriKind.Relative);
                clientImage.Source = new BitmapImage(uriSource);
            }
            else
            {
                clientImage.Source = this.ImageSource;
            }
            Grid.SetRow(clientImage, 0);
            Grid.SetColumn(clientImage, 0);
            //TODO : clientImage.Stretch = Stretch;
            gridMazeBoard.Children.Add(clientImage);
        }

        public void gridMazeBoard_KeyDown(object sender, KeyEventArgs e)
        {
            int currentRow, currentColumn;
            switch (e.Key)
            {
                case Key.Up:
                    {
                        if ((currentRow = Grid.GetRow(clientImage)) > 0)
                            Grid.SetRow(clientImage, currentRow - 1);
                        break;
                    }
                case Key.Down:
                    {
                        if ((currentRow = Grid.GetRow(clientImage)) < Rows - 1)
                            Grid.SetRow(clientImage, currentRow + 1);
                        break;
                    }
                case Key.Right:
                    {
                        if ((currentColumn = Grid.GetColumn(clientImage)) < Columns - 1)
                            Grid.SetColumn(clientImage, currentColumn + 1);
                        break;
                    }
                case Key.Left:
                    {
                        if ((currentColumn = Grid.GetColumn(clientImage)) > 0)
                            Grid.SetColumn(clientImage, currentColumn - 1);
                        break;
                    }
                default: break;
            }
        }

        //var uriSource = new Uri(@"images/girl.jpg", UriKind.Relative);
        //clientImage.Source = new BitmapImage(uriSource); 
        
        //Image a = new Image();
        //clientImage.Height = 150 / Rows;
        //clientImage.Width = 150 / Columns;
        ////var uriSource = new Uri(@"images/girl.jpg", UriKind.Relative);
        //a.Source = this.ImageSource; //new BitmapImage(uriSource);
        //Grid.SetRow(a, 1);
        //Grid.SetColumn(a, 0);
        //gridMazeBoard.Children.Add(a);


        //Button c = new Button();
        //clientImage.Height = 150 / Rows;
        //clientImage.Width = 150 / Columns;
        //c.Content = "kkkkkkk";
        //Grid.SetRow(c, 0);
        //Grid.SetColumn(c, 1);
        //gridMazeBoard.Children.Add(c);

    }
}
