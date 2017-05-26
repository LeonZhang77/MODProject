using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Wicresoft.BadmintonSystem.Entity;
using Wicresoft.BadmintonSystem.Unity;

namespace Badminton.Models.ScoreCalc
{
    public class ScoreUpdateMember:BaseUpdateMemberModel
    {
        public static ScoreUpdateMember GetModel(MemberInfo item, List<DataHelper.MemberRank> rankList)
        {
            ScoreUpdateMember updateMember = new ScoreUpdateMember();
            updateMember.ID = item.ID;
            updateMember.MemberName = item.Name;
            updateMember.OriginalScore = item.Score;
            updateMember.OriginalRank = rankList.Find(u => u.MemberID == item.ID).Rank;
            updateMember.UpdateScore = 100;

            return updateMember;
        }
    }
}