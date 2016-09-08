using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Wicresoft.MODLibrarySystem.Entity;
using Wicresoft.MODLibrarySystem.Unity;

namespace Wicresoft.MODLibrarySystem.Wap.Models.RentManage
{
    public class ReadHistoryModel : BaseViewModel
    {
        public ReadHistoryModel()
        {
            this.ReadHistoryModelList = new List<ReadHistoryModel>();
        }
        public String Title
        {
            get;
            set;
        }
        public List<ReadHistoryModel> ReadHistoryModelList
        {
            get;
            set;
        }
    }
}
