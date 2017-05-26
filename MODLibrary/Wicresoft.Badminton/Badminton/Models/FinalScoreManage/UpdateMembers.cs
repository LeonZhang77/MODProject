using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Wicresoft.BadmintonSystem.Entity;
using Wicresoft.BadmintonSystem.Unity;

namespace Badminton.Models.FinalScoreManage
{
    public class UpdateMembers:BaseUpdateMemberModel
    {
        public static UpdateMembers GetModel(MemberInfo item, List<DataHelper.MemberRank> rankList)
        {
            UpdateMembers updateMember = new UpdateMembers();
            updateMember.ID = item.ID;
            updateMember.MemberName = item.Name;
            updateMember.OriginalScore = item.Score;
            updateMember.OriginalRank = rankList.Find(u => u.MemberID == item.ID).Rank;
            updateMember.UpdateScore = 100;

            return updateMember;
        }
    }   
}