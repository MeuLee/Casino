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

namespace CasinoUI.View
{
    /// <summary>
    /// Logique d'interaction pour OptionMenu.xaml
    /// </summary>
    public partial class OptionMenu : Window
    {
        public OptionMenu()
        {
            InitializeComponent();
        }

        private void ClickedOption1(object sender, RoutedEventArgs e)
        {
            btnOpt1.Opacity = 1;
            btnOpt2.Opacity = 0.25;
            btnOpt3.Opacity = 0.25;
            btnOpt4.Opacity = 0.25;
        }

        private void ClickedOption2(object sender, RoutedEventArgs e)
        {
            btnOpt1.Opacity = 0.25;
            btnOpt2.Opacity = 1;
            btnOpt3.Opacity = 0.25;
            btnOpt4.Opacity = 0.25;
        }

        private void ClickedOption3(object sender, RoutedEventArgs e)
        {
            btnOpt1.Opacity = 0.25;
            btnOpt2.Opacity = 0.25;
            btnOpt3.Opacity = 1;
            btnOpt4.Opacity = 0.25;
        }

        private void ClickedOption4(object sender, RoutedEventArgs e)
        {
            btnOpt1.Opacity = 0.25;
            btnOpt2.Opacity = 0.25;
            btnOpt3.Opacity = 0.25;
            btnOpt4.Opacity = 1;
        }
    }
}
