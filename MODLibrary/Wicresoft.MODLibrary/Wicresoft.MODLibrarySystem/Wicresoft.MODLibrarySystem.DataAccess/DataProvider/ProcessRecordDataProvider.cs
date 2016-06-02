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

        public ProcessRecord GetProcessRecordByID(long id)
        {
            return this.DataSource.ProcessRecords.FirstOrDefault(c => c.ID == id);
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
            ProcessRecord processRecord = this.GetProcessRecordByID(entity.ID);

            if (processRecord.BorrowAndReturnRecordInfo != null)
            {
                processRecord.BorrowAndReturnRecordInfo = this.DataSource.BorrowAndReturnRecordInfos.Find(entity.BorrowAndReturnRecordInfo.ID);
            }
            else 
            {
                processRecord.BorrowAndReturnRecordInfo = null;
            }

            if (processRecord.UserInfo != null)
            {
                processRecord.UserInfo = this.DataSource.UserInfos.Find(entity.UserInfo.ID);
            }
            else
            {
                processRecord.UserInfo = null;
            }

            processRecord.Status = entity.Status;
            processRecord.Comments = entity.Comments;

            this.DataSource.SaveChanges();
        }

        public void DeleteByID(long id)
        {
            throw new NotImplementedException();
        }
    }
}
