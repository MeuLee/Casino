using CasinoUI.Models.Poker;
using System;

namespace CasinoUI.Models.Profiles
{
    public class PokerProfile : Profile, IPokerAction
    {
        public int PokerAllIn()
        {
            throw new NotImplementedException();
        }
        public bool hasPlayed { get; set; }

        public PokerProfile() {
            hasPlayed = false;
        }

        public int PokerCall(int currentRaise)
        {
            throw new NotImplementedException();
        }

        public void PokerCheck()
        {
            throw new NotImplementedException();
        }

        public void PokerFold()
        {
            throw new NotImplementedException();
        }

        public int PokerRaise(int moneyRaised)
        {
            throw new NotImplementedException();
        }
    }
}
