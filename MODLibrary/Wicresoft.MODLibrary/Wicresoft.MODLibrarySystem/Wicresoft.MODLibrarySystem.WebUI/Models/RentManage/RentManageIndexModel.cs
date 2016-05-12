using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Wicresoft.MODLibrarySystem.Entity;

namespace Wicresoft.MODLibrarySystem.WebUI.Models.RentManage
{
    public class RentManageIndexModel : BaseIndexListModel<BookInfo>
    {
        public RentManageIndexModel()
        {
            this.MyRequestList = new List<MyRequestModel>();
            this.ReadHistoryModelList = new List<ReadHistoryModel>();
            this.BookInHandModelList = new List<BookInHandModel>();
        }
        public List<MyRequestModel> MyRequestList
        {
            get;
            private set;
        }
        public List<ReadHistoryModel> ReadHistoryModelList
        {
            get;
            private set;
        }
        public List<BookInHandModel> BookInHandModelList
        {
            get;
            private set;
        }
    }
}
