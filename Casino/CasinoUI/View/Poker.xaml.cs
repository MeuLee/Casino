using CasinoUI.Utils;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
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
