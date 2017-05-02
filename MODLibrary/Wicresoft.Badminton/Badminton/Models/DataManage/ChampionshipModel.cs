using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Wicresoft.BadmintonSystem.DataAccess.DataProvider;
using Wicresoft.BadmintonSystem.DataAccess.IDataProvider;
using Wicresoft.BadmintonSystem.Entity;
using Wicresoft.BadmintonSystem.Unity;
using System.Web.Mvc;

namespace Badminton.Models.DataManage
{
    public class ChampionshipModel:BaseViewModel
    {
        
        public String Title
        {
            get;
            set;
        }

        public string StartDate
        {
            get;
            set;
        }
        public string EndDate
        {
            get;
            set;
        }

        public string ChampionType
        {
            get;
            set;
        }
        public int ChampionTypeID
        {
            get;
            set;
        }
        public ChampionType ChampionTypeSelected
        {
            get;
            set;
        }

        public string CompetingType
        {
            get;
            set;
        }
        public int CompetingTypeID
        {
            get;
            set;
        }
        public CompetingType CompetingTypeSelected
        {
            get;
            set;
        }
        public bool IsUse
        {
            get;
            set;
        }

        public bool IsActive 
        {
            get;
            set;
        }

        public ChampionshipInfo GetEntity()
        {
            ChampionshipInfo returnInfo = new ChampionshipInfo();
            returnInfo.ID = this.ID;
            returnInfo.Title = this.Title;
            returnInfo.StartDate = Convert.ToDateTime(this.StartDate);
            returnInfo.EndDate = Convert.ToDateTime(this.EndDate);
            returnInfo.ChampionType = this.ChampionTypeSelected;
            returnInfo.CompetingType = this.CompetingTypeSelected;
            returnInfo.CreateTime = DateTime.Now;
            returnInfo.IsActive = this.IsActive;

            return returnInfo;
        }

        public static ChampionshipModel GetViewModel(ChampionshipInfo championshipInfo,IBadmintionDataProvider provider)
        {
            ChampionshipModel model = new ChampionshipModel();
            model.ID = championshipInfo.ID;
            model.Title = championshipInfo.Title;
            model.StartDate = championshipInfo.StartDate.ToString("MM/dd/yyyy");
            model.EndDate = championshipInfo.EndDate.ToString("MM/dd/yyyy");
            model.CompetingType = EnumHelper.GetEnumDescription(championshipInfo.CompetingType);
            model.ChampionType = EnumHelper.GetEnumDescription(championshipInfo.ChampionType);
            model.IsActive = championshipInfo.IsActive;
            model.IsUse = false;
            if (provider.GetMatchInfoByChampionID(championshipInfo.ID).Count()>0) 
            {
                model.IsUse = true;
            }
            

            return model;
        }    
    }
}