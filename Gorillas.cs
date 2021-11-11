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
using System.Windows.Threading;

namespace WPF_Game_Gorillas
{
    class Gorillas
    {   
        private const int gravityCoeficient = 10; //9,81 rounded to 10
        public int[] player1 = new int[2]; //angle and speed
        public int[] player2 = new int[2]; //angle and speed
        private bool player1Starts = true;

        private Canvas gameCanvas = new Canvas();
        private Random rnd = new Random();

        //Players sprites
        private Rectangle gorillaSprite1 = new Rectangle();
        private Rectangle gorillaSprite2 = new Rectangle();

        //Bullet variables
        DispatcherTimer physicTimerUpdate = new DispatcherTimer();
        private Ellipse gameBullet = new Ellipse { Fill = Brushes.White, Width = 15, Height = 15 };
        private int osaX = 0, differenceOfY = 0, angle = 0, power = 0;

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

        public Gorillas(double windowWidth, double windowHeight, Canvas gameCanvas, int playerSize)
        {
            GorillaSize = playerSize;
            this.gameCanvas = gameCanvas;
            CanvasColumns = rnd.Next(9, 11 + 1);

            InitialRender(); //w GorillaSpawn and bullet
        }

        private void InitialRender() 
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
                    GorillaSpawn(skycraperWidth * i, (int)(gameCanvas.ActualHeight - rectangle.Height), i);
            }

        }

        private int[] GorillasColumnLocation()
        {
            int[] player = new int[2];
            player[0] = rnd.Next(0, 3);
            player[1] = rnd.Next((int)CanvasColumns - 3, (int)CanvasColumns);

            return player;
        }

        private void GorillaSpawn(int leftPosition, int topPosition, int index)
        {
            if (index < 3) //of course I'm writing the most efficient code... As I always did...
            {
                gorillaSprite1.Fill = new ImageBrush(new BitmapImage(new Uri("Resources/gorilla.png", UriKind.Relative))); //Microsoft's horrible image implementation
                gorillaSprite1.Width = GorillaSize;
                gorillaSprite1.Height = GorillaSize;
                Canvas.SetLeft(gorillaSprite1, leftPosition);
                Canvas.SetTop(gorillaSprite1, topPosition - gorillaSprite1.Height);
                gameCanvas.Children.Add(gorillaSprite1);
            }

            else
            {
                gorillaSprite2.Fill = new ImageBrush(new BitmapImage(new Uri("Resources/gorilla.png", UriKind.Relative))); //Microsoft's horrible image implementation
                gorillaSprite2.Width = GorillaSize;
                gorillaSprite2.Height = GorillaSize;
                Canvas.SetLeft(gorillaSprite2, leftPosition);
                Canvas.SetTop(gorillaSprite2, topPosition - gorillaSprite2.Height);
                gameCanvas.Children.Add(gorillaSprite2);
            }

        }

        //private void PlayersNames(string name, int row, int column)
        //{
        //    Label playersName = new Label { Foreground = Brushes.White };
        //    playersName.Content = name;
        //    playersName.SetValue(Grid.RowProperty, row);
        //    playersName.SetValue(Grid.ColumnProperty, column);
        //    Grid.SetColumnSpan(playersName, 2);

        //    gameCanvas.Children.Add(playersName);
        //}

        public void ThrowCalculation()
        {
            gameCanvas.Children.Remove(gameBullet);

            if (player1Starts) //player 1 is shooting
            {
                Canvas.SetLeft(gameBullet, Canvas.GetLeft(gorillaSprite1) + gorillaSprite1.Height / 2);
                Canvas.SetTop(gameBullet, Canvas.GetTop(gorillaSprite1) - gorillaSprite1.Width / 5);
                gameCanvas.Children.Add(gameBullet);
                player1Starts = false;
                angle = player1[0];
                power = player1[1];
            }
            else //player 2 is shooting
            {
                Canvas.SetLeft(gameBullet, Canvas.GetLeft(gorillaSprite2) + gorillaSprite2.Height / 2);
                Canvas.SetTop(gameBullet, Canvas.GetTop(gorillaSprite2) - gorillaSprite2.Width / 5);
                gameCanvas.Children.Add(gameBullet);
                player1Starts = true;
                angle = player2[0];
                power = player2[1];
            }
        }

        private void ThrowUpdate()
        {
            double radians = (angle % 360) / 180 * Math.PI;

            double i = (double)osaX / (power * Math.Cos(radians));

            int axisY = (int)(power * i * Math.Sin(radians));
            axisY = (int)(axisY - 0.5 * gravityCoeficient * i * i);

            differenceOfY = differenceOfY - axisY;

            //gameBullet.Location = new Point(gameBullet.Location.X + 15, gameBullet.Location.Y + rozdilOsyY);
            Canvas.SetLeft(gameBullet, Canvas.GetLeft(gameBullet) + 10);
            Canvas.SetTop(gameBullet, Canvas.GetTop(gameBullet) + differenceOfY);

            osaX += 15;
            differenceOfY = axisY;
            //detekceKolize();
        }
    }
}
