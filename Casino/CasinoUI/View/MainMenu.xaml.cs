using CasinoUI.Model.Cards;
using System;
using System.Drawing;
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
            InitializeComponent();
        }

        private void AddImageToCanvas(Card card)
        {
            MyCanvas.CardsToDraw.Add(card);
            MyCanvas.InvalidateVisual();
        }

        private void BtnAddCards_Click(object sender, RoutedEventArgs e)
        {
            if (_cards.Cards.Count < 1) return;

            Card card = _cards.Cards[0];
            AddImageToCanvas(card);
        }
    }
}
