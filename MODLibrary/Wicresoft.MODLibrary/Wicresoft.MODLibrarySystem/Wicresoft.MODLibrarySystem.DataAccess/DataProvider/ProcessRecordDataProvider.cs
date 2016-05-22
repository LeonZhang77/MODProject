using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wicresoft.MODLibrarySystem.DataAccess.IDataProvider;
using Wicresoft.MODLibrarySystem.Entity;

namespace Wicresoft.MODLibrarySystem.DataAccess.DataProvider
{
    public class ProcessRecordDataProvider: IProcessRecordDataProvider
    {
        private DBSource DataSource;

        public ProcessRecordDataProvider()
        {
            this.DataSource = new DBSource();
        }

        public IEnumerable<ProcessRecord> GetProcessRecordList()
        {
            return this.DataSource.ProcessRecords;
        }
        public void Add(ProcessRecord entity)
        {
            if (entity.UserInfo != null)
            {
                entity.UserInfo = this.DataSource.UserInfos.FirstOrDefault(u => u.ID == entity.UserInfo.ID);
            }
            if (entity.BorrowAndReturnRecordInfo != null)
            {
                entity.BorrowAndReturnRecordInfo = this.DataSource.BorrowAndReturnRecordInfos.FirstOrDefault(u => u.ID == entity.BorrowAndReturnRecordInfo.ID);
            }

            this.DataSource.ProcessRecords.Add(entity);
            this.DataSource.SaveChanges();
        }

        public void Update(ProcessRecord entity)
        {
            throw new NotImplementedException();
        }

        public void DeleteByID(long id)
        {
            throw new NotImplementedException();
        }
    }
}
