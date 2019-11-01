namespace CasinoUI.Models.Blackjack
{
    interface IBlackjackAction {
        void BlackjackHit();
        void BlackjackStand();
        void BlackjackInsurance();
        void BlackjackDoubleDown();

    }
}
