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
using System.Windows.Media.Animation;
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
        double width, height;
        int colGoalPos, rowGoalPos, colStartPos, rowStartPos;
        int rowPlayerPos, colPlayerPos;
        int indexInMaze = 0;


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

        public int Cols
        {
            get { return (int)GetValue(ColumnsProperty); }
            set { SetValue(ColumnsProperty, value); }
        }

        public static readonly DependencyProperty ColumnsProperty =
         DependencyProperty.Register("Cols", typeof(int), typeof(MazeBoard), new
        PropertyMetadata(1));

        public string Maze
        {
            get { return (string)GetValue(MazeProperty); }
            set { SetValue(MazeProperty, value); }
        }

        public static readonly DependencyProperty MazeProperty =
        DependencyProperty.Register("Maze", typeof(string), typeof(MazeBoard), new
        PropertyMetadata(default(string)));

        public string InitialPos
        {
            get { return (string)GetValue(InitialPosProperty); }
            set { SetValue(InitialPosProperty, value); }
        }

        public static readonly DependencyProperty InitialPosProperty =
        DependencyProperty.Register("InitialPos", typeof(string), typeof(MazeBoard), new
        PropertyMetadata(default(string)));

        public string GoalPos
        {
            get { return (string)GetValue(GoalPosProperty); }
            set { SetValue(GoalPosProperty, value); }
        }

        public static readonly DependencyProperty GoalPosProperty =
        DependencyProperty.Register("GoalPos", typeof(string), typeof(MazeBoard), new
        PropertyMetadata(default(string)));

        public ImageSource ImageSource
        {
            get { return (ImageSource)GetValue(ImageSourceProperty); }
            set { SetValue(ImageSourceProperty, value); }
        }

        public static readonly DependencyProperty ImageSourceProperty =
         DependencyProperty.Register("ImageSource", typeof(ImageSource), typeof(MazeBoard));
        //, new PropertyMetadata(new BitmapImage(new Uri(@"Images/girl.jpg", UriKind.Relative)))

        public ImageSource ExitImageFile
        {
            get { return (ImageSource)GetValue(ExitImageFileProperty); }
            set { SetValue(ExitImageFileProperty, value); }
        }

        public static readonly DependencyProperty ExitImageFileProperty =
         DependencyProperty.Register("ExitImageFile", typeof(ImageSource), typeof(MazeBoard));

        public MazeBoard()
        {
            InitializeComponent();        
            this.Loaded += MazeBoard_Loaded;
        }

        public void setMazeBoardDatacontext(Object vm)
        {
            this.DataContext = vm;
        }

        private void MazeBoard_Loaded(object sender, RoutedEventArgs e)
        {
            string[] startPos = InitialPos.Split(',');
            rowStartPos = Int32.Parse(startPos[0]);
            colStartPos = Int32.Parse(startPos[1]);
            string[] goalPos = GoalPos.Split(',');
            rowGoalPos = Int32.Parse(goalPos[0]);
            colGoalPos = Int32.Parse(goalPos[1]);

            rowPlayerPos = Int32.Parse(startPos[0]);
            colPlayerPos = Int32.Parse(startPos[1]);

            width = myCanvas.Width / Cols;
            height = myCanvas.Height / Rows;
            int counter = 0;
            for (int i = 0; i < Rows; i++)
            {
                for (int j = 0; j < Cols; j++)
                {
                    if (Maze[counter] == '1')
                    {
                        AddWall(j, i);
                    }
                    if (i == rowStartPos && j == colStartPos)
                    {
                        AddImage(j, i, ImageSource, "Player");
                        indexInMaze = counter;
                    }
                    if (i == rowGoalPos && j == colGoalPos)
                    {
                        AddImage(j, i, ExitImageFile, "Exit");
                    }

                    counter++;

                }
            }

            //setStartUpPoint();
        }

        private void setStartUpPoint()
        {
            clientImage.Height = 150 / Rows;
            clientImage.Width = 150 / Cols;
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
        }

        private void AddWall(int x, int y)
        {
            Rectangle rect = new Rectangle();
            rect.Width = width;
            rect.Height = height;
            rect.Stroke = Brushes.Black;
            rect.StrokeThickness = 3;
            rect.Fill = new SolidColorBrush(Colors.Black);
            Canvas.SetLeft(rect, x*rect.Width);
            Canvas.SetTop(rect, y*rect.Height);
            rect.Name = "Wall";
            myCanvas.Children.Add(rect);

        }

        private void AddImage(int x, int y, ImageSource imageSource, string name)
        {
            Image image = new Image();
            image.Source = imageSource;
            image.Width = width;
            image.Height = height;
            Canvas.SetLeft(image, x*image.Width);
            Canvas.SetTop(image, y*image.Height);
            image.Name = name;
            myCanvas.Children.Add(image);

        }

        public void gridMazeBoard_KeyDown(object sender, KeyEventArgs e)
        {
            int pointer = rowPlayerPos*Rows + colPlayerPos + 1;
            foreach (UIElement child in myCanvas.Children)
            {
                if(((System.Windows.FrameworkElement)child).Name == "Player")
                {
                    clientImage = (Image)child;
                }
            }

            Vector offset = VisualTreeHelper.GetOffset(clientImage);
            var top = offset.Y;
            var left = offset.X;

            switch (e.Key)
            {
                case Key.Up:
                    {
                        if (Maze[indexInMaze - Rows] == '0' && (rowPlayerPos - 1) >= 0)
                        {
                                DoubleAnimation anim1 = new DoubleAnimation(top, top - clientImage.Height, TimeSpan.FromMilliseconds(300));
                                clientImage.BeginAnimation(Canvas.TopProperty, anim1);
                                rowPlayerPos -= 1;
                                indexInMaze -= Rows;
                        }
                        break;
                    }
                case Key.Down:
                    {
                        if (Maze[indexInMaze + Rows] == '0' && (rowPlayerPos + 1) <= Rows)
                        {
                                DoubleAnimation anim1 = new DoubleAnimation(top, top + clientImage.Height, TimeSpan.FromMilliseconds(300));
                                clientImage.BeginAnimation(Canvas.TopProperty, anim1);
                                rowPlayerPos += 1;
                                indexInMaze += Rows;
                        }
                        break;
                    }
                case Key.Right:
                    {
                        if (Maze[indexInMaze + 1] == '0' && (colPlayerPos + 1) <= Cols)
                        {
                            DoubleAnimation anim1 = new DoubleAnimation(left, left + clientImage.Width, TimeSpan.FromMilliseconds(300));
                            clientImage.BeginAnimation(Canvas.LeftProperty, anim1);
                            colPlayerPos += 1;
                            indexInMaze += 1;
                        }
                        break;
                    }
                case Key.Left:
                    {
                        if (Maze[indexInMaze - 1] == '0' && (colPlayerPos - 1) >= 0)
                        {
                            DoubleAnimation anim1 = new DoubleAnimation(left, left - clientImage.Width, TimeSpan.FromMilliseconds(300));
                            clientImage.BeginAnimation(Canvas.LeftProperty, anim1);
                            colPlayerPos -= 1;
                            indexInMaze -= 1;
                        }
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
