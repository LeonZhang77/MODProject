using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Wicresoft.BadmintonSystem.Entity;
using Wicresoft.BadmintonSystem.DataAccess.DataProvider;
using Wicresoft.BadmintonSystem.DataAccess.IDataProvider;

namespace Badminton.Models.FinalScoreManage
{
    public class FinalScoreBonusInfo:BaseViewModel
    {
        public long MemberID
        {
            get;
            set;
        }

        public String MembernName
        {
            get;
            set;
        }

        public long ChampionID
        {
            get;
            set;
        }

        public String ChampionTitle
        {
            get;
            set;
        }

        public long BonusTypeID
        {
            get;
            set;
        }

        public String BonusTypeDescription
        {
            get;
            set;
        }
        public long Score
        {
            get;
            set;
        }

        public static BonusInfo GetEntity(FinalScoreBonusInfo item)
        {
            IBadmintionDataProvider provider = new BadmintionDataProvider();
            BonusInfo bonusInfo = new BonusInfo();
            bonusInfo.BonusType = (BonusType)Enum.ToObject(typeof(BonusType), item.BonusTypeID);
            bonusInfo.ChampionID = provider.GetChampionshipInfoByID(item.ChampionID);
            bonusInfo.CreateTime = DateTime.Now;            
            bonusInfo.MemberID = provider.GetMemberInfoByID(item.MemberID);
            bonusInfo.Score = Int32.Parse(item.Score.ToString());
            bonusInfo.Updated = true;
            return bonusInfo;
        }
    }
}