namespace CasinoUI.Models.Poker
{
    public interface IPokerAction : IGameType
    {
        void PokerCheck();
        void PokerFold();
        void PokerAllIn();
        int PokerRaise(int moneyRaised);
        int PokerCall(int currentRaise);
    }
}
