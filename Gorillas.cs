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
            RenderSkyscrapers();
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

        private void RenderSkyscrapers()
        {
            for (int x = 0; x < GridColumns; x++)
            {
                int skyscraperHeight = rnd.Next(2, (int)GridRows - 5);

                Rectangle rectangle = new Rectangle { Fill = Brushes.Gray, StrokeThickness = 1, Stroke = Brushes.Black}; //one is gray other is brown and so on...
                rectangle.SetValue(Grid.RowProperty, (int)GridRows - skyscraperHeight);
                rectangle.SetValue(Grid.ColumnProperty, x);
                Grid.SetRowSpan(rectangle,skyscraperHeight + 1);
                gameGrid.Children.Add(rectangle);
            }
        }
    }
}
