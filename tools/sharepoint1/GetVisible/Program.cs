using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Text;



namespace visiableToTrue
{
    [System.Runtime.Serialization.DataContract]
    class Metadata
    {
        [System.Runtime.Serialization.DataMember]
        //public string AppCategory { get; set; }
        public string AppKey { get; set; }
         [System.Runtime.Serialization.DataMember]
        //public string IdentifierUrl { get; set; }
        public string HomePageUrl { get; set; }
       
        //public string DisplayName { get; set; }
        
        //public string Publisher { get; set; }
        //public string Visible { get; set; }
        //public string ChangeHistory { get; set; }
        //public string Version { get; set; }
        //public string EntitlementsAvailable { get; set; }
        //public string Entitlements { get; set; }
        //public string DisplayCategories { get; set; }
    }
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            string activeDir = @"Y:\";
            //string activeDir = args[0];
            string fileName = @"\metadata.json";

            using (StreamReader sr = new StreamReader(activeDir + "AppKey.txt"))
            {
                string line;
                string result = null;
                while ((line = sr.ReadLine()) != null)
                {

                    try
                    {
                        String content = File.ReadAllText(activeDir + line + fileName);
                        //content = content.Replace("\"Visible\": false", "\"Visible\": true");
                        DataContractJsonSerializer outDs = new DataContractJsonSerializer(typeof(Metadata));
                        using (MemoryStream outMs = new MemoryStream(Encoding.UTF8.GetBytes(content)))
                        {
                            Metadata md = outDs.ReadObject(outMs) as Metadata;
                            String tempVisible = md.HomePageUrl.Trim();
                            String tempAppKey = md.AppKey.Trim();
                            result = result + tempAppKey+","+tempVisible + Environment.NewLine;

                            Console.WriteLine("AppKey:" + md.AppKey + "......Visible:" + md.HomePageUrl);
                        }

                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                        result = result + e.Message + Environment.NewLine;
                    }

                }
                File.WriteAllText(activeDir + "result.txt", result);
            }

            Console.WriteLine("Press Enter to continue....");
            Console.ReadLine();
        }
    }
}
