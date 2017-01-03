﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Badminton.Models.ScoreList;
using Badminton.Models.MemberManage;
using Badminton.Models.Chart;
using Newtonsoft.Json.Linq;
using Wicresoft.BadmintonSystem.DataAccess.IDataProvider;
using Wicresoft.BadmintonSystem.DataAccess.DataProvider;
using Wicresoft.BadmintonSystem.Entity;
using Wicresoft.BadmintonSystem.Entity;

namespace Badminton.Controllers
{
    public class ScoreListController : Controller
    {
        // GET: ScoreList
        IBadmintionDataProvider provider;
        
        public ScoreListController()
        {
            provider = new BadmintionDataProvider();
        }
        
        public ActionResult Index(long searchselectedID = 0)
        {
            ScoreListIndexModel model = new ScoreListIndexModel();

            model.ScoreList = GetScoreList();          

            model.SearchMemberList = GetSearchMemberList();           
            
            return View(model);
        }

        internal List<ScoreMemberModel> GetScoreList()
        {
            List<ScoreMemberModel> returnList = new List<ScoreMemberModel>();
            List<MemberInfo> memberList = provider.GetMemberInfos().ToList();
            
            ScoreMemberModel temp;
            foreach (var item in memberList)
            {
                temp = new ScoreMemberModel();
                temp.ID = item.ID;
                temp.MemberName = item.Name;
                temp.Male = item.Male;
                temp.Score = item.Score;
                temp.WinRate = GetWinRate(item.ID);
                temp.AverageWinRate = GetAverageWinRate(item.ID);
                //temp.Ranking = 1;                
                returnList.Add(temp);
            }                        
            return returnList;
        }

        internal double GetWinRate(long ID)
        {
            double returnValue = 0;
            List<MatchInfo> matchList = provider.GetMatchInfos(ID, true,ChampionType.Normal,false).ToList();
            int WinCount = matchList.Count();
            matchList = provider.GetMatchInfos(ID, false, ChampionType.Normal, false).ToList();            
            int LostCount = matchList.Count();
            if (WinCount != 0 && LostCount != 0)
            {
                returnValue = WinCount / (WinCount + LostCount);
            }
           
            return returnValue;
        }

        internal double GetAverageWinRate(long ID)
        {
            double returnValue = 0;
            List<MatchInfo> matchList = provider.GetMatchInfos(ID, true).ToList();
            int WinCount = matchList.Count();
            matchList = provider.GetMatchInfos(ID, false).ToList();
            int LostCount = matchList.Count();
            if (WinCount != 0 && LostCount != 0)
            {
                returnValue = WinCount / (WinCount + LostCount);
            }
            
            return returnValue;
        }

        internal List<SelectListItem> GetSearchMemberList()
        {
            List<SelectListItem> returnList = new List<SelectListItem>();
            List<MemberInfo> memberList = provider.GetMemberInfos().ToList();
            SelectListItem temp;
            foreach (MemberInfo item in memberList)
            {
                temp = new SelectListItem();
                temp.Text = item.Name;
                temp.Value = item.ID.ToString();
                returnList.Add(temp);
            }
            //SelectListItem temp = new SelectListItem();
            //temp.Text = "牛江磊";
            //temp.Value = "1";
            //returnList.Add(temp);
            //temp = new SelectListItem();
            //temp.Text = "郭秋浩";
            //temp.Value = "2";
            //returnList.Add(temp);
            //temp = new SelectListItem();
            //temp.Text = "张剑峰";
            //temp.Value = "3";
            //returnList.Add(temp);
            //temp = new SelectListItem();
            //temp.Text = "大花";
            //temp.Value = "4";
            //returnList.Add(temp);
            //temp = new SelectListItem();
            //temp.Text = "二花";
            //temp.Value = "5";
            //returnList.Add(temp);
            //temp = new SelectListItem();
            //temp.Text = "三花";
            //temp.Value = "6";
            //returnList.Add(temp);
            return returnList;
        }


        [HttpPost]
        public ActionResult Index(ScoreListIndexModel model)
        {
            return RedirectToAction("Index", new
            {
                searchselectedID = model.SearchSelectedID
            });
        }

        //internal String GetPlayersNames(long id1, long id2, out String name2)
        internal String GetPlayersNames(String q, out long id1, out long id2, out String name2)
        {
            String name1 = "";
            name2 = "";
            JObject obj = JObject.Parse(q);
            id1 = long.Parse((string)obj["player1"]);
            id2 = long.Parse((string)obj["player2"]);
            List<SelectListItem> tempList = GetSearchMemberList();
            foreach (SelectListItem item in tempList)
            {
                if (long.Parse(item.Value) == id1)
                {
                    name1 = item.Text;
                }
                else if (long.Parse(item.Value) == id2)
                {
                    name2 = item.Text;
                }
            }

            return name1;
        }
        public JsonResult GetJSONForSingles(String q)
        {
            long player1Id, player2Id;
            String player1Name, player2Name;
            player1Name = GetPlayersNames(q, out player1Id, out player2Id, out player2Name);

            List<ChartModel.returnListForPie> returnList = new List<ChartModel.returnListForPie>();

            ChartModel.returnListForPie temp = new ChartModel.returnListForPie();
            temp.value = 0.9;
            temp.title = player1Name;
            temp.color = "#F38630";
            returnList.Add(temp);
            temp = new ChartModel.returnListForPie();
            temp.value = 1 - 0.9;
            temp.title = player2Name;
            temp.color = "#E0E4CC";
            returnList.Add(temp);
            return Json(returnList, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetJSONForDoubles(string q)
        {
            long player1Id, player2Id;
            String player1Name, player2Name;
            player1Name = GetPlayersNames(q, out player1Id, out player2Id, out player2Name);
            
            List<ChartModel.returnListForPie> returnList = new List<ChartModel.returnListForPie>();

            ChartModel.returnListForPie temp = new ChartModel.returnListForPie();
            temp.value = 0.6;
            temp.color = "#F38630";
            returnList.Add(temp);
            temp = new ChartModel.returnListForPie();
            temp.value = 1 - 0.6;
            temp.color = "#E0E4CC";
            returnList.Add(temp);
            return Json(returnList, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetJSONForMixedDoubles(String q)
        {
            long player1Id, player2Id;
            String player1Name, player2Name;
            player1Name = GetPlayersNames(q, out player1Id, out player2Id, out player2Name);

            List<ChartModel.returnListForPie> returnList = new List<ChartModel.returnListForPie>();

            ChartModel.returnListForPie temp = new ChartModel.returnListForPie();
            temp.value = 0.75;
            temp.color = "#F38630";
            returnList.Add(temp);
            temp = new ChartModel.returnListForPie();
            temp.value = 1 - 0.75;
            temp.color = "#E0E4CC";
            returnList.Add(temp);
            return Json(returnList, JsonRequestBehavior.AllowGet);
        }
    }
}