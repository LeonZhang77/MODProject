using System;
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

namespace Badminton.Controllers
{
    public class ScoreListController : Controller
    {
        // GET: ScoreList
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
            IBadmintionDataProvider provider = new BadmintionDataProvider();
            List<MemberInfo> memberList = provider.GetMemberInfos().ToList();

            foreach (var item in memberList)
            {
                ScoreMemberModel temp = new ScoreMemberModel();
                temp.ID = item.ID;
                temp.MemberName = item.Name;
                temp.Male = true;
                temp.Score = 100;
                temp.WinRate = 0.99;
                temp.AverageWinRate = 0.55;
                temp.Ranking = 1;
                temp.LastUpdateDate = System.DateTime.Now;

                returnList.Add(temp);
            }
            
            
            return returnList;
        }

        internal List<SelectListItem> GetSearchMemberList()
        {
            List<SelectListItem> returnList = new List<SelectListItem>();
            SelectListItem temp = new SelectListItem();
            temp.Text = "牛江磊";
            temp.Value = "1";
            returnList.Add(temp);
            temp = new SelectListItem();
            temp.Text = "郭秋浩";
            temp.Value = "2";
            returnList.Add(temp);
            temp = new SelectListItem();
            temp.Text = "张剑峰";
            temp.Value = "3";
            returnList.Add(temp);
            temp = new SelectListItem();
            temp.Text = "大花";
            temp.Value = "4";
            returnList.Add(temp);
            temp = new SelectListItem();
            temp.Text = "二花";
            temp.Value = "5";
            returnList.Add(temp);
            temp = new SelectListItem();
            temp.Text = "三花";
            temp.Value = "6";
            returnList.Add(temp);
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