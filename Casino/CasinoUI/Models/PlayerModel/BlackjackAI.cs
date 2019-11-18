using CasinoUI.Models.Blackjack;
using CasinoUI.Models.PlayerModel;

namespace CasinoUI.Models.PlayerModel
{
    public class BlackjackAI : PlayerAI, IBlackjackAction
    {
        public int PlayerHandValue { get; set; }

        public int BlackjackDoubleDown(int currentBet)
        {
            throw new System.NotImplementedException();
        }

        public void BlackjackHit()
        {
            throw new System.NotImplementedException();
        }

        public void BlackjackInsurance()
        {
            throw new System.NotImplementedException();
        }

        public void BlackjackStand()
        {
            throw new System.NotImplementedException();
        }
    }
}
