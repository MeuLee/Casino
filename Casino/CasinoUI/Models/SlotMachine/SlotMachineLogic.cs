using CasinoUI.Models.PlayerModel;
using System;
using System.Collections.Generic;
using System.Windows;

namespace CasinoUI.Models.SlotMachine
{
    public class SlotMachineLogic
    {
        private readonly HumanPlayer _human;
        private static readonly Random _r;
        private static List<Point>[] _payLines;
        private static readonly int SCREEN_HEIGHT = 3;
        private static readonly int SCREEN_WIDTH = 5;
        private static readonly int TOTAL_HEIGHT = 150;
        private SlotMachineIcon[,] _columns;

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

        /// <summary>
        /// For each column, generates a random number between 0 and the y-length of _columns.
        /// The random number corresponds to the starting index in the current column.
        /// Then, the sub-column from random number to random number + SCREEN_HEIGHT is assigned to the current column in the screen.
        /// </summary>
        /// <returns>A 2d array consisting of the SlotMachines to print on the screen.</returns>
        public SlotMachineIcon[,] Spin()
        {
            SlotMachineIcon[,] screen = new SlotMachineIcon[SCREEN_WIDTH, SCREEN_HEIGHT];
            for (int i = 0; i < _columns.GetLength(0); i++)
            {
                int rowIndex = _r.Next(_columns.GetLength(1));
                for (int j = 0; j < screen.GetLength(1); j++)
                {
                    screen[i, j] = _columns[i,
                                            (rowIndex + j) % _columns.GetLength(1)]; // go back to 0 to prevent out of range
                }
            }
            return screen;
        }
    }
}
