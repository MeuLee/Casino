using CasinoUI.Models.Profiles;

namespace CasinoUI.Models.PlayerModel
{
    public class HumanPlayer : Player
    {
        public int X { get; set; }
        public int Y { get; set; }
        public string Name { get; set; }
        public int AlcoholLevel { get; set; } = 0;
        public int StressLevel { get; set; } = 0;

        public Profile CurrentProfile { get; set; }

        public HumanPlayer(int x, int y, string name)
        {
            X = x;
            Y = y;
            Name = name;
        }
    }
}
