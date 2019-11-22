using CasinoUI.Models.Cards;
using System.Collections.Generic;

namespace CasinoUI.Models.PlayerModel
{
    public class PlayerAI : Player
    {
        public List<Card> Hand { get; set; } // need to new this list every time he plays a new game
    }
}
