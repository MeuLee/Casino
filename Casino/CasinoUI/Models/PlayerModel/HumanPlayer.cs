using CasinoUI.Models.PlayerModel.PlayerSkin;
using CasinoUI.Models.Profiles;
using CasinoUI.Models.Settings;
using System.Windows.Media.Imaging;

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


        private Skin _currentSkin;
        public Skin CurrentSkin
        {
            get { return _currentSkin; }
            set
            {
                _currentSkin = value;
                CurrentImage = value.BaseDownImage;
            }
        }

        public BitmapImage CurrentImage { get; set; }

        public HumanPlayer(int x, int y) : base() {
            X = x;
            Y = y;
            CurrentSkin = SkinManager.Instance[Skins.GreenMan];
        }

        internal void MoveLeft()
        {
            if (X > 0)
            {
                X--;
                CurrentImage = CurrentSkin.NextLeftImage;
            }
        }

        internal void MoveUp()
        {
            if (Y > 0)
            {
                Y--;
                CurrentImage = CurrentSkin.NextUpImage;
            }
        }

        internal void MoveDown()
        {
            var mapLength = ApplicationSettings.Map.GetLength(1);
            if (mapLength - 1 > Y)
            {
                Y++;
                CurrentImage = CurrentSkin.NextDownImage;
            }
        }

        internal void MoveRight()
        {
            var mapLength = ApplicationSettings.Map.GetLength(0);
            if (mapLength - 1 > X)
            {
                X++;
                CurrentImage = CurrentSkin.NextRightImage;
            }
        }


    }
}
