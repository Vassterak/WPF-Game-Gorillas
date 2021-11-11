using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Shapes;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace WPF_Game_Gorillas
{
    class Gorillas
    {
        private Canvas gameCanvas = new Canvas();
        private Random rnd = new Random();
        public int CanvasColumns { get; set; }

        public Gorillas(double windowWidth, double windowHeight, Canvas gameCanvas, string[] playersNames)
        {
            this.gameCanvas = gameCanvas;
            CanvasColumns = rnd.Next(9, 11 + 1);

            RenderSkyscrapers(); //w GorillaSpawn

            //PlayersNames(playersNames[0], 0, 0);
            //PlayersNames(playersNames[1], 0, (int)CanvasHeight -1);
        }

        //private void PrepareGameField(double windowWidth, double windowHeight) //Create a grid layout for the content USELESS NOW !
        //{
        //    CanvasWidth = windowHeight / 20;
        //    CanvasHeight = rnd.Next(9, 11 + 1);

        //    for (int i = 0; i < CanvasWidth; i++)
        //    {
        //        RowDefinition row = new RowDefinition();
        //        gameCanvas.RowDefinitions.Add(row);
        //    }

        //    for (int i = 0; i < CanvasHeight; i++)
        //    {
        //        ColumnDefinition column = new ColumnDefinition();
        //        gameCanvas.ColumnDefinitions.Add(column);
        //    }
        //}

        private void RenderSkyscrapers() 
        {
            //int[] gorillasLocation = GorillasLocation(); //generate random column position for gorillas (players)

            int skycraperWidth = (int)(gameCanvas.ActualWidth / CanvasColumns);

            //for (int x = 0; x < CanvasHeight; x++)
            //{
            //    int skyscraperHeight = rnd.Next(2, (int)CanvasWidth - 5);

            //    Rectangle rectangle = new Rectangle { Fill = Brushes.Gray, StrokeThickness = 1, Stroke = Brushes.Black}; //one is gray other is brown and so on...
            //    rectangle.Width
            //    rectangle.SetValue(Grid.ColumnProperty, x);
            //    Grid.SetRowSpan(rectangle,skyscraperHeight + 1);
            //    gameCanvas.Children.Add(rectangle);

            //    if (gorillasLocation[0] == x || gorillasLocation[1] == x) //when position is met add player.
            //        GorillaSpawn(x, (int)CanvasWidth - skyscraperHeight - 3);
            //}

            for (int i = 0; i < CanvasColumns; i++)
            {
                Rectangle rectangle = new Rectangle { Fill = Brushes.Gray, StrokeThickness = 1, Stroke = Brushes.Black };
                rectangle.Width = skycraperWidth;
                rectangle.Height = rnd.Next((int)(gameCanvas.ActualHeight * 0.1), (int)(gameCanvas.ActualHeight * 0.5));
                Canvas.SetLeft(rectangle, skycraperWidth * i);
                Canvas.SetTop(rectangle, gameCanvas.ActualHeight - rectangle.Height);
                gameCanvas.Children.Add(rectangle);
            }

        }

        //private int[] GorillasLocation()
        //{
        //    int[] player = new int[2];
        //    player[0] = rnd.Next(0, 3);
        //    player[1] = rnd.Next((int)CanvasHeight - 3, (int)CanvasHeight);

        //    return player;
        //}

        private void GorillaSpawn(int gridColumn, int rowHeight)
        {
            Rectangle gorillaSprite = new Rectangle();
            gorillaSprite.Fill = new ImageBrush(new BitmapImage(new Uri("Resources/gorilla.png", UriKind.Relative))); //Microsoft's horrible image implementation

            gorillaSprite.SetValue(Grid.RowProperty, rowHeight);
            gorillaSprite.SetValue(Grid.ColumnProperty, gridColumn);
            Grid.SetRowSpan(gorillaSprite, 3);
            gameCanvas.Children.Add(gorillaSprite);

        }

        private void PlayersNames(string name, int row, int column)
        {
            Label playersName = new Label { Foreground = Brushes.White };
            playersName.Content = name;
            playersName.SetValue(Grid.RowProperty, row);
            playersName.SetValue(Grid.ColumnProperty, column);
            Grid.SetColumnSpan(playersName, 2);

            gameCanvas.Children.Add(playersName);
        }
    }
}
