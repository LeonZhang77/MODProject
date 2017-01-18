using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Badminton.Models.ScoreCalc
{
    public class ScoreCalcIndexModel
    {

        public ScoreCalcIndexModel()
        {
            this.WaitingMatchList = new List<WaitingMatchModel>();
            this.Parameters = new ParametersModel();
            this.UpdateMemberList = new List<ScoreUpdateMember>();
            this.AddBonusInfoList = new List<AddBonusInfo>();
            this.AddScoreInfoList = new List<AddScoreInfo>();
        }


        public List<WaitingMatchModel> WaitingMatchList
        {
            get;
            set;
        }

        public ParametersModel Parameters 
        {
            get;
            set;
        }
        public List<ScoreUpdateMember> UpdateMemberList
        {
            get;
            set;
        }

        public List<AddBonusInfo> AddBonusInfoList
        {
            get;
            set;
        }

        public List<AddScoreInfo> AddScoreInfoList
        {
            get;
            set;
        }
    }
}