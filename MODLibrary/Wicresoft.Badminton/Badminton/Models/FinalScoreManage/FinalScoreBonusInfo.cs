using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Wicresoft.BadmintonSystem.Entity;
using Wicresoft.BadmintonSystem.DataAccess.DataProvider;
using Wicresoft.BadmintonSystem.DataAccess.IDataProvider;
using Wicresoft.BadmintonSystem.Unity;

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

        public static FinalScoreBonusInfo GetModel(FinalScoreManageIndexModel modelInput, IBadmintionDataProvider provider)
        {
            FinalScoreBonusInfo bonusInfo = new FinalScoreBonusInfo();
            bonusInfo.CreateTime = DateTime.Now;
            bonusInfo.BonusTypeID = long.Parse(Convert.ToInt32(BonusType.Final).ToString());
            bonusInfo.BonusTypeDescription = EnumHelper.GetEnumDescription(BonusType.Final);
            bonusInfo.MemberID = long.Parse(modelInput.Parameters.MemberID);
            bonusInfo.MembernName = provider.GetMemberInfoByID(bonusInfo.MemberID).Name;
            bonusInfo.ChampionID = long.Parse(modelInput.Parameters.ChampionshipID);
            bonusInfo.ChampionTitle = provider.GetChampionshipInfoByID(bonusInfo.ChampionID).Title;
            bonusInfo.Score = modelInput.Parameters.Score;
            return bonusInfo;
        }
    }
}