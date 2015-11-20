using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using Microsoft.SharePoint.Client;
using System.Windows.Forms;
using System.IO;


namespace UpdateProdToSP
{
    class Program
    {

        static ClientContext clientContext;
        static ListItemCollection listItems;
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

        [STAThread]
        static void Main(string[] args)
        {

            GetSPList("http://sharepoint/sites/fim/ISV9-3", "v-jianzh", "isnes21)$MAM", "fareast.corp.microsoft.com");

            Console.WriteLine("please choose txt File");
            OpenFileDialog ofD = new OpenFileDialog();
            ofD.InitialDirectory = @"\\APP-PSSO-CHINA\WAAD Password SSO Wicresoft Team\metaData\AppsPublisehdToProd";
            ofD.ShowDialog();
            string fName = ofD.FileName;

            using (StreamReader sr = new StreamReader(fName))
            {
                string line;
                string result = null;
                bool flag;
                string temp;
                while ((line = sr.ReadLine()) != null)
                {
                    //Console.WriteLine(line);
                    //result = result + line + Environment.NewLine;
                    //(activeDir + line + @"\logo");
                    flag = false;
                    foreach (ListItem listItem in listItems)
                    {
                        temp = GetAppKey(listItem["Application_x0020_Name"].ToString());
                        if (temp.Equals(line))
                        {
                            listItem["Publish_x0020_To_x0020_PROD"] = "Yes";
                           
                            listItem.Update();
                            clientContext.ExecuteQuery();
                            flag = true;
                        }

                    }
                    if (flag)
                    {
                        Console.WriteLine("{0} Done!", line);
                        //result = result +  "Done!   "+ nameArr[i]  + Environment.NewLine;                  
                    }
                    else
                    {
                        Console.WriteLine("{0} Not Found!", line);
                        result = result + "Not Found!   " + line + Environment.NewLine;
                    }
                }
                System.IO.File.WriteAllText(@"c:\share\result.txt", result);
                Console.WriteLine("Press any Key to continue!");
                Console.ReadLine();
            }
        }
    }
}
