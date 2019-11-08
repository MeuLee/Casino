using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CasinoUI.Models.PlayerModel.PlayerSkin
{
    class SkinsGenerator
    {
        private const int IMAGE_WIDTH = 32, IMAGE_HEIGHT = 32;

        internal static Dictionary<Skins, Skin> Skins { get; } = new Dictionary<Skins, Skin>();

        static SkinsGenerator()
        {
            GenerateSkins();
        }

        private static void GenerateSkins()
        {
            var list = Enum.GetValues(typeof(Skins)).Cast<Skins>().ToList();
            for (int i = 0; i < list.Count; i++)
            {
                Skins skinType = list[i];
                Skins.Add(skinType, new Skin());
            }
        }

        
    }
}
