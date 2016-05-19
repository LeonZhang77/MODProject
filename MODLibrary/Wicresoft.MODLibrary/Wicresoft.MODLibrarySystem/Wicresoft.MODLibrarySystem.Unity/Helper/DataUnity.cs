using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wicresoft.MODLibrarySystem.Entity;

namespace Wicresoft.MODLibrarySystem.Unity.Helper
{
    public class DataUnity
    {
        public static int GetCanBorrowCount(UserInfo userInfo)
        {
            int returnInt = 0;
            switch (userInfo.Grade) 
            {
                case UserGrade.Resign:
                    returnInt = 0;
                    break;
                
                case UserGrade.Junior:
                case UserGrade.PrepVolunteer:
                    returnInt = 2;
                    break;


                case UserGrade.Ordinary:
                    returnInt = 3;
                    break;

                case UserGrade.Admin:                              
                case UserGrade.VIP:
                    returnInt = 3;
                    break;
            }

            return returnInt;
        }
    }
}
