using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Wicresoft.BadmintonSystem.Entity;

namespace Badminton.Models.ScoreManage.InitialScore
{
    public class InitialScoreMember:BaseViewModel
    {
        public String Name
        {
            get;
            set;
        }

        public String Male
        {
            get;
            set;
        }

        public String joinDate
        {
            get;
            set;
        }

        public InitialScoreMember GetViewModel(MemberInfo info)
        {
            InitialScoreMember Member = new InitialScoreMember();
            Member.ID = info.ID;
            Member.Name = info.Name;
            if (info.Male)
            {
                Member.Male = "男";
            }
            else 
            {
                Member.Male = "女";
            }
            Member.joinDate = info.CreateTime.ToString("MM-dd-yyyy");
            return Member;
            
        }

    }
}