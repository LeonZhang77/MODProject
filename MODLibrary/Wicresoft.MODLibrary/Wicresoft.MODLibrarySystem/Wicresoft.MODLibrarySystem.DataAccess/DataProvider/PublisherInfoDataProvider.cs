using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wicresoft.MODLibrarySystem.DataAccess.IDataProvider;
using Wicresoft.MODLibrarySystem.Entity;

namespace Wicresoft.MODLibrarySystem.DataAccess.DataProvider
{
    public class PublisherInfoDataProvider : IPublisherInfoDataProvider
    {
        private DBSource DataSource;

        public PublisherInfoDataProvider()
        {
            this.DataSource = new DBSource();
        }

        public IEnumerable<PublisherInfo> GetPublisherList()
        {
            return this.DataSource.PublisherInfos;
        }

        public void Add(PublisherInfo entity)
        {
            throw new NotImplementedException();
        }

        public void Update(PublisherInfo entity)
        {
            throw new NotImplementedException();
        }

        public void DeleteByID(long id)
        {
            throw new NotImplementedException();
        }
    }
}
