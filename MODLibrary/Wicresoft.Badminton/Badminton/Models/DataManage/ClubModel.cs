using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Wicresoft.BadmintonSystem.Entity;
using Wicresoft.BadmintonSystem.DataAccess.DataProvider;
using Wicresoft.BadmintonSystem.DataAccess.IDataProvider;

namespace Badminton.Models.DataManage
{
    public class ClubModel:BaseViewModel
    {
        static IBadmintionDataProvider provider;

        public ClubModel()
        {
            provider = new BadmintionDataProvider(); 
        }
        
        public String Name
        {
            get;
            set;
        }
        public String Description
        {
            get;
            set;
        }

        public bool IsUse
        {
            get;
            set;
        }

        public String CaptainName
        {
            get;
            set;
        }

        public int CaptainID 
        {
            get;
            set;
        }

        public ClubInfo GetEntity()
        {
            ClubInfo clubInfo = new ClubInfo();
            clubInfo.ID = this.ID;
            clubInfo.Name = this.Name;
            clubInfo.Description = this.Description;
            return clubInfo;
        }

        public static ClubModel GetViewModel(ClubInfo club)
        {
            ClubModel model = new ClubModel();
            model.ID = club.ID;
            model.Name = club.Name;
            model.Description = club.Description;

            var memberAndClub = provider.GetMemberAndClubRelations(club);
            model.IsUse = false;
            if (memberAndClub.ToList().Count() > 0)
            {
                model.IsUse = true;
                foreach (MemberAndClubRelation info in memberAndClub.ToList())
                {
                    if (info.IsCaption)
                    {
                        model.CaptainName = info.MemberID.Name;
                    }
                }
            }

            return model;
        }
    }
}