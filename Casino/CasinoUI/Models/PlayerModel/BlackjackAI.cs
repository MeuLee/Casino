using CasinoUI.Models.Blackjack;

namespace CasinoUI.Models.PlayerModel
{
    public class BlackjackAI : PlayerAI, IBlackjackAction
    {
        public int PlayerHandValue { get; set; }

        public bool PlayerStand { get; set; }
        public bool PlayerBust { get; set; }
        public bool Has21 { get; set; }

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
