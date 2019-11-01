namespace CasinoUI.Model.Blackjack
{
    interface IBlackjackAction {
        void BlackjackHit();
        void BlackjackStand();
        void BlackjackInsurance();
        int BlackjackDoubleDown(int currentBet);

    }
}
