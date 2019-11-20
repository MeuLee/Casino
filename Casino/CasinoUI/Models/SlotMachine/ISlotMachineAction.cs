using System.Collections;

namespace CasinoUI.Models.SlotMachine
{
    interface ISlotMachineAction : IGameType
    {
        /// <summary>
        /// Loads credits into the slot machine, at a rate of 1$ = 4 credits.
        /// </summary>
        /// <param name="money">The amount, in dollars, that the player spends in the machine.</param>
        /// <returns>The amount of credits generated.</returns>
        int AddCredits(int money);

        /// <summary>
        /// The player stops playing; he gets back his money at a rate of 4 credits = 1$.
        /// </summary>
        /// <returns>The amount, in dollar, returned to the player.</returns>
        int CashOut();

        void PullLever(int bet);
    }
}
