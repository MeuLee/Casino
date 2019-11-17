using CasinoUI.Models.Cards;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
