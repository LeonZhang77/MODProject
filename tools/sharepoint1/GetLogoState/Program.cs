using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using Microsoft.SharePoint.Client;
using System.Net;
using System.IO;

namespace GetLogoState
{
    class Program
    { 
        
        static ClientContext clientContext;
        static WebClient webClient;
        static ListItemCollection listItems = null;
        static ListItemCollection picListItems = null;
        static ArrayList nameArr = new ArrayList();
        static string result = null;
        static string fName = @"C:\share\Nologo.txt";
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
            char[] chArr = inputString.Trim().Replace(" ", "").ToLower().ToCharArray();
            foreach (char tempCh in chArr)
            {
                if (((tempCh >= 'a') && (tempCh <= 'z')) || ((tempCh >= '0') && (tempCh <= '9'))) { appKey = appKey + tempCh; }
            }
            return appKey;

        }

      
        static void CreateResult(string appKeyStr)
        {
            result = result + appKeyStr + ',';
            Console.WriteLine(appKeyStr);
            
            foreach (ListItem listItem in listItems)
            {
                string temp = GetAppKey(listItem["Application_x0020_Name"].ToString());
                if (temp.Equals(appKeyStr))
                {
                    result = result + listItem["Application_x0020_Name"].ToString() + ',';
                    Console.WriteLine(listItem["Application_x0020_Name"].ToString());
                    result = result + listItem["Logo_x0020_Status"].ToString() + ',';
                    Console.WriteLine(listItem["Logo_x0020_Status"].ToString());
                    if (listItem["Logo_x0020_Status"].ToString().Equals("logo work done"))
                    {
                        foreach (ListItem picItem in picListItems)
                        {
                            if (GetAppKey(picItem["FileRef"].ToString()).Contains(appKeyStr))
                            {
                                result = result + picItem["FileRef"].ToString();
                                Console.WriteLine(picItem["FileRef"].ToString());
                                break;
                            }
                        }
                    }
                    
                }
                
            }
   
   
        }
        static void Main(string[] args)
        {
            GetSPList("http://sharepoint/sites/fim/ISV9-3", "v-jianzh", "isnes21)$MAM", "fareast.corp.microsoft.com");
            GetSPLogo("http://sharepoint/sites/fim/ISV9-3", "v-jianzh", "isnes21)$MAM", "fareast.corp.microsoft.com");

            //Console.WriteLine("please choose bak.txt File");
            //OpenFileDialog ofD = new OpenFileDialog(); ofD.InitialDirectory = activeDir; ofD.ShowDialog();
            //string fName = ofD.FileName;
            using (StreamReader sr = new StreamReader(fName))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    line = line.Trim().Replace(" ", "").ToLower();
                    CreateResult(line);
                    result = result + Environment.NewLine;
                }
            }
            System.IO.File.WriteAllText(@"c:\share\result.txt", result);
            Console.WriteLine("Press any Key to continue!");
            Console.ReadLine();

        }
    }
}
