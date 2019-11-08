using CasinoUI.Models.Cards;
using System.Collections.Generic;

namespace CasinoUI.Models.Profiles
{
    public abstract class Profile
    {
        public List<Card> Hand { get; private set; }
        protected Profile()
        {
            Hand = new List<Card>();
        }
    }
}
