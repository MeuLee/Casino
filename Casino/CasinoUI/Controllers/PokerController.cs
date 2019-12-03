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

            int numPlayers = pokerModel.ListPlayers.Count;

            // game loop
            //while (true) {

            //    // sub game loop
            //    while (pokerModel.arePlayersDone()) {
            //        // AI shit
            //        //PlayerPlaysTurn();
            //        pokerModel.incCurrentPlayerTurn();
            //    }

            //    pokerModel.ProceedNextGame();
            //}
        }

        private void addEvent() {
            pokerView.BtnSeCoucher.Click += BtnFold_Click;
            pokerView.BtnRelancer.Click += BtnRaise_Click;
            pokerView.BtnSuivre.Click += BtnCall_Click;
        }

        private void BtnCall_Click(object sender, RoutedEventArgs e) {
            pokerModel.PlayerPlaysTurn(PokerActionCode.CALL, -1);
            pokerModel.incCurrentPlayerTurn();


        }

        private void BtnRaise_Click(object sender, RoutedEventArgs e) {
            pokerModel.PlayerPlaysTurn(PokerActionCode.RAISE, 22);
            pokerModel.incCurrentPlayerTurn();
        }

        private void BtnFold_Click(object sender, RoutedEventArgs e) {
            pokerModel.PlayerPlaysTurn(PokerActionCode.FOLD, -1);
            pokerModel.incCurrentPlayerTurn();
        }
    }
}
