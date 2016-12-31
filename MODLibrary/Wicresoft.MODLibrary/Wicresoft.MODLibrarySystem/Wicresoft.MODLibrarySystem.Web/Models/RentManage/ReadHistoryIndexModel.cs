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
    public class ReadHistoryIndexModel : BaseIndexListModel<BorrowAndReturnRecordInfo>
    {
        public ReadHistoryIndexModel()
        {
            this.ReadHistoryModelList = new List<ReadHistoryModel>();
        }

        public List<ReadHistoryModel> ReadHistoryModelList
        {
            get;
            private set;
        }

        public static ReadHistoryModel GetViewModel(BorrowAndReturnRecordInfo info)
        {
            ReadHistoryModel model = new ReadHistoryModel();

            model.ID = info.ID;
            model.Title = info.BookDetailInfo.BookInfo.BookName;
            model.UserName = info.UserInfo.DisplayName;
            model.RentDateFrom = info.Borrow_Date.ToString(Unity.UntityContent.IOSDateTemplate);
            model.RentDateTo = info.Return_Date.ToString(Unity.UntityContent.IOSDateTemplate);

            return model;
        }

        public static ReadHistoryIndexModel GetReadHistoryModelList(Int32 pageIndex = 0)
        {
            ReadHistoryIndexModel returnModel = new ReadHistoryIndexModel();
            IBorrowAndReturnRecordInfoDataProvider dataProvider = new BorrowAndReturnRecordInfoDataProvider();
            List<BorrowAndReturnRecordInfo> tempList = dataProvider.GetBorrowAndReturnRecordListByStatus(RentRecordStatus.Returned).OrderByDescending(b => b.Return_Date).ToList();

            PagingContent<BorrowAndReturnRecordInfo> paging = new PagingContent<BorrowAndReturnRecordInfo>(tempList, pageIndex);

            foreach (var item in paging.EntityList)
            {
                returnModel.ReadHistoryModelList.Add(ReadHistoryIndexModel.GetViewModel(item));
            }

            returnModel.PagingContent = paging;

            return returnModel;
        }
    }
}