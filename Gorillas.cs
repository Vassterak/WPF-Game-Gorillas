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
        private Grid gameGrid = new Grid();
        private Random rnd = new Random();
        private double GridRows { get; set; }
        private double GridColumns { get; set; }

        public Gorillas(double windowWidth, double windowHeight, Grid gameGrid)
        {
            this.gameGrid = gameGrid;
            PrepareGameField(windowWidth, windowHeight);
            RenderSkyscrapers(); //w GorillaSpawn
        }

        private void PrepareGameField(double windowWidth, double windowHeight) //Create a grid layout for the content
        {
            GridRows = windowHeight / 20;
            GridColumns = rnd.Next(9, 11 + 1);

            for (int i = 0; i < GridRows; i++)
            {
                RowDefinition row = new RowDefinition();
                gameGrid.RowDefinitions.Add(row);
            }

            for (int i = 0; i < GridColumns; i++)
            {
                ColumnDefinition column = new ColumnDefinition();
                gameGrid.ColumnDefinitions.Add(column);
            }
        }

        private void RenderSkyscrapers() 
        {
            int[] gorillasLocation = GorillasLocation(); //generate random column position for gorillas (players)

            for (int x = 0; x < GridColumns; x++)
            {
                int skyscraperHeight = rnd.Next(2, (int)GridRows - 5);

                Rectangle rectangle = new Rectangle { Fill = Brushes.Gray, StrokeThickness = 1, Stroke = Brushes.Black}; //one is gray other is brown and so on...
                rectangle.SetValue(Grid.RowProperty, (int)GridRows - skyscraperHeight);
                rectangle.SetValue(Grid.ColumnProperty, x);
                Grid.SetRowSpan(rectangle,skyscraperHeight + 1);
                gameGrid.Children.Add(rectangle);

                if (gorillasLocation[0] == x || gorillasLocation[1] == x) //when position is met add player.
                    GorillaSpawn(x, (int)GridRows - skyscraperHeight - 3);
            }
        }

        private int[] GorillasLocation()
        {
            int[] player = new int[2];
            player[0] = rnd.Next(0, 3);
            player[1] = rnd.Next((int)GridColumns - 3, (int)GridColumns);

            return player;
        }

        private void GorillaSpawn(int gridColumn, int rowHeight)
        {
            Rectangle gorillaSprite = new Rectangle();
            gorillaSprite.Fill = new ImageBrush(new BitmapImage(new Uri("Resources/gorilla.png", UriKind.Relative))); //Microsoft's horrible image implementation

            gorillaSprite.SetValue(Grid.RowProperty, rowHeight);
            gorillaSprite.SetValue(Grid.ColumnProperty, gridColumn);
            Grid.SetRowSpan(gorillaSprite, 3);
            gameGrid.Children.Add(gorillaSprite);

        }
    }
}
