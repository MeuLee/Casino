﻿namespace CasinoUI.Models.Blackjack
{
    public interface IBlackjackAction : IGameType
    {
        void BlackjackHit();
        void BlackjackStand();
        void BlackjackInsurance();
        int BlackjackDoubleDown(int currentBet);

        int PlayerHandValue { get; set; }

        bool PlayerStand { get; set; }

        bool PlayerBust { get; set; }

        bool Has21 { get; set; }
    }
}
