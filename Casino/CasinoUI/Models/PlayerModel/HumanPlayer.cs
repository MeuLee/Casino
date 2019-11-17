using CasinoUI.Models.Profiles;

namespace CasinoUI.Models.PlayerModel
{
    public class HumanPlayer : Player
    {
        public int X { get; set; }
        public int Y { get; set; }
        public string Name { get; set; }

        public Profile CurrentProfile { get; set; }

        public HumanPlayer(int x, int y, string name)
        {
            X = x;
            Y = y;
            Name = name;
        }
    }
}
