using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wicresoft.MODLibrarySystem.Entity;

namespace Wicresoft.MODLibrarySystem.DataAccess.IDataProvider
{
    public interface IBorrowAndReturnRecordInfoDataProvider : IBaseDataProvider<BorrowAndReturnRecordInfo>
    {
        IEnumerable<BorrowAndReturnRecordInfo> GetBorrowAndReturnRecordList();

        int GetBooksInHandCount(UserInfo userInfo);

        BorrowAndReturnRecordInfo GetBorrowAndReturnRecordById(long id);
        IEnumerable<BorrowAndReturnRecordInfo> GetBorrowAndReturnRecordListByStatus(RentRecordStatus status);
        IEnumerable<BorrowAndReturnRecordInfo> GetBorrowAndReturnRecordListByStatusAndUser(RentRecordStatus status, UserInfo userInfo);
    }
}
