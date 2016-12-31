using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Wicresoft.MODLibrarySystem.Entity;
using Wicresoft.MODLibrarySystem.Unity;

namespace Wicresoft.MODLibrarySystem.WebUI.Models.RentManage
{
    public class ReadHistoryModel : BaseViewModel
    {
        public ReadHistoryModel()
        {
            this.ReadHistoryModelList = new List<ReadHistoryModel>();
        }
        public String Title
        {
            get;
            set;
        }
        public List<ReadHistoryModel> ReadHistoryModelList
        {
            get;
            set;
        }

        public static ReadHistoryModel GetViewModel(BorrowAndReturnRecordInfo info)
        {
            ReadHistoryModel model = new ReadHistoryModel();

            model.ID = info.ID;
            model.Title = info.BookDetailInfo.BookInfo.BookName;

            return model;
        }
    }
}
