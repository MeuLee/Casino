using CasinoUI.View;
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

namespace CasinoUI
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            Button btn = new Button();
            btn.Name = "Button";
            btn.Click += button_Click;
            InitializeComponent();
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            CasinoGame game = new CasinoGame();
            game.Show();
            this.Close();
        }


    }
}
