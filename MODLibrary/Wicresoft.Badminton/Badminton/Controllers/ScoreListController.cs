using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Badminton.Models.ScoreList;
using Badminton.Models.Chart;
using Newtonsoft.Json.Linq;
using Wicresoft.BadmintonSystem.DataAccess.IDataProvider;
using Wicresoft.BadmintonSystem.DataAccess.DataProvider;
using Wicresoft.BadmintonSystem.Entity;
using Wicresoft.BadmintonSystem.Unity;

namespace Badminton.Controllers
{
    public class ScoreListController : Controller
    {
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
                temp.WinRate = GetWinRate(item);
                temp.AverageWinRate = GetAverageWinRate(item);
                //temp.Ranking = 1;                
                returnList.Add(temp);
            }                        
            return returnList;
        }

        internal double GetWinRate(MemberInfo memberInfo)
        {
            double returnValue = 0;
            List<MatchInfo> matchInfos = provider.GetMatchInfos().ToList();
            List<MatchInfo> matchList = DataHelper.GetMatchInfos(memberInfo, true, ChampionType.Normal, false, matchInfos).ToList();
            int WinCount = matchList.Count();
            matchList = DataHelper.GetMatchInfos(memberInfo, false, ChampionType.Normal, false, matchInfos).ToList();            
            int LostCount = matchList.Count();
            if ((WinCount + LostCount) != 0)
            {
                returnValue = WinCount / (WinCount + LostCount);
            }
           
            return returnValue;
        }

        internal double GetAverageWinRate(MemberInfo memberInfo)
        {
            double returnValue = 0;
            List<MatchInfo> matchInfos = provider.GetMatchInfos().ToList();
            List<MatchInfo> matchList = DataHelper.GetMatchInfos(memberInfo, true, matchInfos).ToList();
            int WinCount = matchList.Count();
            matchList = DataHelper.GetMatchInfos(memberInfo, false, matchInfos).ToList();
            int LostCount = matchList.Count();
            if ((WinCount + LostCount) != 0)
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

        internal double GetPlayer1WinRate(long player1id, long player2id, CompetingType competingType)
        {
            double player1WinRate = 0.5;
            MemberInfo memberInfo = provider.GetMemberInfoByID(player1id);
            List<MatchInfo> matchInfos = provider.GetMatchInfos().ToList();
            List<MatchInfo> player1WinList = DataHelper.GetMatchInfos(memberInfo, true, competingType, matchInfos).ToList();
            List<MatchInfo> tempList;
            if (EnumHelper.GetEnumDescription(competingType).Contains("Singles"))
            {
                tempList = DataHelper.GetMatchInfos(memberInfo, true, CompetingType.MixSin, matchInfos).ToList();
                foreach (MatchInfo info in tempList)
                {
                    player1WinList.Add(info);
                }
            }

            foreach (MatchInfo item in player1WinList)
            {
                if (EnumHelper.GetEnumDescription(competingType).Contains("Singles"))
                {
                    if (item.LoserID.ID != player2id) player1WinList.Remove(item);
                }
                else
                {
                    if (item.LoserID.ID != player2id && item.LoserID2.ID != player2id) player1WinList.Remove(item);
                }                
            }
            List<MatchInfo> player1LostList = DataHelper.GetMatchInfos(memberInfo, false, competingType, matchInfos).ToList();
            if (EnumHelper.GetEnumDescription(competingType).Contains("Singles"))
            {
                tempList = DataHelper.GetMatchInfos(memberInfo, false, CompetingType.MixSin, matchInfos).ToList();
                foreach (MatchInfo info in tempList)
                {
                    player1WinList.Add(info);
                }
            }

            foreach (MatchInfo item in player1LostList)
            {
                if (EnumHelper.GetEnumDescription(competingType).Contains("Singles"))
                {
                    if (item.WinnerID.ID != player2id) player1LostList.Remove(item);
                }
                else
                {
                    if (item.WinnerID.ID != player2id && item.WinnerID2.ID != player2id) player1LostList.Remove(item);
                }                
            }

            if ((player1WinList.Count + player1LostList.Count()) != 0)
            {
                player1WinRate = player1WinList.Count() / (player1WinList.Count + player1LostList.Count());
            }
            return player1WinRate;
        }
        public JsonResult GetJSONForSingles(String q)
        {
            long player1Id, player2Id;
            String player1Name, player2Name;
            player1Name = GetPlayersNames(q, out player1Id, out player2Id, out player2Name);

            CompetingType competingType;
            MemberInfo player1Member = provider.GetMemberInfoByID(player1Id);
            if (player1Member.Male)
                competingType = CompetingType.MaleSin;
            else
                competingType = CompetingType.FemaleSin;
            double player1WinRate = GetPlayer1WinRate(player1Id, player2Id, competingType);

            List<ChartModel.returnListForPie> returnList = new List<ChartModel.returnListForPie>();
            ChartModel.returnListForPie temp = new ChartModel.returnListForPie();
            temp.value = player1WinRate;
            temp.title = player1Name;
            temp.color = "#F38630";
            returnList.Add(temp);
            temp = new ChartModel.returnListForPie();
            temp.value = 1 - player1WinRate;
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

            CompetingType competingType;
            MemberInfo player1Member = provider.GetMemberInfoByID(player1Id);
            if (player1Member.Male)
                competingType = CompetingType.MaleDou;
            else
                competingType = CompetingType.FemaleDou;
            double player1WinRate = GetPlayer1WinRate(player1Id, player2Id, competingType);

            List<ChartModel.returnListForPie> returnList = new List<ChartModel.returnListForPie>();
            ChartModel.returnListForPie temp = new ChartModel.returnListForPie();
            temp.value = player1WinRate;
            temp.color = "#F38630";
            returnList.Add(temp);
            temp = new ChartModel.returnListForPie();
            temp.value = 1 - player1WinRate;
            temp.color = "#E0E4CC";
            returnList.Add(temp);
            return Json(returnList, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetJSONForMixedDoubles(String q)
        {
            long player1Id, player2Id;
            String player1Name, player2Name;
            player1Name = GetPlayersNames(q, out player1Id, out player2Id, out player2Name);

            CompetingType competingType = CompetingType.MixDou;
            double player1WinRate = GetPlayer1WinRate(player1Id, player2Id, competingType);

            List<ChartModel.returnListForPie> returnList = new List<ChartModel.returnListForPie>();
            ChartModel.returnListForPie temp = new ChartModel.returnListForPie();
            temp.value = player1WinRate;
            temp.color = "#F38630";
            returnList.Add(temp);
            temp = new ChartModel.returnListForPie();
            temp.value = 1 - player1WinRate;
            temp.color = "#E0E4CC";
            returnList.Add(temp);
            return Json(returnList, JsonRequestBehavior.AllowGet);
        }
    }
}