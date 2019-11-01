namespace CasinoUI.Model.Blackjack
{
    interface IBlackjackAction {
        void BlackjackHit();
        void BlackjackStand();
        void BlackjackInsurance();
        void BlackjackDoubleDown();

    }
}
