using CasinoUI.Models.Blackjack;
using CasinoUI.Models.Cards;
using System.Collections.Generic;

namespace CasinoUI.Models.PlayerModel
{
    public class Player
    {
        public int Money { get; set; }

        public List<Card> GetHand()
        {
            if (this is HumanPlayer hp)
            {
                return hp.CurrentProfile.Hand;
            }
            else if (this is PlayerAI ai)
            {
                return ai.Hand;
            }
            throw new System.Exception("\"this\" should be human or ai");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T">Must be of type GameType, such as IPokerAction, IBlackjackAction etc.</typeparam>
        /// <returns></returns>
        public T GetGameType<T>() where T : IGameType
        {
            if (this is HumanPlayer hp && hp.CurrentProfile is T type)
            {
                return type;
            }
            else if (this is PlayerAI ai && ai is T aiType)
            {
                return aiType;
            }
            throw new System.Exception("see comment above method");
        }
    }
}
