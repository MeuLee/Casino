using System.Windows;
using CasinoUI.Models.Poker.PokerBrains;
using CasinoUI.Models.Profiles;
using CasinoUI.Models.Settings;
using CasinoUI.View;

namespace CasinoUI.Models.Poker
{
    class PokerController {
        PokerLogic pokerModel;
        GamePoker pokerView;
        int playerIdx; 

        public PokerController() {
            ApplicationSettings.HumanPlayer.CurrentProfile = new PokerProfile();
            pokerModel = new PokerLogic(ApplicationSettings.HumanPlayer);
            pokerView = new GamePoker();
            pokerView.Show();

            int numPlayers = pokerModel.ListPlayers.Count;

            addEvent();

            // if first turn is not humanPlayer
            if (pokerModel.currentPlayerTurnIdx != 0) {
                AIsBeforeHumanPlayTurn();
            }

            // game loop
            //while (true) {

            //    // sub game loop
            //    while (pokerModel.arePlayersDone()) {
            //         // AI shit
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

            if (!pokerModel.arePlayersDone())
            {
                AIsAfterHumanPlayTurn();
            }
            else {
                // split pot
            }
        }

        private void BtnRaise_Click(object sender, RoutedEventArgs e) {
            pokerModel.PlayerPlaysTurn(PokerActionCode.RAISE, 22);
            pokerModel.incCurrentPlayerTurn();

            if (!pokerModel.arePlayersDone()) {
                AIsAfterHumanPlayTurn();
            }
        }

        private void BtnFold_Click(object sender, RoutedEventArgs e) {
            pokerModel.PlayerPlaysTurn(PokerActionCode.FOLD, -1);
            pokerModel.incCurrentPlayerTurn();

            if (!pokerModel.arePlayersDone()) {
                AIsAfterHumanPlayTurn();
            }
        }

        private void AIsBeforeHumanPlayTurn() {
            while (pokerModel.currentPlayerTurnIdx != 0)
            {
                pokerModel.PlayerPlaysTurn(PokerActionCode.CALL, -1);   // temp
                pokerModel.incCurrentPlayerTurn();
            }
        }

        private void AIsAfterHumanPlayTurn() {
            while (pokerModel.currentPlayerTurnIdx != pokerModel.PlayerRoles[2]) {
                pokerModel.PlayerPlaysTurn(PokerActionCode.CALL, -1);   // temp
                pokerModel.incCurrentPlayerTurn();
            }
        }
    }
}
