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
        Admin = 6
    }

    public enum RentRecordStatus
    {
        [Description("InProcess")]
        InProcess = 0,
        [Description("Finished")]
        Finished = 1
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
}
