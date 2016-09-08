using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Wicresoft.MODLibrarySystem.Unity.Helper;
namespace Wicresoft.MODLibrarySystem.WebUI.Models
{
    public class Format2IndexListModel<TEntity>
    {
        public PagingContentFormat2<TEntity> PagingContent
        {
            get;
            set;
        }
    }
}