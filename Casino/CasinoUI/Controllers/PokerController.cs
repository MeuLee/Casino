using System;
using System.Windows;
using CasinoUI.Models.Poker.PokerBrains;
using CasinoUI.Models.Settings;
using CasinoUI.View;

namespace CasinoUI.Models.Poker
{
    class PokerController {
        PokerLogic pokerModel;
        GamePoker jeuPoker;

        public PokerController() {
            pokerModel = new PokerLogic(ApplicationSettings.HumanPlayer);
            jeuPoker = new GamePoker();
            jeuPoker.Show();


        }

        private void addEvent()
        {
            jeuPoker.BtnSeCoucher.Click += BtnSeCoucher_Click;
            jeuPoker.BtnRelancer.Click += BtnRelancer_Click;
            jeuPoker.BtnSuivre.Click += BtnSuivre_Click;
        }

        private void BtnSuivre_Click(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void BtnRelancer_Click(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void BtnSeCoucher_Click(object sender, RoutedEventArgs e)
        {

            throw new NotImplementedException();
        }
    }
}
