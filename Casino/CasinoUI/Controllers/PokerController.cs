using System;
using System.Windows;
using CasinoUI.Models.Poker.PokerBrains;
using CasinoUI.Models.Settings;
using CasinoUI.View;

namespace CasinoUI.Models.Poker
{
    class PokerController {
        PokerLogic pokerModel;
        GamePoker pokerView;
        int playerIdx;

        public PokerController() {
            pokerModel = new PokerLogic(ApplicationSettings.HumanPlayer);
            pokerView = new GamePoker();
            pokerView.Show();


        }

        private void addEvent()
        {
            pokerView.BtnSeCoucher.Click += BtnFold_Click;
            pokerView.BtnRelancer.Click += BtnRaise_Click;
            pokerView.BtnSuivre.Click += BtnCall_Click;
        }

        private void BtnCall_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void BtnRaise_Click(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void BtnFold_Click(object sender, RoutedEventArgs e)
        {

            throw new NotImplementedException();
        }
    }
}
