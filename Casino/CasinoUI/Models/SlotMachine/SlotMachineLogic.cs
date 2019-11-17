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
        public SlotMachineIcon[,] Screen { get; private set; }

        static SlotMachineLogic()
        {
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
            InitScreen(); 
        }

        private void InitScreen()
        {
            Screen = new SlotMachineIcon[SCREEN_WIDTH, SCREEN_HEIGHT];
            for (int i = 0; i < Screen.GetLength(0); i++)
            {
                for (int j = 0; j < Screen.GetLength(1); j++)
                {
                    Screen[i, j] = SlotMachineIcon.GenerateRandomIcon();
                }
            }
        }
    }
}
