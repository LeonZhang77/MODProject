using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wicresoft.MODLibrarySystem.Entity
{
    public enum UserGrade
    {
        [Description("PrepVolunteer")]
        PrepVolunteer = 1,
        [Description("Resign")]
        Resign = 2,
        [Description("Junior")]
        Junior = 3,
        [Description("Ordinary")]
        Ordinary = 4,
        [Description("VIP")]
        VIP = 5,
        [Description("Admin")]
        Admin = 6,
        [Description("Senior")]
        Senior = 7
    }

    public enum RentRecordStatus
    {
        [Description("Pending")]
        Pending = 0,
        [Description("Approved")]
        Approved = 1,
        [Description("Rejected")]
        Rejected = 2,
        [Description("Taked")]
        Taked = 3,
        [Description("Not Success")]
        Revoked = 4,
        [Description("Returned")]
        Returned = 5
    }

    public enum BookStatus
    {
        [Description("InStore Normal")]
        InStore = 1,
        [Description("Rent")]
        Rent = 2,
        [Description("Error")]
        Error = 3,
        [Description("OutStore")]
        OutStore = 4
    }

    public enum SupportAgainstStatus
    {
        [Description("Support")]
        Support = 0,
        [Description("Against")]
        Against = 1
    }
}
