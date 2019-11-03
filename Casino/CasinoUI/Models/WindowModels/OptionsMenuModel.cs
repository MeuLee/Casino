using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CasinoUI.Models.WindowModels
{
    public class OptionsMenuModel
    {
        public void ModifySongVolume(double volume)
        {
            ApplicationSettings.SoundPlayer.SongVolume = volume;
        }

        public void ModifySFXVolume(double volume)
        {
            ApplicationSettings.SoundPlayer.SFXVolume = volume;
        }
    }
}
