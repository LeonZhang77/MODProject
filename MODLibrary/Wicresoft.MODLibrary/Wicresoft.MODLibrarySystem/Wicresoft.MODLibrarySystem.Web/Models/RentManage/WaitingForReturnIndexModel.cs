using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Wicresoft.MODLibrarySystem.Entity;
using Wicresoft.MODLibrarySystem.Unity;
using Wicresoft.MODLibrarySystem.DataAccess.DataProvider;
using Wicresoft.MODLibrarySystem.DataAccess.IDataProvider;
using Wicresoft.MODLibrarySystem.Web.Models.BookManage;
using Wicresoft.MODLibrarySystem.Unity.Helper;

namespace Wicresoft.MODLibrarySystem.Web.Models.RentManage
{
    public class WaitingForReturnIndexModel : BaseIndexListModel<BorrowAndReturnRecordInfo>
    {
        public WaitingForReturnIndexModel()
        {
            this.WaitingForReturnModelList = new List<WaitingForReturnModel>();
        }

        public List<WaitingForReturnModel> WaitingForReturnModelList
        {
            get;
            private set;
        }

        public static WaitingForReturnModel GetViewModel(BorrowAndReturnRecordInfo info)
        {
            WaitingForReturnModel model = new WaitingForReturnModel();

            model.ID = info.ID;
            model.Title = info.BookDetailInfo.BookInfo.BookName;
            model.UserName = info.UserInfo.DisplayName;
            model.ExpirationDay = info.Forcast_Date.ToString(UntityContent.IOSDateTemplate);
            model.Delay = DateTime.Today < info.Forcast_Date ? "" : (DateTime.Today - info.Forcast_Date).Days.ToString();

            return model;
        }

        public static WaitingForReturnIndexModel GetWaitingForReturnList(Int32 pageIndex = 0)
        {
            WaitingForReturnIndexModel returnModel = new WaitingForReturnIndexModel();
            IBorrowAndReturnRecordInfoDataProvider dataProvider = new BorrowAndReturnRecordInfoDataProvider();
            List<BorrowAndReturnRecordInfo> tempList = dataProvider.GetBorrowAndReturnRecordListByStatus(RentRecordStatus.Taked).ToList();
            tempList.OrderByDescending(b => b.Forcast_Date);

            PagingContent<BorrowAndReturnRecordInfo> paging = new PagingContent<BorrowAndReturnRecordInfo>(tempList, pageIndex);

            foreach (var item in paging.EntityList)
            {
                returnModel.WaitingForReturnModelList.Add(WaitingForReturnIndexModel.GetViewModel(item));
            }

            returnModel.PagingContent = paging;

            return returnModel;
        }
    }
}