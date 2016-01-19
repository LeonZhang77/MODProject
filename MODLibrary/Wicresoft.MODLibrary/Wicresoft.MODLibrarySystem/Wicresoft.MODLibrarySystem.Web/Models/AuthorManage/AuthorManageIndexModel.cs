using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Wicresoft.MODLibrarySystem.Entity;

namespace Wicresoft.MODLibrarySystem.Web.Models.Author
{
    public class AuthorManageIndexModel : BaseIndexListModel<AuthorInfo>
    {
        public AuthorManageIndexModel()
        {
            this.AuthorModelList = new List<AuthorModel>();
        }

        public string FilterName
        {
            get;
            set;
        }

        public List<AuthorModel> AuthorModelList
        {
            get;
            private set;
        }
    }
}