using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Wicresoft.BadmintonSystem.Entity;
using Wicresoft.BadmintonSystem.DataAccess.DataProvider;
using Wicresoft.BadmintonSystem.DataAccess.IDataProvider;

namespace Badminton.Models.ScoreCalc
{
    public class AddScoreInfo:BaseViewModel
    {
        public long MemberID
        {
            get;
            set;
        }
        
        public String MemberName
        {
            get;
            set;
        }

        public long Score
        {
            get;
            set;
        }

        public String Comments
        {
            get;
            set;
        }

        public static ScoreInfo GetEntity(AddScoreInfo item)
        {
            IBadmintionDataProvider provider = new BadmintionDataProvider();
            ScoreInfo scoreInfo= new ScoreInfo();
            scoreInfo.MemberID = provider.GetMemberInfoByID(item.MemberID);
            scoreInfo.Score = Int32.Parse(item.Score.ToString());
            scoreInfo.CreateTime = DateTime.Now;
            scoreInfo.Comment = item.Comments;
            return scoreInfo;
        }
    }
}