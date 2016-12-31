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
            count = tempList.Count() > 5 ? 5 : tempList.Count();
            for (int i = 0; i < count; i++)
            {
                UserRequestModel userRequestModel = GetUserRequestModel(tempList[i]);
                returnList.Add(userRequestModel);
            }
            if (count < 5)
            {
                tempList = dataProvider.GetBorrowAndReturnRecordListByStatus(RentRecordStatus.Rejected).ToList();
                tempList.OrderByDescending(b => b.CreateTime);
                count = tempList.Count() > 5 - count ? 5 - count : tempList.Count();
                for (int i = 0; i < count; i++)
                {
                    UserRequestModel userRequestModel = GetUserRequestModel(tempList[i]);
                    userRequestModel.IsRejected = true;
                    returnList.Add(userRequestModel);
                }
            }
            return returnList;
        }

        internal static UserRequestModel GetUserRequestModel(BorrowAndReturnRecordInfo borrowAndReturnRecordInfo)
        {
            UserRequestModel userRequestModel = new UserRequestModel();
            userRequestModel.ID = borrowAndReturnRecordInfo.ID;
            userRequestModel.Title = borrowAndReturnRecordInfo.BookDetailInfo.BookInfo.BookName;
            string displayName = string.Empty;
            string authorNameValue = string.Empty;
            userRequestModel.Author = BookModel.GetAuthorName(borrowAndReturnRecordInfo.BookDetailInfo.BookInfo, out displayName, out authorNameValue);
            userRequestModel.Publish = borrowAndReturnRecordInfo.BookDetailInfo.BookInfo.PublisherInfo.PublisherName;
            userRequestModel.UserName = borrowAndReturnRecordInfo.UserInfo.DisplayName;
            userRequestModel.Email = borrowAndReturnRecordInfo.UserInfo.Email;
            userRequestModel.Floor = borrowAndReturnRecordInfo.UserInfo.Floor.ToString();
            return userRequestModel;
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
            for (int i = 0; i < 5 && i < tempList.Count(); i++)
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
            count = tempList.Count();

            for (int i = 1; i < 6 && (count - i) > 0; i++)
            {
                ReadHistoryModel model = new ReadHistoryModel();
                model.ID = tempList[count - i].ID;
                model.Title = tempList[count - i].BookDetailInfo.BookInfo.BookName;
                model.UserName = tempList[count - i].UserInfo.DisplayName;
                model.RentDateFrom = tempList[count - i].Borrow_Date.ToString(Unity.UntityContent.IOSDateTemplate);
                model.RentDateTo = tempList[count - i].Return_Date.ToString(Unity.UntityContent.IOSDateTemplate);
                returnList.Add(model);
            }
            return returnList;
        }
    }

}
