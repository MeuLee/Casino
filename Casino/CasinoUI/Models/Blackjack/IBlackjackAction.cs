namespace CasinoUI.Model.Blackjack
{
    interface IBlackjackAction {
        void BlackjackHit();
        void BlackjackStand();
        void BlackjackSplit();
        void BlackjackInsurance();
        void BlackjackDoubleDown();

    }
}
