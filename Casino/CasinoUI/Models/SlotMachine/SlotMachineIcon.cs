using CasinoUI.Utils;
using System;
using System.Collections.Generic;
using System.Windows.Media.Imaging;

namespace CasinoUI.Models.SlotMachine
{
    public class SlotMachineIcon
    {
        public BitmapImage Image { get; private set; }
        public SlotMachineIcons Icon { get; private set; }
        private static Random _r = new Random();
        private static Dictionary<SlotMachineIcons, double> _odds
            = new Dictionary<SlotMachineIcons, double>
            {
                { SlotMachineIcons.Cherry, 0.125 },
                { SlotMachineIcons.Watermelon, 0.125 },
                { SlotMachineIcons.Pear, 0.125 },
                { SlotMachineIcons.Banana, 0.125 },
                { SlotMachineIcons.Heart, 0.1 },
                { SlotMachineIcons.Diamond, 0.1 },
                { SlotMachineIcons.Clubs, 0.1 },
                { SlotMachineIcons.Spades, 0.1 },
                { SlotMachineIcons.Bell, 0.0625 },
                { SlotMachineIcons.Seven, 0.0375 }
            };

        private SlotMachineIcon(SlotMachineIcons icon)
        {
            Icon = icon;
            // assign image based on icon value
        }

        public static SlotMachineIcon GenerateRandomIcon()
        {
            return new SlotMachineIcon(_r.Pick(_odds));
        }
    }
}
