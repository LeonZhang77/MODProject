using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Badminton.Models.MatchVerify;
using Wicresoft.BadmintonSystem.DataAccess.DataProvider;
using Wicresoft.BadmintonSystem.DataAccess.IDataProvider;
using Wicresoft.BadmintonSystem.Entity;
using Wicresoft.BadmintonSystem.Unity;
using Newtonsoft.Json.Linq;

namespace Badminton.Controllers
{
    public class MatchVerifyController : Controller
    {
        IBadmintionDataProvider provider;

        public MatchVerifyController()
        {
            provider = new BadmintionDataProvider();
        }
        public ActionResult Index()
        {
            MatchVerifyIndexModel model = new MatchVerifyIndexModel();
            List<MatchInfo> matchList = provider.GetMatchInfos().Where(u => !u.Updated).ToList();
            MatchModel matchModel;
            MatchVerifyModel matchVerifyModel;
            foreach (MatchInfo item in matchList)
            {
                matchModel = MatchModel.GetViewModel(item);
                matchVerifyModel = MatchVerifyModel.GetViewModel(item,provider);
                if (item.Verified)
                {
                    model.WaitingForScoreList.Add(matchModel);
                }
                else
                {
                    model.WaitingForVerifyList.Add(matchVerifyModel);
                }
            }
            MatchInputController match = new MatchInputController();
            model.ChampionshipList = match.GetTrueActiveChampionshipList();
            model.ChampionshipCompetingTypeList = match.GetChampionshipCompetingTypeList();
            model.CompetingList = EnumHelper.GetEnumIEnumerable(CompetingType.FemaleDou);
            model.SearchMemberList = match.GetSearchMemberList();
            model.SearchMaleMemberList = match.GetSearchMemberList(true);
            model.SearchFemaleMemberList = match.GetSearchMemberList(false);
            model.MatchTypeList = EnumHelper.GetEnumIEnumerable(MatchType.Top4);
            model.TipMessage = this.TempData["message"] as String;
            return View(model);
        }

        public bool DeleteMatch(long q)
        {
            try
            {
                MatchInfo info = provider.GetMatchInfoByID(q);
                provider.DeleteMatchInfo(info);
            }
            catch (Exception ex) {
                this.TempData["message"] = EnumHelper.GetEnumDescription(MessagePoint.DelFail);
                return false; 
            }
            this.TempData["message"] = EnumHelper.GetEnumDescription(MessagePoint.DelSucc);
            return true;
        }

        public bool ValidMatch(long[] q)
        {
            try
            {
                List<MatchInfo> matchList = new List<MatchInfo>();
                foreach(long item in q)
                {
                    MatchInfo info = provider.GetMatchInfoByID(item);
                    info.Verified = true;
                    info.VerifyDate = DateTime.Now;
                    matchList.Add(info);
                }
                provider.BatchUpdateMatchInfoValid(matchList);
                
            }
            catch (Exception ex)
            {
                this.TempData["message"] = EnumHelper.GetEnumDescription(MessagePoint.VerifyFail);
                return false;
            }
            this.TempData["message"] = EnumHelper.GetEnumDescription(MessagePoint.VerifySucc);
            return true;
        }

        public bool NotValidMatch(long[] q)
        {
            try
            {
                List<MatchInfo> matchList = new List<MatchInfo>();
                foreach (long item in q)
                {
                    MatchInfo info = provider.GetMatchInfoByID(item);
                    info.Verified = false;
                    info.VerifyDate = DateTime.Now;
                    matchList.Add(info);
                }
                provider.BatchUpdateMatchInfoValid(matchList);
                this.TempData["message"] = EnumHelper.GetEnumDescription(MessagePoint.RecallSucc);
                return true;
            }
            catch (Exception ex) {
                this.TempData["message"] = EnumHelper.GetEnumDescription(MessagePoint.RecallFail);
                return false;
            }
        }

        [HttpPost]
        public ActionResult BatchEditMatch(MatchVerifyIndexModel models)
        {
            try
            {
                foreach (MatchVerifyModel model in models.WaitingForVerifyList)
                {
                    if(model.IsChange)
                    {
                        MatchInfo info = provider.GetMatchInfoByID(model.ID);
                        info.MatchDate = DateTime.Parse(model.MatchDate);
                        info.ChampionID = provider.GetChampionshipInfoByID(model.ChampionshipID);
                        info.WinnerPoints = model.WinPoints;
                        info.LoserPoints = model.LosePoints;
                        info.WinnerID = provider.GetMemberInfoByID(model.WinnerID1);
                        info.LoserID = provider.GetMemberInfoByID(model.LoserID1);
                        if (!EnumHelper.GetEnumDescription((info.ChampionID.CompetingType)).Contains("Singles"))
                        {
                            info.WinnerID2 = provider.GetMemberInfoByID(model.WinnerID2);
                            info.LoserID2 = provider.GetMemberInfoByID(model.LoserID2);
                        }
                        info.MatchType = (MatchType)Enum.GetValues(typeof(MatchType)).GetValue(model.MatchTypeID);
                        provider.UpdateMatchInfo(info);
                    }
                }
                this.TempData["message"] = EnumHelper.GetEnumDescription(MessagePoint.EditSucc);
                return RedirectToAction("Index", "MatchVerify");
            }
            catch (Exception ex)
            {
                this.TempData["message"] = EnumHelper.GetEnumDescription(MessagePoint.EditFail)+ex.ToString();
                return RedirectToAction("Index", "MatchVerify");
            }

        }
    }
}