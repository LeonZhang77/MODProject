using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Wicresoft.MODLibrarySystem.Entity;
using Wicresoft.MODLibrarySystem.DataAccess.DataProvider;
using Wicresoft.MODLibrarySystem.DataAccess.IDataProvider;
using Wicresoft.MODLibrarySystem.Web.Models.BookManage;
using Wicresoft.MODLibrarySystem.Unity.Helper;

namespace Wicresoft.MODLibrarySystem.Web.Models.RentManage
{
    public class WaitingForTakeIndexModel : BaseIndexListModel<BorrowAndReturnRecordInfo>
    {
        public WaitingForTakeIndexModel()
        {
            this.WaitingForTakeModelList = new List<WaitingForTakeModel>();
        }

        public List<WaitingForTakeModel> WaitingForTakeModelList
        {
            get;
            private set;
        }

        public static WaitingForTakeModel GetViewModel(BorrowAndReturnRecordInfo info)
        {
            WaitingForTakeModel model = new WaitingForTakeModel();

            model.ID = info.ID;
            model.Title = info.BookDetailInfo.BookInfo.BookName;
            model.UserName = info.UserInfo.DisplayName;
            model.Email = info.UserInfo.Email;
            model.Floor = info.UserInfo.Floor.ToString();

            return model;
        }

        public static WaitingForTakeIndexModel GetWaitingForTakeList(Int32 pageIndex = 0)
        {
            WaitingForTakeIndexModel returnModel = new WaitingForTakeIndexModel();
            IBorrowAndReturnRecordInfoDataProvider dataProvider = new BorrowAndReturnRecordInfoDataProvider();
            List<BorrowAndReturnRecordInfo> tempList = dataProvider.GetBorrowAndReturnRecordListByStatus(RentRecordStatus.Approved).ToList();
            tempList.OrderByDescending(b => b.CreateTime);

            PagingContent<BorrowAndReturnRecordInfo> paging = new PagingContent<BorrowAndReturnRecordInfo>(tempList, pageIndex);

            foreach (var item in paging.EntityList)
            {
                returnModel.WaitingForTakeModelList.Add(WaitingForTakeIndexModel.GetViewModel(item));
            }

            returnModel.PagingContent = paging;

            return returnModel;
        }
    }

}