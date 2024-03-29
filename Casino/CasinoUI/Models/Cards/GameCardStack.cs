﻿using CasinoUI.Models.PlayerModel;
using CasinoUI.Utils;
using System;
using System.Collections.Generic;
using System.Drawing;

namespace CasinoUI.Models.Cards
{
    public class GameCardStack : CardStack
    {
        public GameCardStack()
        {
            FillCardStack();
            ShuffleCards();
        }

        private void ShuffleCards()
        {
            Random r = new Random();

            for (int i = 0; i < Cards.Count; i++)
            {
                int j = r.Next(Cards.Count);
                Cards.Swap(i, j);
            }
        }

        private void FillCardStack()
        {
            Cards = new List<Card>();
            foreach (var rank in Extensions.GetValues<Card.CardRank>())
            {
                int value = (int)rank;
                foreach (var suit in Extensions.GetValues<Card.CardSuit>())
                {
                    string suitPrefix = suit.ToString()[0].ToString();
                    string cardName = value == 14 ? $"1{suitPrefix}" : $"{value}{suitPrefix}";
                    var image = Properties.Resources.ResourceManager.GetObject(cardName) as Bitmap;
                    Cards.Add(new Card(rank, suit, image));
                }

            }
        }

        public void PlayerDrawCard(Player current)
        {
            Card card = Cards[0];
            current.Hand.Add(card);
            Cards.Remove(card);
        }

        public Card DrawCard() {
            Card card = Cards[0];
            Cards.Remove(card);
            return card;
        }
    }
}
