using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Wicresoft.MODLibrarySystem.Unity.Helper;

namespace Wicresoft.MODLibrarySystem.WebUI.Models
{
    public class BaseIndexListModel<TEntity>
    {
        public PagingContent<TEntity> PagingContent
        {
            get;
            set;
        }
    }
}