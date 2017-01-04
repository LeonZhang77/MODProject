using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Badminton.Models.DataManage
{
    public class DataManageIndexModel
    {
        public DataManageIndexModel()
        {
            this.MemberList = new List<MemberModel>();
        }
        public List<MemberModel> MemberList
        {
            get;
            set;
        }

        public List<ClubModel> ClubList
        {
            get;
            set;
        }
    }
}