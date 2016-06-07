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
                    returnInt = 2;
                    break;

                case UserGrade.Ordinary:
                case UserGrade.Senior:
                    returnInt = 3;
                    break;

                case UserGrade.Admin:                              
                case UserGrade.VIP:
                case UserGrade.PrepVolunteer:
                    returnInt = 5;
                    break;
            }

            return returnInt;
        }

        public static bool GetCanRenewLevel(UserInfo userInfo)
        {
            bool returnBool = true;
            switch (userInfo.Grade)
            {
                case UserGrade.Resign:
                case UserGrade.Junior:
                    returnBool = false;
                    break;

                case UserGrade.PrepVolunteer:
                case UserGrade.Ordinary:
                case UserGrade.Senior:
                case UserGrade.Admin:
                case UserGrade.VIP:
                    break;
            }

            return returnBool;
        }
    }
}
