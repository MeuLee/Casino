using System.Windows;

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
