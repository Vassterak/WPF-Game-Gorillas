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
using System.Windows.Shapes;
using System.Text.RegularExpressions;

namespace WPF_Game_Gorillas
{
    /// <summary>
    /// Interaction logic for GameSettings.xaml
    /// </summary>
    public partial class GameSettings : Window
    {
        public GameSettings()
        {
            InitializeComponent();
        }

        private static readonly Regex _lettNum = new Regex("^[A-Za-z0-9ěščřžýáíé]*$");

        private void textBox_player1_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!_lettNum.IsMatch(e.Text))
                e.Handled = true;

            CheckNameLenght(e, textBox_player1);
        }

        private void textBox_player2_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!_lettNum.IsMatch(e.Text))
                e.Handled = true;

            CheckNameLenght(e, textBox_player2);
        }

        private void CheckNameLenght(TextCompositionEventArgs e, TextBox textBox)
        {
            if (textBox.Text.Length > 8)
            {
                MessageBox.Show("Jméno je moc dlouhé!"); 
                e.Handled = true;
            }
        }

        private void button_newGame_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainGame = new MainWindow();
            mainGame.ShowDialog(); //Prevents to creating new window until the old is closed
        }
    }
}
