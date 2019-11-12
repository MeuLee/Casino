using CasinoUI.Model;
using CasinoUI.Model.Blackjack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CasinoUI.Models
{
    public class BlackjackPlayer : Player, IBlackjackAction
    {
        public bool Stand { get; set; } 

        public int HandValue { get; set; }

        public BlackjackPlayer()
        {
            Stand = false;
            HandValue = 0;
        }

        public void BlackjackHit()
        {
            throw new System.NotImplementedException();
        }

        public void BlackjackStand()
        {
            Stand = true;
        }

        public int BlackjackDoubleDown(int currentBet)
        {            
            Stand = true;

            return currentBet * 2;   
        }               

        public void BlackjackInsurance()
        {
            throw new System.NotImplementedException();
        }

        
    }
}
