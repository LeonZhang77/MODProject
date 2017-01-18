using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Wicresoft.BadmintonSystem.Entity;
using Wicresoft.BadmintonSystem.DataAccess.DataProvider;
using Wicresoft.BadmintonSystem.DataAccess.IDataProvider;

namespace Badminton.Models.ScoreCalc
{
    public class AddBonusInfo:BaseViewModel
    {
        public long MatchID
        {
            get;
            set;
        }
        
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

        public static BonusInfo GetEntity(AddBonusInfo item)
        {
            IBadmintionDataProvider provider = new BadmintionDataProvider();
            BonusInfo bonusInfo = new BonusInfo();
            bonusInfo.BonusType = (BonusType)Enum.ToObject(typeof(BonusType), item.BonusTypeID);
            bonusInfo.ChampionID = provider.GetChampionshipInfoByID(item.ChampionID);
            bonusInfo.CreateTime = DateTime.Now;
            bonusInfo.MatchID = Int32.Parse(item.MatchID.ToString()); ;
            bonusInfo.MemberID = provider.GetMemberInfoByID(item.MemberID);
            bonusInfo.Score = Int32.Parse(item.Score.ToString());
            bonusInfo.Updated = true;
            return bonusInfo;
        }

    }
}