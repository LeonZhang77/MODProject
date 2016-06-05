using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Wicresoft.MODLibrarySystem.Entity;
using Wicresoft.MODLibrarySystem.Unity;
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

        public bool RenewLevel
        {
            get;
            set;
        }

        public static RentManageIndexModel GetViewModel(UserInfo userInfo)
        {
            RentManageIndexModel model = new RentManageIndexModel();
            // renewLevel: true, everybook can be renew once.
            // renewLevel: false, totally can be renew once.
            model.RenewLevel = Unity.Helper.DataUnity.GetCanRenewLevel(userInfo); 
            model.MyRequestList = GetMyRequestList(userInfo);
            model.BookInHandModelList = GetBookInHandList(userInfo, model.RenewLevel);
            model.ReadHistoryModelList = GetReadHistoryList(userInfo);
            return model;
        }

        public static List<MyRequestModel> GetMyRequestList(UserInfo userInfo)
        {
            IBorrowAndReturnRecordInfoDataProvider dataProvider = new BorrowAndReturnRecordInfoDataProvider();
            List<MyRequestModel> returnList = new List<MyRequestModel>();
            List<BorrowAndReturnRecordInfo> borrowAndReturnRecordInfoList = dataProvider.GetBorrowAndReturnRecordListByStatusAndUser(RentRecordStatus.Pending, userInfo).ToList();
            List<BorrowAndReturnRecordInfo> approvedList = dataProvider.GetBorrowAndReturnRecordListByStatusAndUser(RentRecordStatus.Approved, userInfo).ToList();
            
            if (approvedList.Count() > 0)
            {
                foreach (BorrowAndReturnRecordInfo item in approvedList)
                {
                    borrowAndReturnRecordInfoList.Add(item);
                }
            }

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

            returnList.OrderByDescending(c => c.CreateTime);

            if (returnList.Count() < 5)
            {
                borrowAndReturnRecordInfoList = dataProvider.GetBorrowAndReturnRecordListByStatusAndUser(RentRecordStatus.Rejected, userInfo).ToList();
                List<BorrowAndReturnRecordInfo> revokeList = dataProvider.GetBorrowAndReturnRecordListByStatusAndUser(RentRecordStatus.Revoked, userInfo).ToList();
                borrowAndReturnRecordInfoList.AddRange(revokeList);
                borrowAndReturnRecordInfoList.OrderByDescending(c => c.CreateTime);

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
                        if(borrowAndReturnRecordInfoList[i].Status == RentRecordStatus.Revoked)
                        {
                            model.Status = "Not Success";
                        }
                        else
                        {
                            model.Status = EnumHelper.GetEnumDescription(borrowAndReturnRecordInfoList[i].Status);
                        }
                        

                        IProcessRecordDataProvider iProcessRecordDataProvider = new ProcessRecordDataProvider();
                        ProcessRecord processRecord = iProcessRecordDataProvider.GetProcessRecordList().OrderByDescending(c=>c.CreateTime).FirstOrDefault(c => c.BorrowAndReturnRecordInfo.ID == borrowAndReturnRecordInfoList[i].ID);
                        model.Comment = processRecord.Comments;

                        returnList.Add(model);
                    }
                }
            }
            return returnList;            
        }

        public static List<BookInHandModel> GetBookInHandList(UserInfo userInfo, bool renewLevel)
        {
            IBorrowAndReturnRecordInfoDataProvider dataProvider = new BorrowAndReturnRecordInfoDataProvider();
            List<BookInHandModel> returnList = new List<BookInHandModel>();
            List<BorrowAndReturnRecordInfo> borrowAndReturnRecordInfoList = dataProvider.GetBorrowAndReturnRecordListByStatusAndUser(RentRecordStatus.Taked, userInfo).ToList();

            IDelayRecordDataProvider dataProvide = new DelayRecordDataProvider();
           
            bool showRenewButton = false;
            if (!renewLevel)
            {
                IEnumerable<DelayRecord> delayRecordList = dataProvide.GetDelayRecordList(userInfo).ToList();
                if (delayRecordList.Count() == 0)
                {
                    showRenewButton = true;
                }
            }

            foreach (BorrowAndReturnRecordInfo item in borrowAndReturnRecordInfoList)
            {
                BookInHandModel model = new BookInHandModel();
                model.ID = item.ID;
                model.Title = item.BookDetailInfo.BookInfo.BookName;
                model.StartBorrowDay = item.Borrow_Date.ToString(Unity.UntityContent.IOSDateTemplate);
                model.EXpirationDay = item.Forcast_Date.ToString(Unity.UntityContent.IOSDateTemplate);
                IEnumerable<DelayRecord> delayListByBorrowID = null;
                if (renewLevel)
                {
                    delayListByBorrowID = dataProvide.GetDelayRecordList(item.ID).ToList();
                    model.HasAlreadyRenewed = delayListByBorrowID.Count() == 0 ? false : true;
                    if (!model.HasAlreadyRenewed)
                    {
                        model.EnableRenew = item.Forcast_Date >= DateTime.Today ? true : false;
                    }
                }
                else
                {
                    if (showRenewButton)
                    {
                        model.EnableRenew = item.Forcast_Date >= DateTime.Today ? true : false;
                        model.HasAlreadyRenewed = false;
                    }
                    else
                    {
                        model.EnableRenew = false;
                        delayListByBorrowID = dataProvide.GetDelayRecordList(item.ID).ToList();
                        model.HasAlreadyRenewed = delayListByBorrowID.Count() == 0 ? false : true;
                    }
                }
                
                returnList.Add(model);
            }

            returnList.OrderByDescending(c => c.StartBorrowDay);
                        
            return returnList;
        }

        public static List<ReadHistoryModel> GetReadHistoryList(UserInfo userInfo)
        {
            IBorrowAndReturnRecordInfoDataProvider dataProvider = new BorrowAndReturnRecordInfoDataProvider();
            List<ReadHistoryModel> returnList = new List<ReadHistoryModel>();
            List<BorrowAndReturnRecordInfo> borrowAndReturnRecordInfoList = dataProvider.GetBorrowAndReturnRecordListByStatusAndUser(RentRecordStatus.Returned, userInfo).ToList();
            borrowAndReturnRecordInfoList.OrderByDescending(c=>c.Return_Date);

            int includCount = borrowAndReturnRecordInfoList.Count() < 10 ? borrowAndReturnRecordInfoList.Count() : 10;
            for (int i = 0; i<includCount; i++)
            {
                ReadHistoryModel model = new ReadHistoryModel();
                model.ID = borrowAndReturnRecordInfoList[i].ID;
                model.Title = borrowAndReturnRecordInfoList[i].BookDetailInfo.BookInfo.BookName;
                returnList.Add(model);
            }
            return returnList;
        }
    }
}
