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

        public PokerHand(List<Card> listCardInGame)
        {
            this.listCardInGame = listCardInGame;
        }

        public int ListCardInGame { get; set; }

        private void CheckCombo()
        {
            bool isStraight = false;
            bool isFlush = false;

            DescendingValueList();
            OnlyValue();

            SameKindCombo();

            isStraight = isStraightCombo();
            //Creer Methode possibilite straight

            DescendingSuitList();
            isFlush = isFlushCombo();
        }

        private void HighestCombo()
        {

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
            }

            if (compt >= 3)
            {
                isFlush = true;
            }

            return isFlush;
        }

        private bool isStraightCombo()
        {
            bool isStraight = false;
            bool firstTime = true;
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
                else if (cardBefore - 1 == actuelCard)
                {
                    cardBefore -= 2;
                    comptStraight++;
                }
                else if (cardBefore - 2 == actuelCard)
                {
                    cardBefore -= 3;
                    comptStraight++;
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
