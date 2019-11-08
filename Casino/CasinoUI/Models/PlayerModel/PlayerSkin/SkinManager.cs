using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CasinoUI.Models.PlayerModel.PlayerSkin
{
    /// <summary>
    /// Singleton class
    /// </summary>
    public class SkinManager
    {
        private Dictionary<Skins, Skin> _skins;
        private Dictionary<Skins, Skin> Skins
        {
            get
            {
                if (_skins == null)
                {
                    _skins = SkinsGenerator.Skins;
                }
                return _skins;
            }
        }
        private static SkinManager _instance;

        public Skin this[Skins key]
        {
            get { return Skins[key]; }
        }

        public List<Skin> SkinsList
        {
            get { return Skins.Select(kvp => kvp.Value).ToList(); }
        }

        public static SkinManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new SkinManager();
                }
                return _instance;
            }
        }

        private SkinManager()
        {
            _skins = SkinsGenerator.Skins;
        }
    }
}
