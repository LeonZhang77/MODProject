using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wicresoft.MODLibrarySystem.DataAccess.IDataProvider;
using Wicresoft.MODLibrarySystem.Entity;

namespace Wicresoft.MODLibrarySystem.DataAccess.DataProvider
{
    public class BookDetailInfoDataProvider : IBookDetailInfoDataProvider
    {
        private DBSource DataSource;

        public BookDetailInfoDataProvider()
        {
            this.DataSource = new DBSource();
        }

        public IEnumerable<BookDetailInfo> GetBookDetailList()
        {
            return this.DataSource.BookDetailInfos;
        }

        public void Add(BookDetailInfo entity)
        {
            throw new NotImplementedException();
        }

        public void Update(BookDetailInfo entity)
        {
            throw new NotImplementedException();
        }

        public void DeleteByID(long id)
        {
            throw new NotImplementedException();
        }
    }
}
