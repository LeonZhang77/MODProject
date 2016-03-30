using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Wicresoft.MODLibrarySystem.DataAccess.DataProvider;
using Wicresoft.MODLibrarySystem.DataAccess.IDataProvider;
using Wicresoft.MODLibrarySystem.Entity;
using Wicresoft.MODLibrarySystem.Unity;
using Wicresoft.MODLibrarySystem.Unity.Helper;

namespace Wicresoft.MODLibrarySystem.WebUI.Models.BookManage
{

    public class DetailBooksModel : BaseViewModel
    {
        public DetailBooksModel()
        {  
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

        public static DetailBooksModel GetViewModel(BookDetailInfo bookDetailInfo)
        {
            DetailBooksModel model = new DetailBooksModel();
            model.ID = bookDetailInfo.ID;
            model.BookID = bookDetailInfo.BookInfo.ID;
            model.BookName = bookDetailInfo.BookInfo.BookName;
            model.ISBN = bookDetailInfo.BookInfo.ISBN;
            model.Owner = bookDetailInfo.UserInfo.DisplayName;
            model.OwnerValue = bookDetailInfo.UserInfo.ID.ToString();
            model.Storage_Time = bookDetailInfo.Storage_Time.ToString(UntityContent.IOSDateTemplate);
            model.Create_Time = bookDetailInfo.CreateTime.ToString();
            model.Status = bookDetailInfo.Status.ToString();
                        
            return model;
        }


    }
}