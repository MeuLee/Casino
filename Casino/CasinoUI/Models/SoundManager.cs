using System;
using System.Collections.Generic;
using System.IO;
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
        public MediaPlayer CurrentSong { get; private set; }

        public double SongVolume
        {
            get { return _songVolume; }
            set
            { 
                _songVolume = value;
                CurrentSong.Volume = value / 100.0;
            }
        }

        public double SFXVolume
        {
            get { return _sfxVolume; }
            set
            {
                _sfxVolume = value;
                // see SongVolume set
            }
        }

        private static List<Uri> _songUris;
        private double _songVolume;
        private double _sfxVolume;
        private int _songIndex = 0;

        static SoundManager()
        {
            LoadSongs();
        }

        private static void LoadSongs()
        {
            _songUris = new List<Uri>();
            string[] paths = Directory.GetFiles("Resources", "*.wav", SearchOption.TopDirectoryOnly);
            foreach (var path in paths)
            {
                _songUris.Add(new Uri(path, UriKind.Relative));
            }
        }

        public SoundManager()
        {
            CurrentSong = new MediaPlayer();
        }

        private void CurrentSong_MediaEnded(object sender, EventArgs e)
        {
            PlaySong();
        }

        public void Start()
        {
            CurrentSong.MediaEnded += CurrentSong_MediaEnded;
            PlaySong();
        }

        public void Stop()
        {
            CurrentSong.MediaEnded -= CurrentSong_MediaEnded;
            CurrentSong.Stop();
        }

        private void PlaySong()
        {
            CurrentSong.Open(_songUris[_songIndex]);
            CurrentSong.Play();
            SetIndex();
        }

        private void SetIndex()
        {
            _songIndex++;
            if (_songIndex > _songUris.Count - 1)
            {
                _songIndex = 0;
            }
        }
    }
}
