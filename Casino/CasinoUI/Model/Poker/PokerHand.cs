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
        private List<Tuple<int, int>> ComboStraightPoss;

        private bool isStraight = false;
        private bool isFlush = false;

        public PokerHand(List<Card> listCardInGame)
        {
            this.listCardInGame = listCardInGame;
            ComboStraightPoss = new List<Tuple<int, int>>;
            ComboValuePoss = new List<Tuple<int, int>>;
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
            //Creer methode possibilite flush

            //Si seulement straight ou flish calcul hignestCombo
        }

        private void StraightCombo()
        {

            int createrOne = 0;
            int createrTwo = 0;

            int cardBefore = 0;

            for(int i = 0; i <listCardInGame.Count; i++)
            {
                createrOne = 0;
                createrTwo = 0;

                if (i == 0)
                {
                    createrOne = (int)listCardInGame[i].Value + 1;

                    if(createrOne > (int)Card.CardRank.Ace)
                    {
                        createrOne = 2;
                        createrTwo = createrOne + 1;

                    }
                    else
                    {
                        if (createrOne != 14)
                        {
                            createrTwo = (int)listCardInGame[i].Value + 2;
                        }
                        else
                        {
                            createrTwo = 2;
                        }
                    }
                }
                else if(i == listCardInGame.Count - 1)
                {
                    createrOne = (int)listCardInGame[i].Value - 1;

                    if (createrOne < (int)Card.CardRank.Two)
                    {
                        createrOne = 14;
                        createrTwo = createrOne - 1;

                    }
                    else
                    {
                        if (createrOne != 2)
                        {
                            createrTwo = (int)listCardInGame[i].Value - 2;
                        }
                        else
                        {
                            createrTwo = 14;
                        }
                    }
                }
                else
                {
                    if (cardBefore - 2 == (int)listCardInGame[i].Value)
                    {
                        createrOne = cardBefore - 1;
                    }
                    else if (cardBefore - 3 == (int)listCardInGame[i].Value)
                    {
                        createrOne = cardBefore - 1;
                        createrTwo = cardBefore - 2;

                        //Checker 2 14 13
                    }
                }

                ComboStraightPoss.Add(Tuple.Create(createrTwo, createrOne));
                cardBefore = (int)listCardInGame[i].Value;
            }
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
                    cardBefore = (int)card.Suit;
                    compt++;
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
