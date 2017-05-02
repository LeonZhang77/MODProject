using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Wicresoft.BadmintonSystem.DataAccess.DataProvider;
using Wicresoft.BadmintonSystem.DataAccess.IDataProvider;
using Wicresoft.BadmintonSystem.Entity;

namespace Badminton.Models.DataManage
{
    public class MemberAndClubRelationsModel : BaseViewModel
    {
        static IBadmintionDataProvider provider;

        public MemberAndClubRelationsModel()
        {
            provider = new BadmintionDataProvider();
        }

        public MemberInfo MemberID
        {
            get;
            set;
        }

        public ClubInfo ClubID
        {
            get;
            set;
        }

        public Boolean IsCaption
        {
            get;
            set;
        }

        public string Captain
        {
            get;
            set;
        }

        public static MemberAndClubRelationsModel GetViewModel(MemberAndClubRelation memberAndClub)
        {
            MemberAndClubRelationsModel model = new MemberAndClubRelationsModel();
            model.ID = memberAndClub.ID;
            model.ClubID = memberAndClub.ClubID;
            model.CreateTime = memberAndClub.CreateTime;
            model.MemberID = memberAndClub.MemberID;
            model.IsCaption = memberAndClub.IsCaption;
            if (memberAndClub.IsCaption)
            {
                model.Captain = memberAndClub.MemberID.Name;
            }
            return model;
        }

        public static MemberAndClubRelation GetEntity(MemberAndClubRelationsModel model)
        {
            MemberAndClubRelation info = new MemberAndClubRelation();
            info.ID = model.ID;
            info.MemberID = model.MemberID;
            info.ClubID = model.ClubID;
            info.CreateTime = model.CreateTime;
            info.IsCaption = model.IsCaption;
            return info;
        }
    }
}