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
        [Description("Base")]
        Base = 3,
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
        [Description("Male's Singles")]
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

    public enum MatchType
    {
        [Description("")]
        Blank = 0,
        [Description("Finals")]
        Finals = 1,
        [Description("Top4")]
        Top4 = 2,
        [Description("Top8")]
        Top8 = 3
    }

    public enum MessagePoint 
    {
        [Description("增加成功")]
        AddSucc = 0,
        [Description("增加失败")]
        AddFail = 1,
        [Description("修改成功")]
        EditSucc = 2,
        [Description("修改失败")]
        EditFail = 3,
        [Description("审核失败")]
        VerifyFail = 4,
        [Description("审核成功")]
        VerifySucc = 5,
        [Description("撤销成功")]
        RecallSucc = 6,
        [Description("撤销失败")]
        RecallFail = 7,
        [Description("删除成功")]
        DelSucc = 8,
        [Description("删除失败")]
        DelFail = 9,
    }
}
