﻿using CasinoUI.Models.PlayerModel;
using CasinoUI.Views.Map;
using CasinoUI.Views.Map.Tiles;

namespace CasinoUI.Models
{
    public static class ApplicationSettings
    {
        public static HumanPlayer HumanPlayer { get; private set; }
        public static MapTile[,] Map { get; private set; }
        public static SoundManager SoundPlayer { get; private set; }

        static ApplicationSettings()
        {
            Map = MapGenerator.LoadMapFromFile(Properties.Resources.map);
            HumanPlayer = new HumanPlayer(10, 4, "");
            SoundPlayer = new SoundManager
            {
                SongVolume = 50,
                SFXVolume = 50
            };
        }

        /// <summary>
        /// This method might seem useless but it calls the static constructor. 
        /// Big brain time
        /// </summary>
        public static void Load()
        {

        }

    }
}
