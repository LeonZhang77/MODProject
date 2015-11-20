using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wicresoft.MODLibrarySystem.DataAccess.IDataProvider
{
    public interface IBaseDataProvider<T>
    {        
        void Add(T entity);

        void Update(T entity);

        void DeleteByID(long id);
    }
}
