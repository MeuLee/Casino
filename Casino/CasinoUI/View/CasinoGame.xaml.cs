using CasinoUI.Utils;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace CasinoUI.View
{
    /// <summary>
    /// Logique d'interaction pour CasinoGame.xaml
    /// </summary>
    public partial class CasinoGame : Window
    {
        public CasinoGame()
        {
            InitializeComponent();
            InitializeImage();
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
        }

        private void InitializeImage()
        {
            ImageJoueur.Source = ToBitmapImage(Properties.Resources.panda);
            TableBackground.ImageSource = ToBitmapImage(Properties.Resources.table);
            Hero.Source = ToBitmapImage(Properties.Resources.droite1);
            EntrerPoker.Source = ToBitmapImage(Properties.Resources.PokerEntrer);
            EntrerPoker.Height = 50;
            EntrerPoker.Width = 50;
        }

        private void Canvas_KeyDown(object sender, KeyEventArgs e)
        {
            long heroPosLeft = Convert.ToInt64(Hero.GetValue(Canvas.LeftProperty));
            long heroPosTop = Convert.ToInt64(Hero.GetValue(Canvas.TopProperty));

            switch (e.Key)
            {
                case Key.S:
                    if (heroPosTop < 380)
                    {
                        Hero.Source = ToBitmapImage(Properties.Resources.bas1);
                        Canvas.SetTop(Hero, Canvas.GetTop(Hero) + 10);
                    }
                    break;

                case Key.W:
                    if (heroPosLeft > 125 || heroPosTop < 300)
                    {
                        if (heroPosTop > 0)
                        {
                            Hero.Source = ToBitmapImage(Properties.Resources.haut1);
                            Canvas.SetTop(Hero, Canvas.GetTop(Hero) - 10);
                        }
                    }

                    break;

                case Key.A:
                    if (heroPosLeft > 135 || heroPosTop > 310)
                    {
                        if (heroPosLeft > 0)
                        {
                            Hero.Source = ToBitmapImage(Properties.Resources.gauche1);
                            Canvas.SetLeft(Hero, Canvas.GetLeft(Hero) - 10);
                        }
                    }
                    break;

                case Key.D:
                    if (heroPosLeft < 750)
                    {
                        Hero.Source = ToBitmapImage(Properties.Resources.droite1);
                        Canvas.SetLeft(Hero, Canvas.GetLeft(Hero) + 10);
                    }
                    break;
                case Key.E:
                    if (heroPosLeft >= 400 && heroPosLeft <= 420)
                    {
                        if (heroPosTop >= 70 && heroPosTop <= 100)
                        {
                            Poker pokerGame = new Poker();
                            pokerGame.Show();
                            this.Close();
                        }
                    }
                    break;
            }
        }


    }
}
