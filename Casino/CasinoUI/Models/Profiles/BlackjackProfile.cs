using CasinoUI.Models.Blackjack;
using System;

namespace CasinoUI.Models.Profiles
{
    public class BlackjackProfile : Profile, IBlackjackAction
    {
        public int PlayerHandValue { get; set; }
        public bool PlayerStand { get; set; }
        public bool PlayerBust { get; set; }
        public bool Has21 { get; set; }

        public int BlackjackDoubleDown(int currentBet)
        {
            throw new NotImplementedException();
        }

        public void BlackjackHit()
        {
            throw new NotImplementedException();
        }

        public void BlackjackInsurance()
        {
            throw new NotImplementedException();
        }

        public void BlackjackStand()
        {
            throw new NotImplementedException();
        }
    }
}
