using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Wicresoft.MODLibrarySystem.Web.Models.Chart
{
    public class BookCountByCategory
    {
        public long ParenetID;
        public long CategoryID;
        public int count;
    }

    public class returnListFordoughnut
    {
        public int value;
        public string color;
    }
    
    public class ChartModel : BaseIndexListModel<returnListFordoughnut>
    {
        public List<returnListFordoughnut> forDoughnutList
        {
            get;
            set;
        }
    }    
}