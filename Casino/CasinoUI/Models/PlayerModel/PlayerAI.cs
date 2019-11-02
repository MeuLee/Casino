using CasinoUI.Models.Cards;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CasinoUI.Models.PlayerModel
{
    public class PlayerAI : Player
    {
        public List<Card> Hand { get; set; } // need to new this list every time he plays a new game
    }
}
