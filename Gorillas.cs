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
        private int CanvasColumns { get; set; }
        private int gorillaSize;
        private int GorillaSize
        {
            get
            {
                return gorillaSize;
            }
            set
            {
                if (value > 100 || value < 20)
                    throw new ArgumentException("špatná hodnota!"); //Wrong value
                else
                    gorillaSize = value;
            }
        }

        public Gorillas(double windowWidth, double windowHeight, Canvas gameCanvas, string[] playersNames, int playerSize)
        {
            GorillaSize = playerSize;
            this.gameCanvas = gameCanvas;
            CanvasColumns = rnd.Next(9, 11 + 1);

            RenderSkyscrapers(); //w GorillaSpawn

            //PlayersNames(playersNames[0], 0, 0);
            //PlayersNames(playersNames[1], 0, (int)CanvasHeight -1);
        }

        private void RenderSkyscrapers() 
        {
            int[] gorillasLocation = GorillasColumnLocation(); //generate random column position for gorillas (players)

            int skycraperWidth = (int)(gameCanvas.ActualWidth / CanvasColumns);

            for (int i = 0; i < CanvasColumns; i++)
            {
                Rectangle rectangle = new Rectangle { Fill = Brushes.Gray, StrokeThickness = 1, Stroke = Brushes.Black };
                rectangle.Width = skycraperWidth;
                rectangle.Height = rnd.Next((int)(gameCanvas.ActualHeight * 0.1), (int)(gameCanvas.ActualHeight * 0.5));
                Canvas.SetLeft(rectangle, skycraperWidth * i);
                Canvas.SetTop(rectangle, gameCanvas.ActualHeight - rectangle.Height);
                gameCanvas.Children.Add(rectangle);

                if (gorillasLocation[0] == i || gorillasLocation[1] == i) //when position is met add player.
                    GorillaSpawn(skycraperWidth * i, (int)(gameCanvas.ActualHeight - rectangle.Height));
            }

        }

        private int[] GorillasColumnLocation()
        {
            int[] player = new int[2];
            player[0] = rnd.Next(0, 3);
            player[1] = rnd.Next((int)CanvasColumns - 3, (int)CanvasColumns);

            return player;
        }

        private void GorillaSpawn(int leftPosition, int topPosition)
        {
            Rectangle gorillaSprite = new Rectangle();
            gorillaSprite.Fill = new ImageBrush(new BitmapImage(new Uri("Resources/gorilla.png", UriKind.Relative))); //Microsoft's horrible image implementation
            gorillaSprite.Width = GorillaSize;
            gorillaSprite.Height = GorillaSize;
            Canvas.SetLeft(gorillaSprite, leftPosition);
            Canvas.SetTop(gorillaSprite, topPosition - gorillaSprite.Height);
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
