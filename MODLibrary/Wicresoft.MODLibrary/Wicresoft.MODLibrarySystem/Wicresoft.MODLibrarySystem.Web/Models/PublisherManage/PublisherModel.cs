using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Wicresoft.MODLibrarySystem.Entity;

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

            return model;

        }

    }
}