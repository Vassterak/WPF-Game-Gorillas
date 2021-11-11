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
using System.Text.RegularExpressions;

namespace WPF_Game_Gorillas
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private static readonly Regex _numOnly = new Regex("^[0-9]*$");
        Gorillas gorillasGame;

        public MainWindow()
        {
            InitializeComponent();
            this.PreviewKeyDown += new KeyEventHandler(ESCapeKey);
        }

        private void Window_SourceInitialized(object sender, EventArgs e)
        {
            gorillasGame = new Gorillas(this.Width, this.Height, gameCanvas, int.Parse(((GameSettings)Application.Current.MainWindow).textBox_playerSize.Text));

            player1Name.Content = ((GameSettings)Application.Current.MainWindow).textBox_player1.Text;
            player2Name.Content = ((GameSettings)Application.Current.MainWindow).textBox_player2.Text;
        }

        private void ESCapeKey(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
                this.Close();
        }

        private void player1Angle_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!_numOnly.IsMatch(e.Text))
                e.Handled = true;

            gorillasGame.player1[0] = CheckNumber(e, player1Angle.Text);
        }

        private void player1Power_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!_numOnly.IsMatch(e.Text))
                e.Handled = true;

            gorillasGame.player1[1] = CheckNumber(e, player1Angle.Text);
        }

        private void player2Angle_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!_numOnly.IsMatch(e.Text))
                e.Handled = true;

            gorillasGame.player2[0] = CheckNumber(e, player1Angle.Text);
        }

        private void player2Power_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!_numOnly.IsMatch(e.Text))
                e.Handled = true;

            gorillasGame.player2[1] = CheckNumber(e, player1Angle.Text);
        }

        private int CheckNumber(TextCompositionEventArgs e, string text)
        {
            int outNumber = 0;
            try
            {
                outNumber = int.Parse(text);
                if (outNumber > 270 || outNumber < 0)
                {
                    MessageBox.Show("Zadal jste neplatné hodnoty");
                    return 0;
                }
                else
                    return outNumber;
            }
            catch (Exception)
            {
                MessageBox.Show("Zadal jste neplatné hodnoty");
                return 0;
            }
        }

        private void nextRoundButton_Click(object sender, RoutedEventArgs e)
        {
            gorillasGame.ThrowCalculation();
        }
    }
}
