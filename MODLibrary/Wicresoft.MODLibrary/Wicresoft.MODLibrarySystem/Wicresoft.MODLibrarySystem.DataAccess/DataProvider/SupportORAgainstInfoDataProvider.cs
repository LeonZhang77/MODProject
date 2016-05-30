using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wicresoft.MODLibrarySystem.Entity;
using Wicresoft.MODLibrarySystem.DataAccess.IDataProvider;
using System.Data.Entity;
using Wicresoft.MODLibrarySystem.Entity.Condition.UserInfo;

namespace Wicresoft.MODLibrarySystem.DataAccess.DataProvider
{
    public class SupportORAgainstInfoDataProvider: ISupportORAgainstInfoDataProvider
    {
        private DBSource DataSource;

        public SupportORAgainstInfoDataProvider()
        {
            this.DataSource = new DBSource();
        }

        public int GetCountByUser(BookInfo bookInfo, UserInfo userInfo)
        {
            IEnumerable<SupportORAgainst> supportORAgainsts
                = this.DataSource.SupportORAgainstInfos.Where(c => c.BookInfo.ID == bookInfo.ID);

            if (userInfo != null)
            {
                supportORAgainsts = supportORAgainsts.Where(c => c.UserInfo.ID == userInfo.ID);
                return supportORAgainsts.Count();
            }
            else
            {
                return 0;
            }
        }

        public int GetCountByStatus(BookInfo bookInfo, SupportAgainstStatus supportOrAgainst)
        {
            IEnumerable<SupportORAgainst> supportORAgainsts
                = this.DataSource.SupportORAgainstInfos.Where(c => c.BookInfo.ID == bookInfo.ID);

            supportORAgainsts = supportORAgainsts.Where(c => c.Status == supportOrAgainst);

            return supportORAgainsts.Count();

        }

        public void DeleteByID(long id)
        {
            this.DataSource.SaveChanges();
        }
        public void Add(SupportORAgainst entity)
        {
            if (entity.BookInfo != null)
            {
                entity.BookInfo = this.DataSource.BookInfos.FirstOrDefault(b => b.ID == entity.BookInfo.ID);
            }

            if (entity.UserInfo != null)
            {
                entity.UserInfo = this.DataSource.UserInfos.FirstOrDefault(u => u.ID == entity.UserInfo.ID);
            }

            this.DataSource.SupportORAgainstInfos.Add(entity);
            this.DataSource.SaveChanges();
        }

        public void Update(SupportORAgainst supportORAgainst)
        {
            this.DataSource.SaveChanges();
        }


    }
}
