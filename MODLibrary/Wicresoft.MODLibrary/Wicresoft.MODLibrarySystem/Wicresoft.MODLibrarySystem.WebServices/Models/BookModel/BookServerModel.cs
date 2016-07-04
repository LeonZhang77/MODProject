using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Wicresoft.MODLibrarySystem.Entity;

namespace Wicresoft.MODLibrarySystem.WebServices.Models.BookModel
{
    public class BookServerModel : BaseModel
    {
        public String BookName
        {
            get;
            set;
        }

        public String ISBN
        {
            get;
            set;
        }

        public static BookServerModel GetServerModel(BookInfo entity)
        {
            BookServerModel model = new BookServerModel();

            model.ID = entity.ID;
            model.BookName = entity.BookName;
            model.ISBN = entity.ISBN;
            model.CreateTime = entity.CreateTime.ToString("yyyy-MM-dd HH:mm:ss");

            return model;
        }
    }
}