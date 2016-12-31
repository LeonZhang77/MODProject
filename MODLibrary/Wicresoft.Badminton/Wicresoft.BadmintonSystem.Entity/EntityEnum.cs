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
        [Description("Mix Sin")]
        MixSin = 1,
        [Description("Male Sin")]
        MaleSin = 2,
        [Description("Female Sin")]
        FemaleSin = 3,
        [Description("Male Dou")]
        MaleDou = 4,
        [Description("Female Dou")]
        FemaleDou = 5,
        [Description("Mix Dou")]
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
