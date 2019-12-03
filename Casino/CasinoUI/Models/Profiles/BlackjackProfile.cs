using CasinoUI.Models.Blackjack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CasinoUI.Models.Profiles
{
    public class BlackjackProfile : Profile, IBlackjackAction
    {
        public int PlayerHandValue { get; set; }
        public bool PlayerStand { get; set; }
        public bool PlayerBust { get; set; }

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
