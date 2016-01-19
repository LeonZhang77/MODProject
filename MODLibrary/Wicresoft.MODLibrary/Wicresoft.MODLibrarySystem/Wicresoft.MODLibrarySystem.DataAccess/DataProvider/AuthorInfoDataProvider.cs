using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wicresoft.MODLibrarySystem.DataAccess.IDataProvider;
using Wicresoft.MODLibrarySystem.Entity;
using Wicresoft.MODLibrarySystem.Entity.Condition.AuthorInfo;

namespace Wicresoft.MODLibrarySystem.DataAccess.DataProvider
{
    public class AuthorInfoDataProvider : IAuthorInfoDataProvider
    {
        private DBSource DataSource;

        public AuthorInfoDataProvider()
        {
            this.DataSource = new DBSource();
        }

        public IEnumerable<AuthorInfo> GetAuthorList()
        {
            return this.DataSource.AuthorInfos;
        }

        public IEnumerable<AuthorInfo> GetAuthorList(AuthorInfoCondition authorCondition)
        {
            var authorList = from author in this.DataSource.AuthorInfos
                           select author;

            if (!String.IsNullOrEmpty(authorCondition.AuthorName))
            {
                authorList = authorList.Where(u => u.AuthorName.Contains(authorCondition.AuthorName));
            }

            return authorList.ToList();
        }

        public AuthorInfo GetAuthorListByID(long ID)
        {
            AuthorInfo author = this.DataSource.AuthorInfos.FirstOrDefault(u => u.ID == ID);
            return author;
        }

        public void Add(AuthorInfo authorInfo)
        {
            this.DataSource.AuthorInfos.Add(authorInfo);
            this.DataSource.SaveChanges();
        }

        public void Update(AuthorInfo authorInfo)
        {
            AuthorInfo author = this.GetAuthorListByID(authorInfo.ID);
            author.AuthorName = authorInfo.AuthorName;
            author.AuthorIntroduction = authorInfo.AuthorIntroduction;
            
            this.DataSource.SaveChanges();
        }

        public void DeleteByID(long id)
        {
            AuthorInfo author = GetAuthorListByID(id);

            if (author != null)
            {
                this.DataSource.AuthorInfos.Remove(author);
                this.DataSource.SaveChanges();
            }
        }
    }
}
