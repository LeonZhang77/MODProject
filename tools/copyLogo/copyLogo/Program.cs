using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace copyLogo
{
    class Program
    {
        
        static void doCopy(string aimPath)
        {
            string filePath = @"\\app-psso-china\WAAD Password SSO Wicresoft Team\metaData\logo\";
            if (Directory.Exists(aimPath) == false)
            {
                Directory.CreateDirectory(aimPath);
            }
            if (File.Exists(aimPath + @"\45x45.png") == false) { File.Copy(filePath + @"\45x45.png", aimPath + @"\45x45.png"); }
            if (File.Exists(aimPath + @"\150x122.png") == false) { File.Copy(filePath + @"\150x122.png", aimPath + @"\150x122.png"); }
            if (File.Exists(aimPath + @"\215x215.png") == false) { File.Copy(filePath + @"\215x215.png", aimPath + @"\215x215.png"); }

            Console.WriteLine(aimPath);
        }
        
        [STAThread]
        static void Main(string[] args)
        {
            string activeDir = @"C:\share\bak\"; 
            //@"\\app-psso-china\WAAD Password SSO Wicresoft Team\metaData\2014_2_13_105Apps\adcash\logo\";
            Console.WriteLine("please choose bak.txt File");
            OpenFileDialog ofD = new OpenFileDialog(); ofD.InitialDirectory = activeDir; ofD.ShowDialog();
            string fName = ofD.FileName;

            using (StreamReader sr = new StreamReader(fName))
            {
                string line;
                string result = null;
                while ((line = sr.ReadLine()) != null)
                {
                    Console.WriteLine(line);
                    result = result + line + Environment.NewLine;
                    doCopy(activeDir + line + @"\logo");


                }

                
            }
            
         
            Console.WriteLine("Press Enter to continue....");
            Console.ReadLine();
        }
    }
}
