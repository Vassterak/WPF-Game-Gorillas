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
        private bool exceptionThrown = false;
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

            nextRoundButton.Content = "Hraje: " + player1Name.Content;
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
        }

        private void player1Power_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!_numOnly.IsMatch(e.Text))
                e.Handled = true;
        }

        private void player2Angle_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!_numOnly.IsMatch(e.Text))
                e.Handled = true;
        }

        private void player2Power_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!_numOnly.IsMatch(e.Text))
                e.Handled = true;
        }

        private int CheckNumber(string text)
        {
            int outNumber = 0;
            try
            {
                outNumber = int.Parse(text);
                if (outNumber > 270 || outNumber < 0)
                {
                    MessageBox.Show("Zadal jste neplatné hodnoty. Úhel nejsmí být větší jak 270° a síla také.");
                    exceptionThrown = true;
                    return 0;
                }
                else
                    return outNumber;
            }

            catch (Exception)
            {
                MessageBox.Show("Zadal jste neplatné hodnoty. Úhel nejsmí být větší jak 270° a síla také.");
                exceptionThrown = true;
                return 0;
            }
        }

        private void nextRoundButton_Click(object sender, RoutedEventArgs e)
        {
            exceptionThrown = false;
            if (gorillasGame.player1Starts)
            {
                gorillasGame.player1[0] = CheckNumber(player1Angle.Text);
                if (!exceptionThrown)
                {
                    gorillasGame.player1[1] = CheckNumber(player1Power.Text);

                    if (!exceptionThrown)
                        nextRoundButton.Content = player2Name.Content;
                }
            }
            else
            {
                gorillasGame.player2[0] = CheckNumber(player2Angle.Text);

                if (!exceptionThrown)
                {
                    gorillasGame.player2[1] = CheckNumber(player2Power.Text);

                    if (!exceptionThrown)
                        nextRoundButton.Content = player1Name.Content;
                }
            }

            if (!exceptionThrown)
                gorillasGame.ThrowCalculation();
        }
    }
}
