using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wicresoft.MODLibrarySystem.Entity;
using Wicresoft.MODLibrarySystem.Entity.Condition.PublisherInfo;


namespace Wicresoft.MODLibrarySystem.DataAccess.IDataProvider
{
    public interface IPublisherInfoDataProvider : IBaseDataProvider<PublisherInfo>
    {
        IEnumerable<PublisherInfo> GetPublisherList();

        IEnumerable<PublisherInfo> GetPublisherList(PublisherInfoCondition publisherCondition);

        PublisherInfo GetPublisherByID(long ID);

    }
}
