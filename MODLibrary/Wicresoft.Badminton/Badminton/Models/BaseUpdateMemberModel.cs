using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Wicresoft.BadmintonSystem.Entity;
using Wicresoft.BadmintonSystem.DataAccess.DataProvider;
using Wicresoft.BadmintonSystem.DataAccess.IDataProvider;


namespace Badminton.Models
{
    public class BaseUpdateMemberModel:BaseViewModel
    {
        public String MemberName
        {
            get;
            set;
        }

        public long OriginalScore
        {
            get;
            set;
        }

        public long UpdateScore
        {
            get;
            set;
        }

        public long OriginalRank
        {
            get;
            set;
        }

        public long UpdateRank
        {
            get;
            set;
        }

        public String Comments
        {
            get;
            set;
        }

        public static MemberInfo GetEntity(BaseUpdateMemberModel item)
        {
            IBadmintionDataProvider provider = new BadmintionDataProvider();
            MemberInfo memberInfo = provider.GetMemberInfoByID(item.ID);
            memberInfo.Score = Int32.Parse(item.UpdateScore.ToString());
            memberInfo.UpdateDate = DateTime.Now;
            return memberInfo;

        }
    }
}