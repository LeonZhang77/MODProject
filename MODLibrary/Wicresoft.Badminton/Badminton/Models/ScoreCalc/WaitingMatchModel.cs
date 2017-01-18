using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Wicresoft.BadmintonSystem.Entity;

namespace Badminton.Models.ScoreCalc
{
    public class WaitingMatchModel:BaseViewModel
    {
        public String MatchDate
        {
            get;
            set;
        }
        public String MatchTitle
        {
            get;
            set;
        }
        public String MatchScore
        {
            get;
            set;
        }

        public long Winner1ID
        {
            get;
            set;
        }
        public String Winner1Name
        {
            get;
            set;
        }

        public long Winner2ID
        {
            get;
            set;
        }
        public String Winner2Name
        {
            get;
            set;
        }

        public long Loser1ID
        {
            get;
            set;
        }
        public String Loser1Name
        {
            get;
            set;
        }

        public long Loser2ID
        {
            get;
            set;
        }
        public String Loser2Name
        {
            get;
            set;
        }

        public String Comments
        {
            get;
            set;
        }

        public static WaitingMatchModel GetViewModel(MatchInfo item)
        {
            WaitingMatchModel model = new WaitingMatchModel();
            model.ID = item.ID;
            model.MatchDate = item.MatchDate.ToString("MM-dd-yyyy");
            model.MatchTitle = item.ChampionID.Title;
            model.MatchScore = item.WinnerPoints.ToString() + ":" + item.LoserPoints.ToString();
            model.Winner1ID = item.WinnerID.ID;
            model.Winner1Name = item.WinnerID.Name;
            model.Loser1ID = item.LoserID.ID;
            model.Loser1Name = item.LoserID.Name;
            if (item.WinnerID2 != null)
            {
                model.Winner2ID = item.WinnerID2.ID;
                model.Winner2Name = item.WinnerID2.Name;
                model.Loser2ID = item.LoserID2.ID;
                model.Loser2Name = item.LoserID2.Name;
            }
            return model;
        }
    }
}