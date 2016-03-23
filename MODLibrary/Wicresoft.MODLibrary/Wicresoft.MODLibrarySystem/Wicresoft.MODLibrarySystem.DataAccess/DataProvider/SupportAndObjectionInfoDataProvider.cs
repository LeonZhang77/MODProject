using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wicresoft.MODLibrarySystem.DataAccess.IDataProvider;
using Wicresoft.MODLibrarySystem.Entity;

namespace Wicresoft.MODLibrarySystem.DataAccess.DataProvider
{
    public class SupportAndObjectionInfoDataProvider:ISupportAndObjectionInfoDataProvider
    {
        private DBSource DataSource;

        public SupportAndObjectionInfoDataProvider()
        {
            this.DataSource = new DBSource();
        }
        public int GetCount(BookInfo bookInfo, Boolean supportOrObjection = true, UserInfo userInfo = null)
        {
            IEnumerable<SupportAndObjectionInfo> supportAndObjections
                = this.DataSource.SupportAndObjectionInfos.Where(c => c.BookInfo.ID == bookInfo.ID);
            
            if (userInfo != null)
            { 
                supportAndObjections = supportAndObjections.Where(c => c.UserInfo.ID == userInfo.ID);
            }
            else
            {
                supportAndObjections = supportAndObjections.Where(c => c.SupportOrObjection == supportOrObjection); 
            }

            return supportAndObjections.Count(); 
        }

        public void DeleteByID(long id)
        {
            this.DataSource.SaveChanges();            
        }

        public void Update(SupportAndObjectionInfo supportAndObjection)
        {
            this.DataSource.SaveChanges();
        }

        public void Add(SupportAndObjectionInfo supportAndObjection)
        {
            this.DataSource.SupportAndObjectionInfos.Add(supportAndObjection);
            this.DataSource.SaveChanges();
        }

    }
}
