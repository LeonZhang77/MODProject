using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Wicresoft.BadmintonSystem.Entity;
using Wicresoft.BadmintonSystem.Unity;

namespace Badminton.Models.MatchVerify
{
    public class MatchModel:BaseViewModel
    {
        public String Winner1Name
        {
            get;
            set;
        }
        public String Loser1Name
        {
            get;
            set;
        }
        public String Winner2Name
        {
            get;
            set;
        }
        public String Loser2Name
        {
            get;
            set;
        }

        public String WinLosePoints
        {
            get;
            set;
        }

        public String InputPersonName
        {
            get;
            set;
        }

        public String MatchDate
        {
            get;
            set;
        }

        public String ChampionshipTitle
        {
            get;
            set;
        }

        public static MatchModel GetViewModel(MatchInfo item)
        {
            MatchModel matchModel = new MatchModel();
            matchModel.ID = item.ID;
            matchModel.MatchDate = item.MatchDate.ToString("MM/dd/yyyy");
            matchModel.InputPersonName = item.InputPersonID.Name;
            matchModel.ChampionshipTitle = item.ChampionID.Title;
            matchModel.WinLosePoints = item.WinnerPoints.ToString() + ":" + item.LoserPoints.ToString();
            matchModel.Winner1Name = item.WinnerID.Name;
            matchModel.Loser1Name = item.LoserID.Name;
            if (item.ChampionID.CompetingType == CompetingType.FemaleSin || item.ChampionID.CompetingType == CompetingType.MaleSin || item.ChampionID.CompetingType == CompetingType.MixSin)
            {
                matchModel.Winner2Name = "";
                matchModel.Loser2Name = "";
            }
            else
            {
                matchModel.Winner2Name = item.WinnerID2.Name;
                matchModel.Loser2Name = item.LoserID2.Name;
            }

            return matchModel;
        }
    }
}