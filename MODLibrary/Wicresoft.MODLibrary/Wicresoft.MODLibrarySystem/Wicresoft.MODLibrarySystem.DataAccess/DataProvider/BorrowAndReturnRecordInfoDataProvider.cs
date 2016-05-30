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

        public IEnumerable<BorrowAndReturnRecordInfo> GetBorrowAndReturnRecordListByStatus(RentRecordStatus status)
        {
            IEnumerable<BorrowAndReturnRecordInfo> returnList = GetBorrowAndReturnRecordList(); ;
            returnList = returnList.Where(b => b.Status == RentRecordStatus.Pending);
            returnList.OrderBy(b => b.CreateTime);
            return returnList;
        }

        public IEnumerable<BorrowAndReturnRecordInfo> GetBorrowAndReturnRecordListByStatusAndUser(RentRecordStatus status, UserInfo userInfo)
        {
            IEnumerable<BorrowAndReturnRecordInfo> returnList = GetBorrowAndReturnRecordListByStatus(status) ;
            returnList = returnList.Where(b => b.UserInfo.ID == userInfo.ID);
            return returnList;
        }

        public int GetBooksInHandCount(UserInfo userInfo)
        {
            int returnInt = 0;
            IEnumerable<BorrowAndReturnRecordInfo> borrowAndReturnList = this.DataSource.BorrowAndReturnRecordInfos;
            borrowAndReturnList = borrowAndReturnList.Where(b => b.UserInfo.ID == userInfo.ID);
            if (borrowAndReturnList.Count() > 0)
            {
                borrowAndReturnList = borrowAndReturnList.Where(b => b.Status == RentRecordStatus.Approved
                    || b.Status == RentRecordStatus.Pending || b.Status == RentRecordStatus.Taked);
            }
            returnInt = borrowAndReturnList.Count();
            return returnInt;
        }

        public void Add(BorrowAndReturnRecordInfo entity)
        {
            if (entity.UserInfo != null)
            { 
                entity.UserInfo =  this.DataSource.UserInfos.FirstOrDefault(u => u.ID == entity.UserInfo.ID);
            }
            if(entity.BookDetailInfo != null)
            {
                entity.BookDetailInfo = this.DataSource.BookDetailInfos.FirstOrDefault(u=>u.ID == entity.BookDetailInfo.ID);
            }

            this.DataSource.BorrowAndReturnRecordInfos.Add(entity);
            this.DataSource.SaveChanges();
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
