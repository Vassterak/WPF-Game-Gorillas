using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Shapes;
using System.Windows.Media;

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
            RenderCheckeredBackground();
        }

        private void PrepareGameField(double windowWidth, double windowHeight) //Create a grid layout for the content
        {
            GridRows = windowHeight / 20;
            GridColumns = rnd.Next(8, 11 + 1);

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

        private void RenderCheckeredBackground()
        {
            bool oddRectangle = true;

            for (int y = 0; y < GridRows; y++)
            {
                for (int x = 0; x < GridColumns; x++)
                {

                    Rectangle rectangle = new Rectangle { Fill = oddRectangle ? Brushes.LightGray : Brushes.SaddleBrown }; //one is gray other is brown and so on...
                    rectangle.SetValue(Grid.RowProperty, y);
                    rectangle.SetValue(Grid.ColumnProperty, x);
                    gameGrid.Children.Add(rectangle);

                    oddRectangle = OddChecker(oddRectangle, x); //Creating the checkboard pattern
                }
            }
        }

        private bool OddChecker(bool oddRectangle, int x) //Return true only when last plate was false
        {
            oddRectangle = !oddRectangle;
            if (x == GridColumns - 1 && GridColumns % 2 == 0) //only true when x(rows) are even
                oddRectangle = !oddRectangle;

            return oddRectangle;
        }
    }
}
