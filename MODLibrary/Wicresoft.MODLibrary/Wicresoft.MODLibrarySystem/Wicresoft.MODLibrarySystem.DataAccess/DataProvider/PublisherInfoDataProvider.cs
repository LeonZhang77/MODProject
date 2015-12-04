using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wicresoft.MODLibrarySystem.Entity;
using Wicresoft.MODLibrarySystem.DataAccess.IDataProvider;
using System.Data.Entity;
using Wicresoft.MODLibrarySystem.Entity.Condition.PublisherInfo;

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

        public IEnumerable<PublisherInfo> GetPublisherList(PublisherInfoCondition publisherCondition)
        {
            var publisherList = from publisher in this.DataSource.PublisherInfos
                                select publisher;
            if (!String.IsNullOrEmpty(publisherCondition.PublisherName))
            {
                publisherList = publisherList.Where(p => p.PublisherName.Contains(publisherCondition.PublisherName));
            }
            return publisherList.ToList();
        }

        public PublisherInfo GetPublisherByID(long ID)
        {
            PublisherInfo publisher = this.DataSource.PublisherInfos.FirstOrDefault(p => p.ID == ID);
            return publisher;
        }

        public void Add(PublisherInfo publisherInfo)
        {
            this.DataSource.PublisherInfos.Add(publisherInfo);
            this.DataSource.SaveChanges();
        }

        public void Update(PublisherInfo publisherInfo)
        {
            PublisherInfo publisher = this.GetPublisherByID(publisherInfo.ID);
            publisher.PublisherName = publisherInfo.PublisherName;
            publisher.PublisherIntroduction = publisherInfo.PublisherIntroduction;

            this.DataSource.SaveChanges();
        }

        public void DeleteByID(long id)
        {
            PublisherInfo publisher = GetPublisherByID(id);

            if (publisher != null)
            {
                this.DataSource.PublisherInfos.Remove(publisher);
                this.DataSource.SaveChanges();
            }
        }

    }
}
