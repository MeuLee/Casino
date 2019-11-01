namespace CasinoUI.Models.Poker
{
    public interface IPokerAction
    {
        void PokerCheck();
        void PokerFold();
        int PokerRaise(int moneyRaised);
        int PokerCall(int currentRaise);
    }
}
