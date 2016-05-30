using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Wicresoft.MODLibrarySystem.Entity;
using Wicresoft.MODLibrarySystem.DataAccess.DataProvider;
using Wicresoft.MODLibrarySystem.DataAccess.IDataProvider;
using Wicresoft.MODLibrarySystem.Web.Models.BookManage;

namespace Wicresoft.MODLibrarySystem.Web.Models.RentManage
{
    public class RentManageIndexModel : BaseIndexListModel<BookInfo>
    {
        

        public RentManageIndexModel()
        {
            this.UserRequestList = new List<UserRequestModel>();
            this.WaitingForReturnModelList = new List<WaitingForReturnModel>();
            this.WaitingForTakeModelList = new List<WaitingForTakeModel>();
            this.ReadHistoryModelList = new List<ReadHistoryModel>();
        }
        public List<UserRequestModel> UserRequestList
        {
            get;
            private set;
        }
        public List<ReadHistoryModel> ReadHistoryModelList
        {
            get;
            private set;
        }
        public List<WaitingForTakeModel> WaitingForTakeModelList
        {
            get;
            private set;
        }
        public List<WaitingForReturnModel> WaitingForReturnModelList
        {
            get;
            private set;
        }

        public static RentManageIndexModel GetViewModel()
        {
            
            RentManageIndexModel model = new RentManageIndexModel();
            model.UserRequestList = GetUserRequestList();
            return model;
        }

        public static List<UserRequestModel> GetUserRequestList()
        {
            List<UserRequestModel> returnList = new List<UserRequestModel>();
            IBorrowAndReturnRecordInfoDataProvider dataProvider = new BorrowAndReturnRecordInfoDataProvider();
            List<BorrowAndReturnRecordInfo> tempList = dataProvider.GetBorrowAndReturnRecordListByStatus(RentRecordStatus.Pending).ToList();
            int count;
            if (tempList.Count() > 5)
            {
                count = 5;
            }
            else
            {
                count = tempList.Count();
            }
            for (int i = 0; i < count; i++)
            {
                UserRequestModel userRequestModel = new UserRequestModel();
                userRequestModel.Title = tempList[i].BookDetailInfo.BookInfo.BookName;
                string displayName = string.Empty;
                string authorNameValue = string.Empty;
                userRequestModel.Author = BookModel.GetAuthorName(tempList[i].BookDetailInfo.BookInfo, out displayName, out authorNameValue);
                userRequestModel.Publish = tempList[i].BookDetailInfo.UserInfo.DisplayName;
                userRequestModel.Email = tempList[i].BookDetailInfo.UserInfo.Email;
                userRequestModel.Floor = tempList[i].BookDetailInfo.UserInfo.Floor.ToString();
            }
            return returnList;
        }
    }
}
