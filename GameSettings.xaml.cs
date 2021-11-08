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

        private static readonly Regex _lettNum = new Regex("^[A-Za-z0-9_-]*$");

        private void textBox_player1_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = _lettNum.IsMatch(e.Text);
        }

        private void textBox_player2_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = _lettNum.IsMatch(e.Text);
        }
    }
}
