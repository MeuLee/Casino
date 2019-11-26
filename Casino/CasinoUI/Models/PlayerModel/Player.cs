using CasinoUI.Models.Cards;
using System;
using System.Collections.Generic;

namespace CasinoUI.Models.PlayerModel
{
    public class Player
    {
        public int Money { get; set; } = 1000;

        /// <summary>
        /// Throws: System.Exception, if the current (this) player is not a HumanPlayer or a PlayerAI. 
        /// </summary>
        /// <returns>The current (this) player's hand. </returns>
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
            throw new InvalidCastException($"{"this"} should be human or ai");
        }

        /// <summary>
        /// Usage: IPokerAction currentPlayer = humanPlayer.GetGameType<IPokerAction>();
        /// </summary>
        /// <typeparam name="T">Must be of type IGameType, such as IPokerAction, IBlackjackAction etc. Must be an interface</typeparam>
        /// <returns>The current player as T</returns>
        public T GetGameType<T>() where T : IGameType
        {
            if (this is HumanPlayer hp)
            {
                if (hp.CurrentProfile is T type)
                {
                    return type;
                }
                throw new InvalidCastException($"{"this"} is a HumanPlayer but is not of type T");
            }
            else if (this is PlayerAI ai)
            {
                if (ai is T aiType)
                {
                    return aiType;
                }
                throw new InvalidCastException($"{"this"} is a PlayerAI but is not of type T");
            }
            
            throw new InvalidCastException($"{"this"} should be human or ai");
        }
    }
}
