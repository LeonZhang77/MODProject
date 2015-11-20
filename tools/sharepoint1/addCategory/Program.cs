using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Collections;
using Microsoft.SharePoint.Client;
using Excel = Microsoft.Office.Interop.Excel;

namespace addCategory
{
    
    class Program
    {
        static ArrayList nameArr = new ArrayList();
        static ArrayList categoryArr = new ArrayList();
        static string result = null;
        static ClientContext clientContext;
        static ListItemCollection listItems = null;
        static string activeDir = @"C:\share\bak\";
        static void GetCategoryFormExcel()
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
            string appName = null; string category = null; 
            int i = 2;
            xRange = xSheet.Cells[i, 1]; appName = xRange.Value2;
            while (!String.IsNullOrEmpty(appName))
            {
                nameArr.Add(GetAppKey(appName));
                xRange = xSheet.Cells[i, 2];
                try
                { category = xRange.Value2; }
                catch (Exception ex)
                { category = ""; }
                categoryArr.Add(category);           
                i++;
                xRange = xSheet.Cells[i, 1]; appName = xRange.Value2;
            }

            Console.WriteLine("Done");
            xSheet = null; xbook = null; xApp.Quit(); xApp = null;
        }
        static string GetAppKey(string inputString)
        {
            string appKey = "";
            char[] chArr = inputString.Trim().Replace(" ", "").ToLower().ToCharArray();
            foreach (char tempCh in chArr)
            {
                if (((tempCh >= 'a') && (tempCh <= 'z')) || ((tempCh >= '0') && (tempCh <= '9'))) { appKey = appKey + tempCh; }
            }
            return appKey;

        }

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
                foreach (ListItem listItem in listItems)
                {
                    nameArr.Add(GetAppKey(listItem["Application_x0020_Name"].ToString()));
                    if (listItem["Category"] == null)
                    {
                        categoryArr.Add("");
                    }
                    else 
                    {
                        categoryArr.Add(listItem["Category"].ToString());
                    }
                    
                  
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Fail" + ex.Message);
            }
            Console.WriteLine("Done!");
        }
        static string getCategory(string appKey)
        {
            string returnValue = "";
            for (int i = 0; i<nameArr.Count;i++)
            { 
                if (appKey.Equals(nameArr[i].ToString()))
                {
                    try { returnValue = categoryArr[i].ToString();}
                    catch (Exception ex)
                    { }
                    
                    
                }
            }
            return returnValue;
  
        }
        static bool updateMetafile(string appkey, string category)
        {
            bool returnValue = false;
            string fileName = @"\metadata.json"; 
            try
            {
                Console.WriteLine(appkey);
                String content = System.IO.File.ReadAllText(activeDir + appkey + fileName);
                //content = content.Replace("\"Visible\": false", "\"Visible\": true");
                int temp = content.LastIndexOf(']');
                content = content.Substring(0,temp+1);
                content = content + "," + Environment.NewLine;
                content = content + "\"DisplayCategories\": [" + Environment.NewLine;
                content = content +'"' + category + '"'+ Environment.NewLine;
                content = content +"]" +Environment.NewLine;
                content = content +"}" +Environment.NewLine;

                System.IO.File.WriteAllText(activeDir + appkey + fileName, content);
                //Console.WriteLine(content);
                result = result + "Done!" + Environment.NewLine;
                returnValue = true;

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                result = result + e.Message + Environment.NewLine;
            }
            return returnValue;
        }

        [STAThread]
        static void Main(string[] args)
        {

            GetCategoryFormExcel();
            //GetSPList("http://sharepoint/sites/fim/ISV9-3", "v-jianzh", "isnes21)$MAM", "fareast.corp.microsoft.com");
            
            //@"\\app-psso-china\WAAD Password SSO Wicresoft Team\metaData\2014_2_13_105Apps\adcash\logo\";
            Console.WriteLine("please choose bak.txt File");
            OpenFileDialog ofD = new OpenFileDialog();
            ofD.InitialDirectory = activeDir;
            ofD.ShowDialog();
            string fName = ofD.FileName;

            using (StreamReader sr = new StreamReader(fName))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    Console.WriteLine(line);
                    result += line;
                    string category = getCategory(line);
                    Console.WriteLine(category);
                    result += "-----" + category;
                    bool tempB = updateMetafile(line, category);
                    Console.WriteLine(tempB);
                    result += "-----" + tempB.ToString() + Environment.NewLine;
                }


            }
            System.IO.File.WriteAllText(@"c:\share\result.txt", result);
            Console.WriteLine("Press Enter to continue....");
            Console.ReadLine();
        }
    }
}
