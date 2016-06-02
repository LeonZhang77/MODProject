using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Wicresoft.MODLibrarySystem.Entity;
using Wicresoft.MODLibrarySystem.Unity.Helper;
using Wicresoft.MODLibrarySystem.WebUI.Models.BookManage;
using Wicresoft.MODLibrarySystem.DataAccess.DataProvider;
using Wicresoft.MODLibrarySystem.DataAccess.IDataProvider;

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

        public static RentManageIndexModel GetViewModel(UserInfo userInfo)
        {
            RentManageIndexModel model = new RentManageIndexModel();
            model.MyRequestList = GetMyRequestList(userInfo);
            return model;
        }

        public static List<MyRequestModel> GetMyRequestList(UserInfo userInfo)
        {
            IBorrowAndReturnRecordInfoDataProvider dataProvider = new BorrowAndReturnRecordInfoDataProvider();
            List<MyRequestModel> returnList = new List<MyRequestModel>();
            List<BorrowAndReturnRecordInfo> borrowAndReturnRecordInfoList = dataProvider.GetBorrowAndReturnRecordListByStatusAndUser(RentRecordStatus.Pending, userInfo).ToList();
            
            foreach (BorrowAndReturnRecordInfo item in borrowAndReturnRecordInfoList)
            { 
                MyRequestModel model = new MyRequestModel();
                model.ID = item.ID;
                model.Title = item.BookDetailInfo.BookInfo.BookName;
                string displayName = string.Empty;
                string authorNameValue = string.Empty;
                model.Author = BookModel.GetAuthorName(item.BookDetailInfo.BookInfo, out displayName, out authorNameValue);
                model.Publish = item.BookDetailInfo.BookInfo.PublisherInfo.PublisherName;
                model.Status = EnumHelper.GetEnumDescription(item.Status);
                returnList.Add(model);
            }
            returnList.OrderByDescending(c => c.CreateTime);

            if (returnList.Count() < 5)
            {
                borrowAndReturnRecordInfoList = dataProvider.GetBorrowAndReturnRecordListByStatusAndUser(RentRecordStatus.Rejected, userInfo).ToList();
                if (borrowAndReturnRecordInfoList.Count() > 0)
                {
                    borrowAndReturnRecordInfoList.OrderByDescending(c => c.CreateTime);
                    int includCount = borrowAndReturnRecordInfoList.Count() < 5 - returnList.Count() ? borrowAndReturnRecordInfoList.Count() : 5 - returnList.Count();
                    for (int i = 0; i < includCount;  i++)
                    {
                        MyRequestModel model = new MyRequestModel();
                        model.ID = borrowAndReturnRecordInfoList[i].ID;
                        model.Title = borrowAndReturnRecordInfoList[i].BookDetailInfo.BookInfo.BookName;
                        string displayName = string.Empty;
                        string authorNameValue = string.Empty;
                        model.Author = BookModel.GetAuthorName(borrowAndReturnRecordInfoList[i].BookDetailInfo.BookInfo, out displayName, out authorNameValue);
                        model.Publish = borrowAndReturnRecordInfoList[i].BookDetailInfo.BookInfo.PublisherInfo.PublisherName;
                        model.Status = EnumHelper.GetEnumDescription(borrowAndReturnRecordInfoList[i].Status);

                        IProcessRecordDataProvider iProcessRecordDataProvider = new ProcessRecordDataProvider();
                        ProcessRecord processRecord = iProcessRecordDataProvider.GetProcessRecordList().OrderByDescending(c=>c.CreateTime).FirstOrDefault(c => c.BorrowAndReturnRecordInfo.ID == borrowAndReturnRecordInfoList[i].ID);
                        model.Comment = processRecord.Comments;

                        returnList.Add(model);
                    }
                }
            }
            return returnList;            
        }
    }
}
