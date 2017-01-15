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
            List<MatchInfo> matchList = provider.GetMatchInfos().Where(u => !u.Updated ).ToList();
            MatchModel matchModel;
            foreach (MatchInfo item in matchList)
            {
                matchModel = MatchModel.GetViewModel(item);
                if (item.Verified)
                {
                    model.WaitingForScoreList.Add(matchModel); 
                }
                else 
                { 
                    model.WaitingForVerifyList.Add(matchModel); 
                }
                
               
            }
            return View(model);
        }

        public string DeleteMatch(long q)
        {
            try
            {
                MatchInfo info = provider.GetMatchInfoByID(q);
                provider.DeleteMatchInfo(info);
            }
            catch (Exception ex) { return ex.Message; };

            return "true";
        }

        public string ValidMatch(long q)
        {
            try
            {
                MatchInfo info = provider.GetMatchInfoByID(q);
                info.Verified = true;
                info.VerifyDate = DateTime.Now;                
                provider.UpdateMatchInfo(info);
            }
            catch (Exception ex) { return ex.Message; };

            return "true";
        }

        public string NotValidMatch(long q)
        {
            try
            {
                MatchInfo info = provider.GetMatchInfoByID(q);
                info.Verified = false;
                info.VerifyDate = DateTime.Now;
                provider.UpdateMatchInfo(info);
            }
            catch (Exception ex) { return ex.Message; };

            return "true";
        }
        
    }
}