using CasinoUI.Models.PlayerModel.PlayerSkin;
using CasinoUI.Models.Profiles;
using System.Windows;

namespace CasinoUI.Models.PlayerModel
{
    public class HumanPlayer : Player
    {
        public int X { get; set; }
        public int Y { get; set; }
        public string Name { get { return CurrentSkin.Name; } }
        public int AlcoholLevel { get; set; } = 1;
        public int StressLevel { get; set; } = 1;

        public Profile CurrentProfile { get; set; }

        public Skin CurrentSkin { get; set; }

        public HumanPlayer(int x, int y)
        {
            X = x;
            Y = y;
            CurrentSkin = SkinManager.Instance[Skins.GreenMan];
        }
    }
}
