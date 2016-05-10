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
        public string CategoryName;
        public int count;
    }

    public class returnListFordoughnut
    {
        public int value;
        public string title;
        public string color;
    }

    public class ChartModel : BaseIndexListModel<BookCountByCategory>
    {
        public List<BookCountByCategory> forDoughnutList
        {
            get;
            set;
        }
    }    
}