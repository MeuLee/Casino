using CasinoUI.Utils;
using System.Windows;

namespace CasinoUI.View
{
    /// <summary>
    /// Logique d'interaction pour Poker.xaml
    /// </summary>
    public partial class Poker : Window
    {
        public Poker()
        {
            InitializeComponent();
            TableBlueBackground.ImageSource =Properties.Resources.TableNeuve.ToBitmapImage();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
        }
    }
}
