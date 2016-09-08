using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Wicresoft.MODLibrarySystem.Entity;
using Wicresoft.MODLibrarySystem.Unity;
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
            model.WaitingForTakeModelList = GetWaitingForTakeList();
            model.WaitingForReturnModelList = GetWaitingForReturnList();
            model.ReadHistoryModelList = GetReadHistoryModelList();
            return model;
        }

        public static List<UserRequestModel> GetUserRequestList()
        {
            List<UserRequestModel> returnList = new List<UserRequestModel>();
            IBorrowAndReturnRecordInfoDataProvider dataProvider = new BorrowAndReturnRecordInfoDataProvider();
            List<BorrowAndReturnRecordInfo> tempList = dataProvider.GetBorrowAndReturnRecordListByStatus(RentRecordStatus.Pending).ToList();
            tempList.OrderByDescending(b => b.CreateTime);
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
                userRequestModel.ID = tempList[i].ID;
                userRequestModel.Title = tempList[i].BookDetailInfo.BookInfo.BookName;
                string displayName = string.Empty;
                string authorNameValue = string.Empty;
                userRequestModel.Author = BookModel.GetAuthorName(tempList[i].BookDetailInfo.BookInfo, out displayName, out authorNameValue);
                userRequestModel.Publish = tempList[i].BookDetailInfo.BookInfo.PublisherInfo.PublisherName;
                userRequestModel.UserName = tempList[i].UserInfo.DisplayName;
                userRequestModel.Email = tempList[i].UserInfo.Email;
                userRequestModel.Floor = tempList[i].UserInfo.Floor.ToString();
                returnList.Add(userRequestModel);
            }
            return returnList;
        }

        public static List<WaitingForTakeModel> GetWaitingForTakeList()
        {
            List<WaitingForTakeModel> returnList = new List<WaitingForTakeModel>();
            IBorrowAndReturnRecordInfoDataProvider dataProvider = new BorrowAndReturnRecordInfoDataProvider();
            List<BorrowAndReturnRecordInfo> tempList = dataProvider.GetBorrowAndReturnRecordListByStatus(RentRecordStatus.Approved).ToList();
            tempList.OrderByDescending(b => b.CreateTime);
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
                WaitingForTakeModel waitingForTakeModel = new WaitingForTakeModel();
                waitingForTakeModel.ID = tempList[i].ID;
                waitingForTakeModel.Title = tempList[i].BookDetailInfo.BookInfo.BookName;
                waitingForTakeModel.UserName = tempList[i].UserInfo.DisplayName;
                waitingForTakeModel.Email = tempList[i].UserInfo.Email;
                waitingForTakeModel.Floor = tempList[i].UserInfo.Floor.ToString();
                returnList.Add(waitingForTakeModel);
            }
            return returnList;
        }

        public static List<WaitingForReturnModel> GetWaitingForReturnList()
        {
            List<WaitingForReturnModel> returnList = new List<WaitingForReturnModel>();
            IBorrowAndReturnRecordInfoDataProvider dataProvider = new BorrowAndReturnRecordInfoDataProvider();
            List<BorrowAndReturnRecordInfo> tempList = dataProvider.GetBorrowAndReturnRecordListByStatus(RentRecordStatus.Taked).ToList();
            tempList.OrderByDescending(b => b.CreateTime);
            for (int i = 0; i < 5 && i < tempList.Count() ; i++)
            {
                WaitingForReturnModel waitingForReturnModel = new WaitingForReturnModel();
                waitingForReturnModel.ID = tempList[i].ID;
                waitingForReturnModel.Title = tempList[i].BookDetailInfo.BookInfo.BookName;
                waitingForReturnModel.UserName = tempList[i].UserInfo.DisplayName;
                waitingForReturnModel.ExpirationDay = tempList[i].Forcast_Date.ToString(UntityContent.IOSDateTemplate);
                waitingForReturnModel.Delay = DateTime.Today < tempList[i].Forcast_Date ? "" : (DateTime.Today - tempList[i].Forcast_Date).Days.ToString();
                returnList.Add(waitingForReturnModel);
            }
            return returnList;
        }

        public static List<ReadHistoryModel> GetReadHistoryModelList()
        {
            List<ReadHistoryModel> returnList = new List<ReadHistoryModel>();
            IBorrowAndReturnRecordInfoDataProvider dataProvider = new BorrowAndReturnRecordInfoDataProvider();
            List<BorrowAndReturnRecordInfo> tempList = dataProvider.GetBorrowAndReturnRecordListByStatus(RentRecordStatus.Returned).ToList();
            tempList.OrderByDescending(b => b.Return_Date);
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
                ReadHistoryModel model = new ReadHistoryModel();
                model.ID = tempList[i].ID;
                model.Title = tempList[i].BookDetailInfo.BookInfo.BookName;
                model.UserName = tempList[i].UserInfo.DisplayName;
               model.RentDateFrom = tempList[i].Borrow_Date.ToString(Unity.UntityContent.IOSDateTemplate);
                model.RentDateTo = tempList[i].Return_Date.ToString(Unity.UntityContent.IOSDateTemplate);
                returnList.Add(model);
            }
            return returnList;
        }
    }
    
}
