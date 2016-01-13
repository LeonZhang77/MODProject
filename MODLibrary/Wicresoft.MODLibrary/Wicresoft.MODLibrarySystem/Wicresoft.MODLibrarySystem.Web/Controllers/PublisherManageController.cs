using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Wicresoft.MODLibrarySystem.Entity;
using Wicresoft.MODLibrarySystem.Entity.Condition.PublisherInfo;
using Wicresoft.MODLibrarySystem.Unity.Helper;
using Wicresoft.MODLibrarySystem.Web.Models.PublisherManage;
using Wicresoft.MODLibrarySystem.DataAccess.DataProvider;
using Wicresoft.MODLibrarySystem.DataAccess.IDataProvider;

namespace Wicresoft.MODLibrarySystem.Web.Controllers
{
    public class PublisherManageController : BaseController
    {
        private IPublisherInfoDataProvider IPublisherInfoDataProvider;
        public PublisherManageController()
        {
            this.IPublisherInfoDataProvider = new PublisherInfoDataProvider();
        }
        // GET: PublisherManage
        public ActionResult Index(String name, Int32 pageIndex = 0)
        {
            if(name != null)
            {
                name = name.Trim();
            }

            PublisherManageIndexModel model = new PublisherManageIndexModel();
            model.FilterName = name;

            PublisherInfoCondition condition = new PublisherInfoCondition();
            condition.PublisherName = name;

            IEnumerable<PublisherInfo> publishers = this.IPublisherInfoDataProvider.GetPublisherList(condition);

            PagingContent<PublisherInfo> paging = new PagingContent<PublisherInfo>(publishers, pageIndex);

            foreach (var item in paging.EntityList)
            {
                model.PublisherModelList.Add(PublisherModel.GetViewModel(item));
            }

            model.PagingContent = paging;

            return View(model);

        }

        [HttpPost]
        public ActionResult Index(PublisherManageIndexModel model)
        {
            return RedirectToAction("Index", new
            {
                name = model.FilterName
            });
        }

        public ActionResult AddPublisher()
        {
            PublisherModel publisher = new PublisherModel();
            return View(publisher);
        }

        [HttpPost]
        public ActionResult AddPublisher(PublisherModel publisher)
        {
            PublisherInfo publisherInfo = publisher.GetEntity();
            PublisherInfoCondition condition = new PublisherInfoCondition();
            condition.PublisherName = publisherInfo.PublisherName;
            IEnumerable<PublisherInfo> publishers = this.IPublisherInfoDataProvider.GetPublisherList(condition);

            if (publishers.Count() > 0)
            {
                publisher.StateMessage = "The same publisher has already been exist!";
                publisher.ErrorState = true;
                return View(publisher);
            }
            else
            {
                this.IPublisherInfoDataProvider.Add(publisherInfo);
                return RedirectToAction("Index");
            }
        }

        public ActionResult EditPublisher(long id)
        {
            PublisherModel publisher = new PublisherModel();
            PublisherInfo publisherInfo = this.IPublisherInfoDataProvider.GetPublisherByID(id);
            if (publisherInfo != null)
            {
                publisher = PublisherModel.GetViewModel(publisherInfo);
            }

            return View(publisher);

        }

        [HttpPost]
        public ActionResult EditPublisher(PublisherModel publisher)
        {
            if (publisher != null)
            {
                this.IPublisherInfoDataProvider.Update(publisher.GetEntity());
            }
            return RedirectToAction("Index");
        }

        public ActionResult DeletePublisher(long id)
        {
            this.IPublisherInfoDataProvider.DeleteByID(id);
            return RedirectToAction("Index");
        }

        public JsonResult JsonGetPublisherByName(string q)
        {
            List<PublisherModel> list = new List<PublisherModel>();
            if (q.Length > 0)
            {
                PublisherInfoCondition condition = new PublisherInfoCondition();
                condition.PublisherName = q;
                IEnumerable<PublisherInfo> publishers = this.IPublisherInfoDataProvider.GetPublisherList(condition);
                foreach (var item in publishers)
                {
                    list.Add(PublisherModel.GetViewModel(item));
                }
            }
            return Json(list, JsonRequestBehavior.AllowGet);
        }
    }
}