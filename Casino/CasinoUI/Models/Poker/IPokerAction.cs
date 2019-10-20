namespace CasinoUI.Model.Poker
{
    interface IPokerAction {
        void PokerCheck();
        void PokerFold();
        void PokerRaise(int money);
        void PokerCall();

    }
}
