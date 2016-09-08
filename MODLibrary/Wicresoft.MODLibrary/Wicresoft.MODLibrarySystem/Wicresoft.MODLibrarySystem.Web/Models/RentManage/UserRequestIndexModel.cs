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
    public class UserRequestIndexModel : BaseIndexListModel<BorrowAndReturnRecordInfo>
    {
        public UserRequestIndexModel()
        {
            this.UserRequestList = new List<UserRequestModel>();
        }

        public List<UserRequestModel> UserRequestList
        {
            get;
            private set;
        }

        public static UserRequestModel GetViewModel(BorrowAndReturnRecordInfo info)
        {
            UserRequestModel userRequestModel = new UserRequestModel();
            userRequestModel.ID = info.ID;
            userRequestModel.Title = info.BookDetailInfo.BookInfo.BookName;
            string displayName = string.Empty;
            string authorNameValue = string.Empty;
            userRequestModel.Author = BookModel.GetAuthorName(info.BookDetailInfo.BookInfo, out displayName, out authorNameValue);
            userRequestModel.Publish = info.BookDetailInfo.BookInfo.PublisherInfo.PublisherName;
            userRequestModel.UserName = info.UserInfo.DisplayName;
            userRequestModel.Email = info.UserInfo.Email;
            userRequestModel.Floor = info.UserInfo.Floor.ToString();

            return userRequestModel;
        }

        public static UserRequestIndexModel GetUserRequestList(Int32 pageIndex = 0)
        {
            UserRequestIndexModel returnModel = new UserRequestIndexModel();
            IBorrowAndReturnRecordInfoDataProvider dataProvider = new BorrowAndReturnRecordInfoDataProvider();
            List<BorrowAndReturnRecordInfo> tempList = dataProvider.GetBorrowAndReturnRecordListByStatus(RentRecordStatus.Pending).ToList();
            tempList.OrderByDescending(b => b.CreateTime);

            PagingContent<BorrowAndReturnRecordInfo> paging = new PagingContent<BorrowAndReturnRecordInfo>(tempList, pageIndex);

            foreach (var item in paging.EntityList)
            {
                returnModel.UserRequestList.Add(UserRequestIndexModel.GetViewModel(item));
            }

            returnModel.PagingContent = paging;

            return returnModel;
        }
    }
}