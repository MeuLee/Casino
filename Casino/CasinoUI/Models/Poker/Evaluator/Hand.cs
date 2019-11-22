using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CasinoUI.Models.Poker.Evaluator
{
    public enum Hand
    {
        Nothing = 1,
        OnePair,
        TwoPairs,
        ThreeKind,
        Straight,
        Flush,
        FullHouse,
        FourKind,
        StraightFlush,
        RoyalFlush
    }
}
