using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Wicresoft.MODLibrarySystem.DataAccess.DataProvider;
using Wicresoft.MODLibrarySystem.DataAccess.IDataProvider;
using Wicresoft.MODLibrarySystem.Entity;
using Wicresoft.MODLibrarySystem.Unity;
using Wicresoft.MODLibrarySystem.Unity.Helper;

namespace Wicresoft.MODLibrarySystem.Web.Models.BookManage
{

    public class AddDetailBookModel : BaseViewModel
    {
        public AddDetailBookModel()
        {
            this.BookStatusList = new List<SelectListItem>();
        }

        public long BookID
        {
            get;
            set;
        }
        public string BookName
        {
            get;
            set;
        }
        public String ISBN
        {
            get;
            set;
        }
        public String Owner
        {
            get;
            set;
        }

        public String OwnerValue
        {
            get;
            set;
        }

        public String Status
        {
            get;
            set;
        }

        public String Storage_Time
        {
            get;
            set;
        }
        public String Create_Time
        {
            get;
            set;
        }

        public List<SelectListItem> BookStatusList
        {
            get;
            set;
        }

        public int BookStatusSelected
        {
            get;
            set;
        }

        public bool IsUse
        {
            get;
            set;
        }

        public BookDetailInfo GetEntity()
        {
            BookDetailInfo bookDetailInfo = new BookDetailInfo();

            IUserInfoDataProvider userDataProvider = new UserInfoDataProvider();
            bookDetailInfo.UserInfo = userDataProvider.GetUserListByID(Convert.ToInt32(this.OwnerValue));
            IBookInfoDataProvider bookDataProvider = new BookInfoDataProvider();
            bookDetailInfo.BookInfo = bookDataProvider.GetBookInfoByID(this.BookID);
            bookDetailInfo.Status = (BookStatus)this.BookStatusSelected;
            bookDetailInfo.Storage_Time = Convert.ToDateTime(this.Storage_Time);
            bookDetailInfo.CreateTime = DateTime.Now;

            return bookDetailInfo;
        }

        public static AddDetailBookModel GetViewModel(BookDetailInfo bookDetailInfo)
        {
            AddDetailBookModel model = new AddDetailBookModel();
            model.ID = bookDetailInfo.ID;
            model.BookName = bookDetailInfo.BookInfo.BookName;
            model.ISBN = bookDetailInfo.BookInfo.ISBN;
            model.Owner = bookDetailInfo.UserInfo.DisplayName;
            model.OwnerValue = bookDetailInfo.UserInfo.ID.ToString();
            model.Status = EnumHelper.GetEnumDescription(bookDetailInfo.Status);
            model.Storage_Time = bookDetailInfo.Storage_Time.ToString(UntityContent.IOSDateTemplate);
            model.Create_Time = bookDetailInfo.CreateTime.ToString();

            //need to add logic
            model.IsUse = false;
            return model;
        }


    }
}