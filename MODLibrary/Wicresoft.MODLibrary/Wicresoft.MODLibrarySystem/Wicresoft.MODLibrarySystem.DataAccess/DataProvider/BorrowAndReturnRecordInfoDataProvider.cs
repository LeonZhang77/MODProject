using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wicresoft.MODLibrarySystem.DataAccess.IDataProvider;
using Wicresoft.MODLibrarySystem.Entity;

namespace Wicresoft.MODLibrarySystem.DataAccess.DataProvider
{
    public class BorrowAndReturnRecordInfoDataProvider : IBorrowAndReturnRecordInfoDataProvider
    {
        private DBSource DataSource;

        public BorrowAndReturnRecordInfoDataProvider()
        {
            this.DataSource = new DBSource();
        }
        public IEnumerable<BorrowAndReturnRecordInfo> GetBorrowAndReturnRecordList()
        {
            return this.DataSource.BorrowAndReturnRecordInfos;
        }

        public void Add(BorrowAndReturnRecordInfo entity)
        {
            throw new NotImplementedException();
        }

        public void Update(BorrowAndReturnRecordInfo entity)
        {
            throw new NotImplementedException();
        }

        public void DeleteByID(long id)
        {
            throw new NotImplementedException();
        }
    }
}
