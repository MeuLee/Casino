using CasinoUI.Model.Cards;
using System;
using System.Collections.Generic;

namespace CasinoUI.Model.Poker
{
    class PokerHand
    {
        private List<Card> listCardInGame;
        private List<Card> listValue;

        private List<Tuple<int, int>> ComboValuePoss;
        private List<int> ComboStraightPoss;
        private Card.CardSuit FlushCombo;

        private bool isStraight = false;
        private bool isFlush = false;

        public PokerHand(List<Card> listCardInGame)
        {
            this.listCardInGame = listCardInGame;
            ComboStraightPoss = new List<int>();
            ComboValuePoss = new List<Tuple<int, int>>();
            FlushCombo = Card.CardSuit.Diamonds;
        }

        public int ListCardInGame { get; set; }

        private void CreateCombo()
        {

            DescendingValueList();
            OnlyValue();

            SameKindCombo();

            isStraight = isStraightCombo();
            if (isStraight)
            {
                StraightCombo();
            }

            DescendingSuitList();
            isFlush = isFlushCombo();
            if (isFlush)
            {
                ComboFlush();
            }

            //Si seulement straight ou flish calcul hignestCombo
        }
        private void ComboFlush()
        {
            double nbr = listCardInGame.Count / 2.0;
            int Middle =(int) Math.Ceiling(nbr);
            FlushCombo = listCardInGame[Middle].Suit;
        }

        private void StraightCombo()
        {
            CreateList();

            foreach(Card card in listValue)
            {
                if (ComboStraightPoss.Contains((int)card.Value))
                {
                    ComboStraightPoss.Remove((int)card.Value);
                }
            }
        }

        private void CreateList()
        {
            ComboStraightPoss = new List<int>(listCardInGame.Count + 4);
            int itemStraight = (int)listCardInGame[0].Value + 2;

            for (int i = 0; i < ComboStraightPoss.Count; i++)
            {
                switch (itemStraight)
                {
                    case 15:
                        itemStraight = 2;
                        break;
                    case 16:
                        itemStraight = 3;
                        break;
                    case 1:
                        itemStraight = 14;
                        break;
                    case 0:
                        itemStraight = 13;
                        break;
                }

                ComboStraightPoss.Add(itemStraight);
                itemStraight--;
            }
        }
        private void DescendingSuitList()
        {
            listCardInGame.Sort((a, b) => b.Suit.CompareTo(a.Suit));
        }

        private bool isFlushCombo()
        {
            bool isFlush = false;
            bool firstTime = true;
            int cardBefore = 0;
            int compt = 0;

            foreach(Card card in listCardInGame)
            {
                if (firstTime)
                {
                    cardBefore = (int)card.Suit;
                    firstTime = false;
                }
                else if (cardBefore == (int)card.Suit)
                {
                    compt++;
                }
                else
                {
                    cardBefore = (int)card.Suit;
                }
            }

            if (compt >= 2)
            {
                isFlush = true;
            }

            return isFlush;
        }

        private bool isStraightCombo()
        {
            bool isStraight = false;
            bool firstTime = true;
            bool blockCombo = false;

            int cardBefore = 0;
            int comptStraight = 0;

            foreach (Card card in listCardInGame)
            {

                int actuelCard = (int)card.Value;

                if (firstTime)
                {
                    cardBefore = (int)card.Value-1;
                    firstTime = false;
                }
                else if (cardBefore == actuelCard)
                {
                    cardBefore--;
                    comptStraight++;
                }
                else if (cardBefore - 1 == actuelCard && !blockCombo)
                {
                    cardBefore -= 2;
                    comptStraight++;

                    if (listCardInGame.Count < 4) {
                        blockCombo = true;
                    }
                }
                else if (cardBefore - 2 == actuelCard && !blockCombo)
                {
                    cardBefore -= 3;
                    comptStraight++;

                    if (listCardInGame.Count < 4)
                    {
                        blockCombo = true;
                    }
                }

            }

            if(comptStraight >= 2)
            {
                isStraight = true;
            }

            return isStraight;
        }

        private void SameKindCombo()
        {
            int taille = listValue.Count;
            int compt = 0;
            for(int i =0; i< listValue.Count; i++)
            {
                while (compt <= taille)
                {
                    if (compt < taille) {
                        ComboValuePoss.Add(Tuple.Create((int)listValue[i].Value, (int)listValue[compt].Value));
                    }
                    else
                    {
                        ComboValuePoss.Add(Tuple.Create((int)listValue[i].Value, 0));
                    }
                    compt++;
                }

                taille--;
            }
        }

        private void OnlyValue()
        {
            bool firstTime = true;
            Card.CardRank CardBefore = 0;

            foreach (Card card in listCardInGame) {

                if (firstTime)
                {
                    CardBefore = card.Value;
                    listValue.Add(card);
                    firstTime = false;
                }
                else if (CardBefore != card.Value)
                {
                    listValue.Add(card);
                    CardBefore = card.Value;
                }

            }
        }

        private void DescendingValueList()
        {
            listCardInGame.Sort((a, b) => b.Value.CompareTo(a.Value));
        }

    }
}
