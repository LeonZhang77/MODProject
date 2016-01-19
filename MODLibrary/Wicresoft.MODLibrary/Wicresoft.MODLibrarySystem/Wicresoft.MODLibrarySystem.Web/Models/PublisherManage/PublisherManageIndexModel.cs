using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Wicresoft.MODLibrarySystem.Entity;

namespace Wicresoft.MODLibrarySystem.Web.Models.PublisherManage
{
    public class PublisherManageIndexModel : BaseIndexListModel<PublisherInfo>
    {
        public PublisherManageIndexModel()
        {
            this.PublisherModelList = new List<PublisherModel>();

        }

        public String FilterName
        {
            get;
            set;

        }

        public List<PublisherModel> PublisherModelList { get; private set; }
    }
}