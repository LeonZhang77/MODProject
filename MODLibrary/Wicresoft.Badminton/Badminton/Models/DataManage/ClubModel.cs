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

            model.IsUse = false;
            if (provider.GetMemberAndClubRelations(club).Count() > 0) model.IsUse = true;

            return model;
        }
    }
}