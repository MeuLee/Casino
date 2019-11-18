using CasinoUI.Models.PlayerModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace CasinoUI.Models.SlotMachine
{
    public class SlotMachineLogic
    {
        public Action<SlotMachineIcon[,]> OnIconsUpdate { get; set; }
        public Action<int> OnPotEarned { get; set; }
        private readonly HumanPlayer _human;
        private static readonly Random _r;
        private static List<Point>[] _payLines;
        private static readonly int SCREEN_HEIGHT = 3, SCREEN_WIDTH = 5, TOTAL_HEIGHT = 150;
        private static readonly double FRUIT_MUL = 1.5, SUIT_MUL = 2.5, BELL_MUL = 4, SEVEN_MUL = 10;
        private SlotMachineIcon[,] _columns;
        private SlotMachineIcon[,] _screen;

        static SlotMachineLogic()
        {
            _r = new Random();
            InitPayLines();
        }

        private static void InitPayLines()
        {
            _payLines = new List<Point>[SCREEN_HEIGHT];
            for (int i = 0; i < SCREEN_HEIGHT; i++)
            {
                _payLines[i] = new List<Point>();
                for (int j = 0; j < SCREEN_WIDTH; j++)
                {
                    _payLines[i].Add(new Point(j, i));
                }
            }
        }

        public SlotMachineLogic(HumanPlayer human)
        {
            _human = human;
            _screen = new SlotMachineIcon[SCREEN_WIDTH, SCREEN_HEIGHT];
            InitColumns();
        }

        private void InitColumns()
        {
            _columns = new SlotMachineIcon[SCREEN_WIDTH, TOTAL_HEIGHT];
            for (int i = 0; i < _columns.GetLength(0); i++)
            {
                for (int j = 0; j < _columns.GetLength(1); j++)
                {
                    _columns[i, j] = SlotMachineIcon.GenerateRandomIcon();
                }
            }
        }

        public void PullLever()
        {
            Spin();
            CalculateWinnings();
        }

        /// <summary>
        /// For each column, generates a random number between 0 and the y-length of _columns.
        /// The random number corresponds to the starting index in the current column.
        /// Then, the sub-column from random number to random number + SCREEN_HEIGHT is assigned to the current column in the screen.
        /// Finally, the delegate is invoked, allowing the UI to manipulate it.
        /// </summary>
        private void Spin()
        {
            for (int i = 0; i < _columns.GetLength(0); i++)
            {
                int rowIndex = _r.Next(_columns.GetLength(1));
                for (int j = 0; j < _screen.GetLength(1); j++)
                {
                    _screen[i, j] = _columns[i,
                                            (rowIndex + j) % _columns.GetLength(1)]; // go back to 0 to prevent out of range
                }
            }
            OnIconsUpdate?.Invoke(_screen);
        }

        /// <summary>
        /// Calculates the winnings for every payline, and invokes the delegate containing the total winnings.
        /// </summary>
        private void CalculateWinnings()
        {
            int winnings = 0;
            foreach (var payline in _payLines)
            {
                var paylineIcons = InitializePaylineIcons(payline);
                winnings += CalculateWinnings(paylineIcons);
            }
            OnPotEarned?.Invoke(winnings);
        }

        private SlotMachineIcon[] InitializePaylineIcons(List<Point> payline)
        {
            var icons = new SlotMachineIcon[payline.Count];

            for (int i = 0; i < payline.Count; i++)
            {
                Point p = payline[i];
                icons[i] = _screen[(int)p.X, (int)p.Y];
            }

            return icons;
        }

        /// <summary>
        /// Calculates the winnings for one payline.
        /// </summary>
        /// <param name="paylineIcons">The payline to have its winnings calculated</param>
        /// <returns>The winnings.</returns>
        private int CalculateWinnings(SlotMachineIcon[] paylineIcons)
        {
            int nbFruits = paylineIcons.Count(i => i.IsFruit()),
                nbSuits = paylineIcons.Count(i => i.IsSuit()),
                nbBells = paylineIcons.Count(i => i.Icon == SlotMachineIcons.Bell),
                nbSeven = paylineIcons.Count(i => i.Icon == SlotMachineIcons.Seven);

            return CalculateWinnings(FRUIT_MUL, nbFruits) +
                   CalculateWinnings(SUIT_MUL, nbSuits) +
                   CalculateWinnings(BELL_MUL, nbBells) +
                   CalculateWinnings(SEVEN_MUL, nbSeven);
        }

        private int CalculateWinnings(double baseValue, int nb)
        {
            if (nb <= 2) return 0;
            return (int)Math.Pow(baseValue, nb);
        }
    }
}
