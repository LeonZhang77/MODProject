using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;

using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;


namespace sharepoint1
{
    class Program
    {
        static ArrayList appKeyArr = new ArrayList();
        static ArrayList VisibleArr = new ArrayList();
        static Excel.Application xApp;
        static Excel.Workbook xbook;
        static Excel.Worksheet xSheet;
        static void GetVisibleFormExcel()
        {
            //Console.WriteLine("please choose Excel File");
            //OpenFileDialog ofD = new OpenFileDialog(); ofD.ShowDialog();
            //string fName = ofD.FileName;

            //Excel.Application xApp = new Excel.Application();
            //xApp.Visible = true;
            //Excel.Workbook xbook = xApp.Workbooks.Open("C:\\share\\temp.xlsx");
            xSheet = xbook.Sheets[2];
            xSheet.Select();


            Excel.Range xRange = null;
            string appKeyStr = null; string visibleStr = null; 
            int i = 2;
            xRange = xSheet.Cells[i, 1]; appKeyStr = xRange.Value2;
            while (!String.IsNullOrEmpty(appKeyStr))
            {
                appKeyArr.Add(appKeyStr.ToString().Trim());
                xRange = xSheet.Cells[i, 3]; visibleStr = xRange.Value2.ToString().Trim(); VisibleArr.Add(visibleStr);
                
                //Console.WriteLine("Name:{0} IE:{1} FireFox:{2} Chrome:{3}", appName,ie,firefox,chrome);
                i++;
                xRange = xSheet.Cells[i, 1]; appKeyStr = xRange.Value2; 
            }

            //Console.WriteLine("Done");
            xSheet = null; 
            //xbook = null; xApp.Quit(); xApp = null;
        }

        static string GetVisibleByAppKeyStr(string appKeyStr)
        {
            string resultStr = "/";
            
            string [] tempStrArr = appKeyStr.Split(new char[] {'\\','/'});
            foreach (string tempStr in tempStrArr)
            {
                int i = appKeyArr.IndexOf(tempStr.Trim());
                resultStr = resultStr + VisibleArr[i].ToString()+"/";
            }
            resultStr = resultStr.Substring(1,resultStr.Length-2);
            return resultStr;

        }

        [STAThread]
        static void UpdateToMasterList()
        {
            //Console.WriteLine("please choose Excel File");
            //OpenFileDialog ofD = new OpenFileDialog(); ofD.ShowDialog();
            //string fName = ofD.FileName;

            //Excel.Application xApp = new Excel.Application();
            //xApp.Visible = true;
            //Excel.Workbook xbook = xApp.Workbooks.Open("C:\\share\\temp.xlsx");
            xSheet = xbook.Sheets[1];
            xSheet.Select();
            
            Excel.Range xRange = null;
            string appNameStr = null; string visibleStr = null; string appKeyStr = null;
            int i = 2;
            xRange = xSheet.Cells[i, 1]; appNameStr = xRange.Value2;
            while (!String.IsNullOrEmpty(appNameStr))
            {

                xRange = xSheet.Cells[i, 6]; appKeyStr = xRange.Value2;

                if (!String.IsNullOrEmpty(appKeyStr)) 
                { 
                    visibleStr = GetVisibleByAppKeyStr(appKeyStr.ToString().Trim());
                    xRange = xSheet.Cells[i, 7]; xRange.Value2 = visibleStr.ToString();
                }

                
                i++;
                xRange = xSheet.Cells[i, 1]; appNameStr = xRange.Value2;
            }

            //Console.WriteLine("Done");
            xSheet = null; 
        //    xSheet = null; xbook = null; xApp.Quit(); xApp = null;
        }

        [STAThread]
        static void Main(string[] args)
        {

            //GetSPList("http://sharepoint/sites/fim/ISV9-3", "v-jianzh", "isnes21)$MAM", "fareast.corp.microsoft.com");

            //foreach (ListItem listItem in listItems)
            //{
            //    Console.WriteLine("ID:{0} Application Name:{1}", listItem.Id, listItem["Application_x0020_Name"].ToString());
            //}
            xApp = new Excel.Application();
            xApp.Visible = true;
            xbook = xApp.Workbooks.Open("C:\\share\\temp.xlsx");

            GetVisibleFormExcel();

            //for (int i = 0; i < nameArr.Count;i++ )
            //{
            //    Console.WriteLine("Name:{0} IE:{1} FireFox:{2} Chrome:{3}", nameArr[i],ieArr[i],firefoxArr[i],chromeArr[i]);
            //}

            UpdateToMasterList();
            
            xbook = null; xApp.Quit(); xApp = null;
        
            Console.ReadLine();

        }
    }
}
