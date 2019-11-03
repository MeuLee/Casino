using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace CasinoUI.Models
{
    public class SoundManager
    {
        public MediaPlayer CurrentSong { get; set; }

        private double _songVolume;
        public double SongVolume
        {
            get { return _songVolume; }
            set
            { 
                _songVolume = value;
                CurrentSong.Volume = value / 100.0;
            }
        }

        private double _sfxVolume;
        public double SFXVolume
        {
            get { return _sfxVolume; }
            set
            {
                _sfxVolume = value;
                // see SongVolume set
            }
        }

        public SoundManager()
        {
            CurrentSong = new MediaPlayer();
        }

        public void ChangeSong()
        {
            CurrentSong.Volume = 50;
            CurrentSong.Open(new Uri(@"C:\crash.wav", UriKind.Absolute));
            CurrentSong.Play();
        }

        [DllImport("winmm.dll")]
        public static extern int waveOutSetVolume(IntPtr hwo, uint dwVolume);
    }
}
