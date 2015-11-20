using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using Microsoft.SharePoint.Client;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;


namespace sharepoint1
{   
    class Program
    {
        static ClientContext clientContext;
        static ListItemCollection listItems = null;
        static ArrayList nameArr = new ArrayList();
        static ArrayList ieArr = new ArrayList();
        static ArrayList firefoxArr = new ArrayList();
        static ArrayList chromeArr = new ArrayList();
       
        static void GetSPList(string URL, string userName, string password, string domain)
        {
            Console.WriteLine("started to get List from Sharepoint...");
            try
            {
                clientContext = new ClientContext(URL);
                System.Net.NetworkCredential nck = new System.Net.NetworkCredential(userName, password, domain);
                clientContext.Credentials = nck;
                List phase3 = clientContext.Web.Lists.GetByTitle("Phase 3 SSO App Status");
                CamlQuery camlQuery = new CamlQuery();
                camlQuery.ViewXml = @"
                <View/>
                ";
                listItems = phase3.GetItems(camlQuery);
                clientContext.Load(listItems);
                clientContext.ExecuteQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Fail" + ex.Message);
            }
            Console.WriteLine("Done!");
        }
       
        static void GetPPEFormExcel()
        {
            Console.WriteLine("please choose Excel File");
            OpenFileDialog ofD = new OpenFileDialog(); ofD.ShowDialog();
            string fName = ofD.FileName;
           
            Excel.Application xApp = new Excel.Application();
            xApp.Visible = true;
            Excel.Workbook xbook = xApp.Workbooks.Open(fName);
            Excel.Worksheet xSheet = xbook.Sheets[1];
            xSheet.Select();

            
            Excel.Range xRange = null;
            string appName = null; string ie = null; string firefox = null; string chrome = null;
            int i = 2;
            xRange = xSheet.Cells[i,1]; appName = xRange.Value2;
            while (!String.IsNullOrEmpty(appName))
            {
                nameArr.Add(GetAppKey(appName));
                xRange = xSheet.Cells[i, 4]; ie = xRange.Value2; ieArr.Add(ie);
                xRange = xSheet.Cells[i, 5]; firefox = xRange.Value2; firefoxArr.Add(firefox);
                xRange = xSheet.Cells[i, 6]; chrome = xRange.Value2; chromeArr.Add(chrome);
                //Console.WriteLine("Name:{0} IE:{1} FireFox:{2} Chrome:{3}", appName,ie,firefox,chrome);
                i++;
                xRange = xSheet.Cells[i, 1]; appName = xRange.Value2;
            }
            
            Console.WriteLine("Done");
            xSheet = null; xbook = null; xApp.Quit();xApp = null;
        }

        static string GetAppKey(string inputString)
        {
            string appKey = "";
            char[] chArr= inputString.Trim().Replace(" ", "").ToLower().ToCharArray();
            foreach (char tempCh in chArr)
            {
                if (((tempCh >= 'a') && (tempCh <= 'z'))||((tempCh >= '0') && (tempCh <= '9'))) { appKey = appKey + tempCh; }
            }
            return appKey;

        }
        static void UpdateToSP()
        {
            string temp;
            string result = null;
            Console.WriteLine("started to update to Sharepoint...");
            bool flag = false;
            for (int i = 0; i < nameArr.Count; i++)
            {
                flag = false;
                foreach (ListItem listItem in listItems)
                {
                    temp = GetAppKey(listItem["Application_x0020_Name"].ToString());
                    if (temp.Equals(nameArr[i]))
                    {
                        listItem["IE"] = ieArr[i];
                        listItem["FireFox"] = firefoxArr[i];
                        listItem["Chrome"] = chromeArr[i];
                        listItem["Upload"] = "Done";
                        listItem.Update();
                        clientContext.ExecuteQuery();
                        flag = true;
                    }
                    
                }
                if (flag)
                {
                    Console.WriteLine("{0} Done!", nameArr[i]);
                    //result = result +  "Done!   "+ nameArr[i]  + Environment.NewLine;                  
                }else{
                    Console.WriteLine("{0} Not Found!", nameArr[i]);
                    result = result + "Not Found!   "+ nameArr[i]  + Environment.NewLine;
                }
            }

            System.IO.File.WriteAllText(@"c:\share\result.txt", result);
            Console.WriteLine("Press any Key to continue!");
        }

         [STAThread]
        static void Main(string[] args)
        {
            
            GetSPList("http://sharepoint/sites/fim/ISV9-3", "v-jianzh", "isnes21)$MAM", "fareast.corp.microsoft.com");

            //foreach (ListItem listItem in listItems)
            //{
            //    Console.WriteLine("ID:{0} Application Name:{1}", listItem.Id, listItem["Application_x0020_Name"].ToString());
            //}

            GetPPEFormExcel();

            //for (int i = 0; i < nameArr.Count;i++ )
            //{
            //    Console.WriteLine("Name:{0} IE:{1} FireFox:{2} Chrome:{3}", nameArr[i],ieArr[i],firefoxArr[i],chromeArr[i]);
            //}

            UpdateToSP();

            Console.ReadLine();

        }
    }
}
