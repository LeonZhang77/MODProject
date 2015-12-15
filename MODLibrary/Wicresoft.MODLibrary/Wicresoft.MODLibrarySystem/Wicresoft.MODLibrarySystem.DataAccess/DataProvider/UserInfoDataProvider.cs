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
    public class UserInfoDataProvider : IUserInfoDataProvider
    {
        private DBSource DataSource;

        public UserInfoDataProvider()
        {
            this.DataSource = new DBSource();
        }

        public IEnumerable<UserInfo> GetUserList(bool isDefult)
        {
            IEnumerable<UserInfo> userList;
            if (isDefult)
            {
                userList = this.DataSource.UserInfos.Where(u => u.IsShow == true);
            }
            else
            {
                userList = this.DataSource.UserInfos;
            }
            return userList;
        }

        public IEnumerable<UserInfo> GetUserList(UserInfoCondition userCondition)
        {
            var userList = from user in this.DataSource.UserInfos
                           where user.IsShow == true
                           select user;

            if (!String.IsNullOrEmpty(userCondition.DisplayName))
            {
                userList = userList.Where(u => u.DisplayName.Contains(userCondition.DisplayName));
            }


            return userList.ToList();
        }

        public UserInfo GetUserListByID(long ID)
        {
            UserInfo user = this.DataSource.UserInfos.FirstOrDefault(u => u.ID == ID);
            return user;
        }

        public UserInfo GetUserListByEmail(String email)
        {
            UserInfo user = this.DataSource.UserInfos.FirstOrDefault(u => u.Email == email);
            return user;
        }

        public UserInfo FindUserInfoByUserNameAndPassword(String userName, String password)
        {
            UserInfo user = this.DataSource.UserInfos.FirstOrDefault(u => u.LoginName == userName && u.Password == password);
            return user;
        }


        public void Add(UserInfo userInfo)
        {
            this.DataSource.UserInfos.Add(userInfo);
            this.DataSource.SaveChanges();
        }

        public void Update(UserInfo userInfo)
        {
            UserInfo user = this.GetUserListByID(userInfo.ID);
            user.DisplayName = userInfo.DisplayName;
            user.RealName = userInfo.RealName;
            user.LoginName = userInfo.LoginName;
            user.Password = userInfo.Password;
            user.Email = userInfo.Email;
            user.Floor = userInfo.Floor;
            user.PM = userInfo.PM;
            user.Team = userInfo.Team;
            user.Chinese_Name = userInfo.Chinese_Name;
            user.Wechat = userInfo.Wechat;
            user.Grade = userInfo.Grade;
            user.Late_point = userInfo.Late_point;
            user.Remark = userInfo.Remark;

            this.DataSource.SaveChanges();
        }

        public void DeleteByID(long id)
        {
            UserInfo user = GetUserListByID(id);

            if (user != null)
            {
                this.DataSource.UserInfos.Remove(user);
                this.DataSource.SaveChanges();
            }
        }
    }
}
