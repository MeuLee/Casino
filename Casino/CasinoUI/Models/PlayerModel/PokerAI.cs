using CasinoUI.Models.PlayerModel;

namespace CasinoUI.Models.Poker
{
    public class PokerAI : PlayerAI, IPokerAction
    {

        public PokerAI()
        {
            Money = 1000;
        }

        public int PokerCall(int currentRaise)
        {
            throw new System.NotImplementedException();
        }

        public void PokerCheck()
        {
            throw new System.NotImplementedException();
        }

        public void PokerFold()
        {
            throw new System.NotImplementedException();
        }

        public int PokerRaise(int moneyRaised)
        {
            throw new System.NotImplementedException();
        }
    }
}
