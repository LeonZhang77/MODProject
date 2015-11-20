using System;
using System.Web.Mvc;
using Wicresoft.MODLibrarySystem.Entity;

namespace Wicresoft.MODLibrarySystem.Web.Models.Author
{
    public class AuthorModel : BaseViewModel
    {
        public String AuthorName
        {
            get;
            set;
        }

        public String  AuthorIntroduction
        {
            get;
            set;
        }

        public AuthorInfo GetEntity()
        {
            AuthorInfo author = new AuthorInfo();
            author.ID = this.ID;
            author.AuthorName = this.AuthorName;
            author.AuthorIntroduction = this.AuthorIntroduction;

            return author;
        }

        public static AuthorModel GetViewModel(AuthorInfo author)
        {
            AuthorModel model = new AuthorModel();
            model.ID = author.ID;
            model.AuthorName = author.AuthorName;
            model.AuthorIntroduction = author.AuthorIntroduction;

            return model;
        }
    }
}