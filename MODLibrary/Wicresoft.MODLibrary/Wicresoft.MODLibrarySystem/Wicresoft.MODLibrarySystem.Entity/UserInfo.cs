using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wicresoft.MODLibrarySystem.Entity
{
    public class UserInfo : BaseEntity
    {
        public UserInfo()
        {
            this.CreateTime = DateTime.Now;
            this.IsShow = true;
        }

        [Required]
        public virtual String DisplayName
        {
            get;
            set;
        }

        [Required]
        public virtual String RealName
        {
            get;
            set;
        }

        [Required]
        public virtual String LoginName
        {
            get;
            set;
        }

        [Required]
        public virtual String Password
        {
            get;
            set;
        }
        public virtual int Floor
        {
            get;
            set;
        }
        public virtual String PM
        {
            get;
            set;
        }

        public virtual String Team
        {
            get;
            set;
        }

        public virtual String Chinese_Name
        {
            get;
            set;
        }
        public virtual bool IsShow
        {
            get;
            set;
        }

        public virtual String Email
        {
            get;
            set;
        }

        public virtual String Wechat
        {
            get;
            set;
        }

        [Required]
        public virtual UserGrade Grade
        {
            get;
            set;
        }

        [Required]
        public virtual int Late_point
        {
            get;
            set;
        }

        public virtual String Remark
        {
            get;
            set;
        }
    }
}
