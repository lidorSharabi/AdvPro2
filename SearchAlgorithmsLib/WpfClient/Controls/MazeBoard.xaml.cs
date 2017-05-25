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
using System.Threading;

namespace WpfClient.Controls
{
    /// <summary>
    /// Interaction logic for MazeBoardd.xaml
    /// </summary>
    public partial class MazeBoard : UserControl
    {
        public event EventHandler YouWonEvent;

        protected virtual void OnYouWonEventEvent(EventArgs args)
        {
            YouWonEvent?.Invoke(this, args);
        }

        Image clientImage = new Image();
        double width, height;
        int colGoalPos, rowGoalPos, colStartPos, rowStartPos;
        int rowPlayerPos, colPlayerPos;
        int indexInMaze = 0, initialIndexInMaze = 0, endOfGame = 0;
        public enum Moves {Left, Right, Up, Down, Default};

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
                    Maze.Count();
                    if (Maze[counter] == '1')
                    {
                        AddWall(j, i);
                    }
                    if (i == rowStartPos && j == colStartPos)
                    {
                        AddImage(j, i, ImageSource, "Player");
                        initialIndexInMaze = counter;
                        indexInMaze = counter;
                    }
                    if (i == rowGoalPos && j == colGoalPos)
                    {
                        AddImage(j, i, ExitImageFile, "Exit");
                        endOfGame = counter;
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
            Canvas.SetLeft(rect, x * rect.Width);
            Canvas.SetTop(rect, y * rect.Height);
            rect.Name = "Wall";
            myCanvas.Children.Add(rect);

        }

        private void AddImage(int x, int y, ImageSource imageSource, string name)
        {
            Image image = new Image();
            image.Source = imageSource;
            image.Width = width;
            image.Height = height;
            Canvas.SetLeft(image, x * image.Width);
            Canvas.SetTop(image, y * image.Height);
            image.Name = name;
            myCanvas.Children.Add(image);

        }

        public void gridMazeBoard_KeyDown(object sender, KeyEventArgs e)
        {
            int pointer = rowPlayerPos * Rows + colPlayerPos + 1;
            foreach (UIElement child in myCanvas.Children)
            {
                if (((System.Windows.FrameworkElement)child).Name == "Player")
                {
                    clientImage = (Image)child;
                }
            }

            Vector offset = VisualTreeHelper.GetOffset(clientImage);
            var top = (rowPlayerPos)* clientImage.Height;
            var left = (colPlayerPos)*clientImage.Width;

            switch (e.Key)
            {
                case Key.Up:
                    {
                        if ((rowPlayerPos - 1) >= 0 && Maze[indexInMaze - Cols] == '0')
                        {
                            DoubleAnimation anim1 = new DoubleAnimation(top, top - clientImage.Height, TimeSpan.FromMilliseconds(200));
                            clientImage.BeginAnimation(Canvas.TopProperty, anim1);
                            rowPlayerPos -= 1;
                            indexInMaze -= Cols;
                        }
                        break;
                    }
                case Key.Down:
                    {
                        if ((rowPlayerPos + 1) < Rows && Maze[indexInMaze + Cols] == '0')
                        {
                            DoubleAnimation anim1 = new DoubleAnimation(top, top + clientImage.Height, TimeSpan.FromMilliseconds(200));
                            clientImage.BeginAnimation(Canvas.TopProperty, anim1);
                            rowPlayerPos += 1;
                            indexInMaze += Cols;
                        }
                        break;
                    }
                case Key.Right:
                    {
                        if ((colPlayerPos + 1) < Cols && Maze[indexInMaze + 1] == '0')
                        {
                            DoubleAnimation anim1 = new DoubleAnimation(left, left + clientImage.Width, TimeSpan.FromMilliseconds(200));
                            clientImage.BeginAnimation(Canvas.LeftProperty, anim1);
                            colPlayerPos += 1;
                            indexInMaze += 1;
                        }
                        break;
                    }
                case Key.Left:
                    {
                        if ((colPlayerPos - 1) >= 0 && Maze[indexInMaze - 1] == '0')
                        {
                            DoubleAnimation anim1 = new DoubleAnimation(left, left - clientImage.Width, TimeSpan.FromMilliseconds(200));
                            clientImage.BeginAnimation(Canvas.LeftProperty, anim1);
                            colPlayerPos -= 1;
                            indexInMaze -= 1;
                        }
                        break;
                    }
                default: break;
            }
            if (indexInMaze == endOfGame)
            {
                EndOfGame();
            }
        }


        public void EndOfGame()
        {
            OnYouWonEventEvent(new EventArgs());
        }

        public void RestartGame(object sender, RoutedEventArgs e)
        {
            foreach (UIElement child in myCanvas.Children)
            {
                if (((System.Windows.FrameworkElement)child).Name == "Player")
                {
                    myCanvas.Children.Remove(child);
                    break;
                    //clientImage = (Image)child;
                }
            }

            indexInMaze = initialIndexInMaze;
            colPlayerPos = colStartPos;
            rowPlayerPos = rowStartPos;
            AddImage(colStartPos, rowStartPos, ImageSource, "Player");
        }

        public void MoveAnimation(Moves move)
        {
            this.Dispatcher.Invoke(() =>
            {
                int pointer = rowPlayerPos * Rows + colPlayerPos + 1;
                foreach (UIElement child in myCanvas.Children)
                {
                    if (((System.Windows.FrameworkElement)child).Name == "Player")
                    {
                        clientImage = (Image)child;
                    }
                }

                Vector offset = VisualTreeHelper.GetOffset(clientImage);
                var top = (rowPlayerPos) * clientImage.Height;
                var left = (colPlayerPos) * clientImage.Width;

                switch (move)
                {
                    case Moves.Up:
                        {
                            if ((rowPlayerPos - 1) >= 0 && Maze[indexInMaze - Cols] == '0')
                            {
                                DoubleAnimation anim1 = new DoubleAnimation(top, top - clientImage.Height, TimeSpan.FromMilliseconds(200));
                                clientImage.BeginAnimation(Canvas.TopProperty, anim1);
                                rowPlayerPos -= 1;
                                indexInMaze -= Cols;
                            }
                            break;
                        }
                    case Moves.Down:
                        {
                            if ((rowPlayerPos + 1) < Rows && Maze[indexInMaze + Cols] == '0')
                            {
                                DoubleAnimation anim1 = new DoubleAnimation(top, top + clientImage.Height, TimeSpan.FromMilliseconds(200));
                                clientImage.BeginAnimation(Canvas.TopProperty, anim1);
                                rowPlayerPos += 1;
                                indexInMaze += Cols;
                            }
                            break;
                        }
                    case Moves.Right:
                        {
                            if ((colPlayerPos + 1) < Cols && Maze[indexInMaze + 1] == '0')
                            {
                                DoubleAnimation anim1 = new DoubleAnimation(left, left + clientImage.Width, TimeSpan.FromMilliseconds(200));
                                clientImage.BeginAnimation(Canvas.LeftProperty, anim1);
                                colPlayerPos += 1;
                                indexInMaze += 1;
                            }
                            break;
                        }
                    case Moves.Left:
                        {
                            if ((colPlayerPos - 1) >= 0 && Maze[indexInMaze - 1] == '0')
                            {
                                DoubleAnimation anim1 = new DoubleAnimation(left, left - clientImage.Width, TimeSpan.FromMilliseconds(200));
                                clientImage.BeginAnimation(Canvas.LeftProperty, anim1);
                                colPlayerPos -= 1;
                                indexInMaze -= 1;
                            }
                            break;
                        }
                    default: break;
                }

                if (indexInMaze == endOfGame)
                {
                    EndOfGame();
                }
            });
        }
    }
}
