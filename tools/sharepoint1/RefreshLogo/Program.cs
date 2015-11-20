using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using Microsoft.SharePoint.Client;
using System.Windows.Forms;
using System.Net;
using System.IO;


namespace RefreshLogo
{
    class Program
    {
        static ClientContext clientContext;
        static WebClient webClient;
        static ListItemCollection listItems = null;
        static ListItemCollection picListItems = null;
        static ArrayList nameArr = new ArrayList();
        static string result = null;
        static string activeDir = @"C:\share\2014_06_09_100Apps\"; 
       
        static void GetSPList(string URL, string userName, string password, string domain)
        {
            Console.WriteLine("started to get List from Sharepoint...");
            try
            {
                clientContext = new ClientContext(URL);
                webClient = new WebClient();
                System.Net.NetworkCredential nck = new System.Net.NetworkCredential(userName, password, domain);
                clientContext.Credentials = nck;
                webClient.Credentials = nck;
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

        static void GetSPLogo(string URL, string userName, string password, string domain)
        {
            Console.WriteLine("started to get Pics from Sharepoint...");
            try
            {
                clientContext = new ClientContext(URL);
                System.Net.NetworkCredential nck = new System.Net.NetworkCredential(userName, password, domain);
                clientContext.Credentials = nck;
                List phase3 = clientContext.Web.Lists.GetByTitle("Web SSO Logo Files");
                CamlQuery camlQuery = new CamlQuery();
                camlQuery.ViewXml = @"
                <View/>
                ";
                picListItems = phase3.GetItems(camlQuery);
                clientContext.Load(picListItems);
                clientContext.ExecuteQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Fail" + ex.Message);
            }
            Console.WriteLine("Done!");
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
        static bool IsLogoDone(string appKeyStr)
        {
            bool returnValue = false;

            foreach (ListItem listItem in listItems)
            {
                string temp = GetAppKey(listItem["Application_x0020_Name"].ToString());
                if (temp.Equals(appKeyStr))
                {
                    if (listItem["Logo_x0020_Status"].ToString().Equals("logo work done")) 
                    { 
                        returnValue = true;
                        //uriStr = uriStr + listItem["Application_x0020_Name"].ToString() + "/logo/";
                        //uriStr = uriStr.Replace(' ','_');
                    }
                    break;
                }
            }
            return returnValue;
        
        }

        static string GetFileDir(string appKeystr)
        {
            string returnValue = null;
            foreach (ListItem picItem in picListItems)
            {
                if (GetAppKey(picItem["FileRef"].ToString()).Contains(appKeystr))
                {
                    returnValue = picItem["FileRef"].ToString();
                    break;
                }
       

            }
            return returnValue;

        }
        static void DownloadLogo(string appKeyStr)
        {
            if (IsLogoDone(appKeyStr))
            {
                String fileName1 = @"\45x45.png";
                String fileName2 = @"\150x122.png";
                String fileName3 = @"\215x215.png";
                string filePath = @"c:";
                string uriStr = "http://sharepoint";
                try 
                {
                    uriStr = "http://sharepoint";
                    uriStr = uriStr + GetFileDir(appKeyStr) + "/logo/";
                    Console.WriteLine(appKeyStr + "-----found Dir: " + uriStr + Environment.NewLine);
                    
                    webClient.DownloadFile(uriStr + fileName1, fileName1);
                    webClient.DownloadFile(uriStr + fileName2, fileName2);
                    webClient.DownloadFile(uriStr + fileName3, fileName3);
                    Console.WriteLine(appKeyStr + "-----downloaded! " + Environment.NewLine);
                   
                    string aimPath = activeDir + appKeyStr + @"\logo";
                    System.IO.File.Copy(filePath + fileName1, aimPath + fileName1, true);
                    System.IO.File.Copy(filePath + fileName2, aimPath + fileName2, true);
                    System.IO.File.Copy(filePath + fileName3, aimPath + fileName3, true);
                    Console.WriteLine(appKeyStr + "-----finished! " + Environment.NewLine);
                }
                catch (Exception ex) 
                {
                    Console.WriteLine(appKeyStr + "-----EXCEPTION! " + ex.Message + Environment.NewLine);
                    result += "Logo isn't download successfully!-----" + appKeyStr + "-----" + ex.Message + Environment.NewLine;
                    result += "Logo isn't download successfully!-----" + appKeyStr + "-----" + uriStr + Environment.NewLine;
              
                }
                

            }
            else
            {
                Console.WriteLine(appKeyStr + "-----Logo is not Ready!" + Environment.NewLine);
                result += "Logo is not Ready!-----" + appKeyStr + Environment.NewLine;
            }
        }

        [STAThread]
        static void Main(string[] args)
        {
            GetSPList("http://sharepoint/sites/fim/ISV9-3", "v-jianzh", "isnes21)$MAM", "fareast.corp.microsoft.com");
            GetSPLogo("http://sharepoint/sites/fim/ISV9-3", "v-jianzh", "isnes21)$MAM", "fareast.corp.microsoft.com");

            Console.WriteLine("please choose bak.txt File");
            OpenFileDialog ofD = new OpenFileDialog(); ofD.InitialDirectory = activeDir; ofD.ShowDialog();
            string fName = ofD.FileName;
            using (StreamReader sr = new StreamReader(fName))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    DownloadLogo(line);
                }
            }
            System.IO.File.WriteAllText(@"c:\share\result.txt", result);
            Console.WriteLine("Press any Key to continue!");
            Console.ReadLine();
        }
    }
}

