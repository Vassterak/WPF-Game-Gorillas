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

namespace WPF_Game_Gorillas
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.PreviewKeyDown += new KeyEventHandler(ESCapeKey);
        }

        private void Window_SourceInitialized(object sender, EventArgs e)
        {
            string[] names = new string[2];
            names[0] = ((GameSettings)Application.Current.MainWindow).textBox_player1.Text;
            names[1] = ((GameSettings)Application.Current.MainWindow).textBox_player2.Text;

            Gorillas gorillasGame = new Gorillas(this.Width, this.Height, gameGrid, names);
        }

        private void ESCapeKey(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                this.Close();
            }
        }
    }
}
