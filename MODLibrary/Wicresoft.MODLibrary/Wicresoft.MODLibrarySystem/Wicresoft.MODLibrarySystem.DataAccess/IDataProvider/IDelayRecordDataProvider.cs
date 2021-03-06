﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wicresoft.MODLibrarySystem.Entity;

namespace Wicresoft.MODLibrarySystem.DataAccess.IDataProvider
{
    public interface IDelayRecordDataProvider : IBaseDataProvider<DelayRecord>
    {
        DelayRecord GetDelayRecordByID(long id);
        IEnumerable<DelayRecord> GetDelayRecordList();

        IEnumerable<DelayRecord> GetDelayRecordList(long id);
        IEnumerable<DelayRecord> GetDelayRecordList(UserInfo userInfo);
    }
}
