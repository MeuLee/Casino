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
        public static readonly double FRUIT_CHANCE = 0.1125;
        public static readonly double SUIT_CHANCE = 0.08125;
        public static readonly double BELL_CHANCE = 0.125;
        public static readonly double SEVEN_CHANCE = 0.1;
        private static Random _r = new Random();
        private static Dictionary<SlotMachineIcons, double> _odds
            = new Dictionary<SlotMachineIcons, double>
            {
                { SlotMachineIcons.Cherry, FRUIT_CHANCE },
                { SlotMachineIcons.Watermelon, FRUIT_CHANCE },
                { SlotMachineIcons.Pear, FRUIT_CHANCE },
                { SlotMachineIcons.Banana, FRUIT_CHANCE },
                { SlotMachineIcons.Heart, SUIT_CHANCE },
                { SlotMachineIcons.Diamond, SUIT_CHANCE },
                { SlotMachineIcons.Clubs, SUIT_CHANCE },
                { SlotMachineIcons.Spades, SUIT_CHANCE },
                { SlotMachineIcons.Bell, BELL_CHANCE },
                { SlotMachineIcons.Seven, SEVEN_CHANCE }
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

        public bool IsFruit()
        {
            switch (Icon)
            {
                case SlotMachineIcons.Cherry:
                case SlotMachineIcons.Watermelon:
                case SlotMachineIcons.Pear:
                case SlotMachineIcons.Banana:
                    return true;
                default:
                    return false;
            }
        }

        public bool IsSuit()
        {
            switch (Icon)
            {
                case SlotMachineIcons.Heart:
                case SlotMachineIcons.Diamond:
                case SlotMachineIcons.Clubs:
                case SlotMachineIcons.Spades:
                    return true;
                default:
                    return false;
            }
        }
    }
}
