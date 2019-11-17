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
                { SlotMachineIcons.Cherry, 0.16 },
                { SlotMachineIcons.Watermelon, 0.16 },
                { SlotMachineIcons.Pear, 0.16 },
                { SlotMachineIcons.Banana, 0.16 },
                { SlotMachineIcons.Heart, 0.08 },
                { SlotMachineIcons.Diamond, 0.08 },
                { SlotMachineIcons.Clubs, 0.08 },
                { SlotMachineIcons.Spades, 0.08 },
                { SlotMachineIcons.Bell, 0.03 },
                { SlotMachineIcons.Seven, 0.01 }
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
