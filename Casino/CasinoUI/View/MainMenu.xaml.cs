using CasinoUI.Model.Cards;
using System;
using System.Drawing;
﻿using CasinoUI.View;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace CasinoUI
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private GameCardStack _cards = new GameCardStack();
        public MainWindow()
        {
            //Button btn = new Button();
            //btn.Name = "Button";
            //btn.Click += button_Click;
            //InitializeComponent();
        }


        private void AddImageToCanvas(Card card)
        {
            //MyCanvas.CardsToDraw.Add(card);
            //MyCanvas.InvalidateVisual();
        }

        private void BtnAddCards_Click(object sender, RoutedEventArgs e)
        {
            if (_cards.Cards.Count < 1) return;

            Card card = _cards.Cards[0];
            AddImageToCanvas(card);
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            CasinoGame game = new CasinoGame();
            game.Show();
            this.Close();
        }
    }
}
