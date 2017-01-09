using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wicresoft.BadmintonSystem.Entity
{
    public enum BonusType
    {
        [Description("Rank")]
        Rank = 1,
        [Description("Final")]
        Final = 2,
    }

    public enum ChampionType
    {
        [Description("Normal")]
        Normal = 1,
        [Description("Rank")]
        Rank = 2,
        [Description("Competitive")]
        Final = 3,

    }

    public enum CompetingType
    {
        [Description("Mixed Singles")]
        MixSin = 1,
        [Description("Men's Singles")]
        MaleSin = 2,
        [Description("Female's Singles")]
        FemaleSin = 3,
        [Description("Male's Double")]
        MaleDou = 4,
        [Description("Female's Double")]
        FemaleDou = 5,
        [Description("Mixed Double")]
        MixDou = 6,

    }

    public enum IgnoreType
    {
        [Description("Winner")]
        Winner = 1,
        [Description("Loser")]
        Loser = 2,
        [Description("Both")]
        Both = 3
    }
}
