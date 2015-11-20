using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wicresoft.MODLibrarySystem.Entity;
using Wicresoft.MODLibrarySystem.Entity.Condition.UserInfo;

namespace Wicresoft.MODLibrarySystem.DataAccess.IDataProvider
{
    public interface IUserInfoDataProvider : IBaseDataProvider<UserInfo>
    {
        IEnumerable<UserInfo> GetUserList(bool isDefult);

        IEnumerable<UserInfo> GetUserList(UserInfoCondition userCondition);

        UserInfo GetUserListByID(long ID);

        UserInfo GetUserListByEmail(String email);

        UserInfo FindUserInfoByUserNameAndPassword(String userName, String password);
    }
}
