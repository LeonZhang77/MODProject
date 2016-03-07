using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Wicresoft.MODLibrarySystem.Resoure.Models.PicManager
{
    public class PicListModel
    {
        public PicListModel()
        {
            this.PicModelList = new List<PicModel>();
        }
        public List<PicModel> PicModelList
        {
            get;
            set;
        }
    }
}