using CasinoUI.View;
using System.Windows;

namespace CasinoUI
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            CasinoGame game = new CasinoGame();
            game.Show();
            this.Close();
            //Poker test = new Poker();
            //test.Show();
        }

        private void button_Options_Click(object sender, RoutedEventArgs e)
        {
            OptionMenu options = new OptionMenu();
            options.Show();
        }
    }
}
