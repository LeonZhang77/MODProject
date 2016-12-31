using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Wicresoft.MODLibrarySystem.DataAccess.DataProvider;
using Wicresoft.MODLibrarySystem.DataAccess.IDataProvider;
using Wicresoft.MODLibrarySystem.Entity;
using Wicresoft.MODLibrarySystem.Unity;
using Wicresoft.MODLibrarySystem.Unity.Helper;
using Wicresoft.MODLibrarySystem.WebUI.Models.BookManage;

namespace Wicresoft.MODLibrarySystem.WebUI.Models.RentManage
{
    public class MyRequestModel : BaseViewModel
    {
        public MyRequestModel()
        {
            this.MyRequestList = new List<MyRequestModel>();
        }

        public string Title
        {
            get;
            set;
        }
        public string Author
        {
            get;
            set;
        }
        public String Publish
        {
            get;
            set;
        }
        public String Status
        {
            get;
            set;
        }
        public String Comment
        {
            get;
            set;
        }

        public List<MyRequestModel> MyRequestList
        {
            get;
            set;
        }

        public static List<MyRequestModel> GetMoreRequestList(UserInfo userInfo)
        {
            IBorrowAndReturnRecordInfoDataProvider dataProvider = new BorrowAndReturnRecordInfoDataProvider();
            List<MyRequestModel> returnList = new List<MyRequestModel>();
            List<BorrowAndReturnRecordInfo> borrowAndReturnRecordInfoList = dataProvider.GetBorrowAndReturnRecordListByStatusAndUser(RentRecordStatus.Approved, userInfo).ToList();
            List<BorrowAndReturnRecordInfo> pendingList = dataProvider.GetBorrowAndReturnRecordListByStatusAndUser(RentRecordStatus.Pending, userInfo).ToList();
            borrowAndReturnRecordInfoList.AddRange(pendingList);

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

                IProcessRecordDataProvider iProcessRecordDataProvider = new ProcessRecordDataProvider();
                ProcessRecord processRecord = iProcessRecordDataProvider.GetProcessRecordList().OrderByDescending(c => c.CreateTime).FirstOrDefault(c => c.BorrowAndReturnRecordInfo.ID == item.ID);
                model.Comment = processRecord.Comments;

                returnList.Add(model);
            }
            borrowAndReturnRecordInfoList = dataProvider.GetBorrowAndReturnRecordListByStatusAndUser(RentRecordStatus.Rejected, userInfo).ToList();
            List<BorrowAndReturnRecordInfo> revokeList = dataProvider.GetBorrowAndReturnRecordListByStatusAndUser(RentRecordStatus.Revoked, userInfo).ToList();
            borrowAndReturnRecordInfoList.AddRange(revokeList);

            if (borrowAndReturnRecordInfoList.Count() > 0)
            {
                borrowAndReturnRecordInfoList = borrowAndReturnRecordInfoList.OrderByDescending(c => c.CreateTime).ToList();
                int includCount = borrowAndReturnRecordInfoList.Count() < 5 - returnList.Count() ? borrowAndReturnRecordInfoList.Count() : 5 - returnList.Count();
                for (int i = 0; i < includCount; i++)
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
                    ProcessRecord processRecord = iProcessRecordDataProvider.GetProcessRecordList().OrderByDescending(c => c.CreateTime).FirstOrDefault(c => c.BorrowAndReturnRecordInfo.ID == borrowAndReturnRecordInfoList[i].ID);
                    model.Comment = processRecord.Comments;

                    returnList.Add(model);
                }

            }
            return returnList;
        }
    }
}
