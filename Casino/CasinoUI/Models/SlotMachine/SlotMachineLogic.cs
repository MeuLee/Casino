using CasinoUI.Models.PlayerModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace CasinoUI.Models.SlotMachine
{
    public class SlotMachineLogic : ISlotMachineAction
    {
        public Action<SlotMachineIcon[,]> OnIconsUpdate { get; set; }
        public Action<int> OnCreditsValueChanged { get; set; }
        private readonly HumanPlayer _human;
        private static readonly Random _r;
        private static List<Point>[] _payLines;
        private static readonly int SCREEN_HEIGHT = 3, SCREEN_WIDTH = 5, TOTAL_HEIGHT = 150;
        private static readonly double FRUIT_MUL = 1.25, SUIT_MUL = 1.5, BELL_MUL = 2.5, SEVEN_MUL = 5;
        private SlotMachineIcon[,] _columns;
        private SlotMachineIcon[,] _screen;
        private int _credits = 0;
        private int Credits
        {
            get
            {
                return _credits;
            }
            set
            {
                _credits = value;
                OnCreditsValueChanged?.Invoke(_credits);
            }
        }

        #region static ctor
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
        #endregion

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

        #region Spin
        public void PullLever(int bet)
        {
            Spin();
            CalculateWinnings(bet);
        }

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

        private void CalculateWinnings(int bet)
        {
            Credits -= bet;
            double winnings = 0;
            foreach (var payline in _payLines)
            {
                var paylineIcons = InitializePaylineIcons(payline);
                winnings += CalculateWinnings(paylineIcons);
            }

            int creditsEarned = (int)Math.Round(winnings * bet);
            Credits += creditsEarned;
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
        private double CalculateWinnings(SlotMachineIcon[] paylineIcons)
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

        private double CalculateWinnings(double baseValue, int nb)
        {
            if (nb <= 2) return 0;
            return Math.Pow(baseValue, nb);
        }
        #endregion

        /// <summary>
        /// Decreases the human player's money by the money arg and gives him credits equal to 4 times the money he spent.
        /// </summary>
        /// <param name="money">The amount of money the player spent</param>
        /// <returns>The amount of credits generated</returns>
        public int AddCredits(int money)
        {
            _human.Money -= money;
            Credits += money * 4;
            return money * 4;
        }

        /// <summary>
        /// Sets the amount of credits to 0 and increases the human player's money by 4 times the amount of credits left.
        /// </summary>
        /// <returns>The money gained by cashing out.</returns>
        public int CashOut()
        {
            int moneyFromCredits = Credits / 4;
            _human.Money += moneyFromCredits;
            Credits = 0;
            return moneyFromCredits;
        }
    }
}
