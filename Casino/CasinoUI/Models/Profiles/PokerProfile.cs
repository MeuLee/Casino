using CasinoUI.Models.Poker;

namespace CasinoUI.Models.Profiles
{
    public class PokerProfile : Profile, IPokerAction
    {
        public bool hasPlayed { get; set; }

        public PokerProfile() {
            hasPlayed = false;
        }

        public int PokerAllIn() {
            return -1;
        }

        public int PokerCall(int currentRaise) {
            return -1;
        }

        public void PokerCheck() {
        }

        public void PokerFold() {
            
        }

        public int PokerRaise(int moneyRaised) {
            return -1;
        }
    }
}
