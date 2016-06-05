

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wicresoft.MODLibrarySystem.DataAccess.IDataProvider;
using Wicresoft.MODLibrarySystem.Entity;

namespace Wicresoft.MODLibrarySystem.DataAccess.DataProvider
{
    public class DelayRecordDataProvider: IDelayRecordDataProvider
    {
        private DBSource DataSource;

        public DelayRecordDataProvider()
        {
            this.DataSource = new DBSource();
        }

        public IEnumerable<DelayRecord> GetDelayRecordList()
        {
            return this.DataSource.DelayRecords;
        }


        public IEnumerable<DelayRecord> GetDelayRecordList(long id)
        {
            return this.DataSource.DelayRecords.Where(c=>c.BorrowAndReturnRecordInfo.ID == id);
        }

        public IEnumerable<DelayRecord> GetDelayRecordList(UserInfo userInfo)
        {
            return this.DataSource.DelayRecords.Where(c => c.UserInfo.ID == userInfo.ID);
        }
        public DelayRecord GetDelayRecordByID(long id)
        {
            return this.DataSource.DelayRecords.FirstOrDefault(c => c.ID == id);
        }
        public void Add(DelayRecord entity)
        {
            if (entity.UserInfo != null)
            {
                entity.UserInfo = this.DataSource.UserInfos.FirstOrDefault(u => u.ID == entity.UserInfo.ID);
            }
            if (entity.BorrowAndReturnRecordInfo != null)
            {
                entity.BorrowAndReturnRecordInfo = this.DataSource.BorrowAndReturnRecordInfos.FirstOrDefault(u => u.ID == entity.BorrowAndReturnRecordInfo.ID);
            }

            this.DataSource.DelayRecords.Add(entity);
            this.DataSource.SaveChanges();
        }

        public void Update(DelayRecord entity)
        {
            DelayRecord record = this.GetDelayRecordByID(entity.ID);

            if (record.BorrowAndReturnRecordInfo != null)
            {
                record.BorrowAndReturnRecordInfo = this.DataSource.BorrowAndReturnRecordInfos.Find(entity.BorrowAndReturnRecordInfo.ID);
            }
            else 
            {
                record.BorrowAndReturnRecordInfo = null;
            }

            if (record.UserInfo != null)
            {
                record.UserInfo = this.DataSource.UserInfos.Find(entity.UserInfo.ID);
            }
            else
            {
                record.UserInfo = null;
            }

            record.Comments = entity.Comments;

            this.DataSource.SaveChanges();
        }

        public void DeleteByID(long id)
        {
            throw new NotImplementedException();
        }
    }
}
