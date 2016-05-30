using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wicresoft.MODLibrarySystem.Unity.Helper
{
    public class ColorUnity
    {
        public static string GetRandomColor()
        {
            Random RandomNum_First = new Random((int)DateTime.Now.Ticks);
            Random RandomNum_Sencond =  new Random(Guid.NewGuid().GetHashCode());
            
            int int_Red = RandomNum_First.Next(256);
            int int_Green = RandomNum_Sencond.Next(256);
            int int_Blue = (int_Red + int_Green > 400) ? 0 : 400 - int_Red - int_Green;
            int_Blue = (int_Blue > 255) ? 255 : int_Blue;

           Color color = Color.FromArgb(int_Red, int_Green, int_Blue);
            string strColor = "#" + Convert.ToString(color.ToArgb(), 16).PadLeft(8, '0').Substring(2, 6);
            return strColor;

        }
     }
}
