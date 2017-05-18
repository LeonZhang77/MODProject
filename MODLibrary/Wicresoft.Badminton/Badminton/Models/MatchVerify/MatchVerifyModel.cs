using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Wicresoft.BadmintonSystem.DataAccess.IDataProvider;
using Wicresoft.BadmintonSystem.Entity;
using Wicresoft.BadmintonSystem.Unity;

namespace Badminton.Models.MatchVerify
{
    public class MatchVerifyModel : BaseViewModel
    {
        public List<SelectListItem> Winner1
        {
            get;
            set;
        }
        public int WinnerID1
        {
            get;
            set;
        }
        public List<SelectListItem> Loser1
        {
            get;
            set;
        }
        public int LoserID1
        {
            get;
            set;
        }
        public List<SelectListItem> Winner2
        {
            get;
            set;
        }
        public int WinnerID2
        {
            get;
            set;
        }
        public List<SelectListItem> Loser2
        {
            get;
            set;
        }
        public int LoserID2
        {
            get;
            set;
        }

        public int WinPoints
        {
            get;
            set;
        }

        public int LosePoints
        {
            get;
            set;
        }

        public String InputPerson
        {
            get;
            set;
        }

        public String MatchDate
        {
            get;
            set;
        }

        public List<SelectListItem> ChampionshipTitle
        {
            get;
            set;
        }
        public int ChampionshipID
        {
            get;
            set;
        }        
        public int MatchTypeID
        {
            get;
            set;
        }
        public Boolean IsChange 
        {
            get;
            set;
        }

        public static MatchVerifyModel GetViewModel(MatchInfo item, IBadmintionDataProvider provider)
        {
            MatchVerifyModel matchModel = new MatchVerifyModel();
            matchModel.ID = item.ID;
            matchModel.MatchDate = item.MatchDate.ToString("MM/dd/yyyy");
            matchModel.InputPerson = item.InputPersonID.Name;
            matchModel.ChampionshipTitle = MatchVerifyModel.ChampionToSelectItemUtil(provider.GetActiveChampionshipInfos(), item.ChampionID.ID);
            matchModel.WinPoints = item.WinnerPoints;
            matchModel.LosePoints = item.LoserPoints;
            matchModel.Winner1 = MatchVerifyModel.MemberToSelectItemUtil(provider.GetMemberInfos(), item.WinnerID.ID);
            matchModel.Loser1 = MatchVerifyModel.MemberToSelectItemUtil(provider.GetMemberInfos(), item.LoserID.ID);            

            if (item.ChampionID.CompetingType == CompetingType.FemaleSin || item.ChampionID.CompetingType == CompetingType.MaleSin || item.ChampionID.CompetingType == CompetingType.MixSin)
            {
                matchModel.Winner2 = null;
                matchModel.Loser2 = null;
            }
            else
            {
                matchModel.Winner2 = MatchVerifyModel.MemberToSelectItemUtil(provider.GetMemberInfos(), item.WinnerID2.ID);
                matchModel.Loser2 = MatchVerifyModel.MemberToSelectItemUtil(provider.GetMemberInfos(), item.LoserID2.ID);
            }
            return matchModel;
        }

        public static List<SelectListItem> MemberToSelectItemUtil(IEnumerable<MemberInfo> members, long ID)
        {
            List<SelectListItem> returnlist = new List<SelectListItem>();
            foreach (var member in members)
            {
                SelectListItem select = new SelectListItem();
                select.Value = member.ID.ToString();
                select.Text = member.Name;
                if (ID == member.ID)
                { select.Selected = true; }
                else
                { select.Selected = false; }
                returnlist.Add(select); 
            }
            return returnlist;
        }
        public static List<SelectListItem> ChampionToSelectItemUtil(IEnumerable<ChampionshipInfo> champions, long ID)
        {
            List<SelectListItem> returnlist = new List<SelectListItem>();
            foreach (var champion in champions)
            {
                SelectListItem select = new SelectListItem();
                select.Value = champion.ID.ToString();
                select.Text = champion.Title;
                if (ID == champion.ID)
                { select.Selected = true; }
                else
                { select.Selected = false; }
                returnlist.Add(select);
            }
            return returnlist;
        }

    }
}