using CasinoUI.Models.PlayerModel;
using CasinoUI.Views.Map;
using CasinoUI.Views.Map.Tiles;
using System.Windows;

namespace CasinoUI.Models.Settings
{
    public static class ApplicationSettings
    {
        public static HumanPlayer HumanPlayer { get; private set; }
        public static MapTile[,] Map { get; private set; }
        public static SoundManager SoundPlayer { get; private set; }

        public static void Load()
        {
            Map = MapGenerator.LoadMapFromFile(Properties.Resources.map);
            HumanPlayer = new HumanPlayer(10, 4);
            SoundPlayer = new SoundManager
            {
                SongVolume = 50,
                SFXVolume = 50
            };
        }

    }
}
