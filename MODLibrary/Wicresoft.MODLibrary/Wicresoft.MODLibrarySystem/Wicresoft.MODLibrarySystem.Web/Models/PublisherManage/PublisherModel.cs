using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Wicresoft.MODLibrarySystem.DataAccess.DataProvider;
using Wicresoft.MODLibrarySystem.DataAccess.IDataProvider;
using Wicresoft.MODLibrarySystem.Entity;
using Wicresoft.MODLibrarySystem.Entity.Condition.BookInfo;

namespace Wicresoft.MODLibrarySystem.Web.Models.PublisherManage
{
    public class PublisherModel : BaseViewModel
    {
        public virtual String PublisherName
        {
            get;
            set;
        }

        public virtual String PublisherIntroduction
        {
            get;
            set;
        }

        public bool IsUse
        {
            get;
            set;
        }
        public PublisherInfo GetEntity()
        {
            PublisherInfo publisher = new PublisherInfo();

            publisher.ID = this.ID;
            publisher.PublisherName = this.PublisherName;
            publisher.PublisherIntroduction = this.PublisherIntroduction;

            return publisher;
        }

        public static PublisherModel GetViewModel(PublisherInfo publisher)
        {
          PublisherModel model = new PublisherModel();

            model.ID = publisher.ID;
            model.PublisherName = publisher.PublisherName;
            model.PublisherIntroduction = publisher.PublisherIntroduction;

            IBookInfoDataProvider iBookInfoDataProvider = new BookInfoDataProvider();
            BookInfoCondition condition = new BookInfoCondition();
            condition.Publisher = publisher;

            if ( iBookInfoDataProvider.GetBookList(condition).Count() > 0)
            {
                model.IsUse = true;
            }

            return model;

        }

    }
}