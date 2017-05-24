using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Badminton.Models.FinalScoreManage;
using Wicresoft.BadmintonSystem.DataAccess.DataProvider;
using Wicresoft.BadmintonSystem.DataAccess.IDataProvider;
using Wicresoft.BadmintonSystem.Entity;
using Wicresoft.BadmintonSystem.Unity;
using Newtonsoft.Json.Linq;

namespace Badminton.Models.FinalScoreManage
{
    public class FinalScoreManageIndexModel:BaseViewModel
    {
        public FinalScoreManageIndexModel()
        {
            this.FinalScoreBonusList = new List<FinalScoreBonusInfo>();
            this.AddScoreInfoList = new List<AddScoreInfo>();
            this.UpdateMembersList = new List<UpdateMembers>();
            this.Parameters = new ParametersModel();
        }

        public ParametersModel Parameters
        {
            get;
            set;
        }

        public List<FinalScoreBonusInfo> FinalScoreBonusList
        {
            get;
            set;
        }

        public List<AddScoreInfo> AddScoreInfoList
        {
            get;
            set;
        }

        public List<UpdateMembers> UpdateMembersList
        {
            get;
            set;
        }

        
    }
}