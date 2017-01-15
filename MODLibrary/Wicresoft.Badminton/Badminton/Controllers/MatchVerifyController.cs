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
    }
}