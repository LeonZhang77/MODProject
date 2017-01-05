using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Wicresoft.BadmintonSystem.Entity;
using Wicresoft.BadmintonSystem.DataAccess.DataProvider;
using Wicresoft.BadmintonSystem.DataAccess.IDataProvider;


namespace Badminton.Models.DataManage
{
    public class MemberModel:BaseViewModel
    {
        static IBadmintionDataProvider provider;

        public MemberModel()
        {
            provider = new BadmintionDataProvider();
        }
        public String Name
        {
            get;
            set;
        }
        public bool Male
        {
            get;
            set;
        }

        public bool IsUse
        {
            get;
            set;
        }

        public MemberInfo GetEntity()
        {
            MemberInfo memberInfo = new MemberInfo();
            memberInfo.ID = this.ID;
            memberInfo.Name = this.Name;
            memberInfo.Male = this.Male;
            return memberInfo;
        }

        public static MemberModel GetViewModel(MemberInfo memberInfo)
        {
            MemberModel model = new MemberModel();
            model.ID = memberInfo.ID;
            model.Name = memberInfo.Name;
            model.Male = memberInfo.Male;

            model.IsUse = false;
            if (provider.GetMemberAndClubRelations(memberInfo).Count() > 0) model.IsUse = true;
            if (provider.GetBonusInfos(memberInfo).Count() > 0) model.IsUse = true;
            if (provider.GetMatchInfos(memberInfo, true).Count() > 0) model.IsUse = true;
            if (provider.GetMatchInfos(memberInfo, false).Count() > 0) model.IsUse = true;

            return model;
        }
    }
}