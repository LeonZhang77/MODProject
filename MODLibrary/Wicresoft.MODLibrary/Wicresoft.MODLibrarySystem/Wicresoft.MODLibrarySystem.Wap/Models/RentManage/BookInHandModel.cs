using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Wicresoft.MODLibrarySystem.Entity;
using Wicresoft.MODLibrarySystem.Unity;
using Wicresoft.MODLibrarySystem.DataAccess.DataProvider;
using Wicresoft.MODLibrarySystem.DataAccess.IDataProvider;

namespace Wicresoft.MODLibrarySystem.Wap.Models.RentManage
{
    public class BookInHandModel : BaseViewModel
    {
        public BookInHandModel()
        {

        }
        public string Title
        {
            get;
            set;
        }
        public string StartBorrowDay
        {
            get;
            set;
        }
        public String EXpirationDay
        {
            get;
            set;
        }

        public bool EnableRenew
        {
            get;
            set;
        }

        public bool HasAlreadyRenewed
        {
            get;
            set;
        }

        public DelayRecord GetEntity(UserInfo user, out BorrowAndReturnRecordInfo borrowAndReturnRecordInfo)
        {
            IBorrowAndReturnRecordInfoDataProvider iBorrowAndReturnRecordInfoDataProviderdataProvider = new BorrowAndReturnRecordInfoDataProvider();
            borrowAndReturnRecordInfo = iBorrowAndReturnRecordInfoDataProviderdataProvider.GetBorrowAndReturnRecordById(this.ID);
            borrowAndReturnRecordInfo.Forcast_Date = borrowAndReturnRecordInfo.Forcast_Date.AddDays(30);

            DelayRecord delayRecord = new DelayRecord();
            delayRecord.BorrowAndReturnRecordInfo = borrowAndReturnRecordInfo;
            delayRecord.UserInfo = user;
            return delayRecord;
        }
    }
}
