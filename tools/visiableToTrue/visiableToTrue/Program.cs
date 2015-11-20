using System;
using System.Collections.Generic;
using System.IO;

namespace visiableToTrue
{
    class Program
    {
        static void Main(string[] args)
        {
            string activeDir = @"C:\share\2014_06_03_100Apps\";
            //string activeDir = args[0];
            string fileName = @"\metadata.json";               
            
            using (StreamReader sr = new StreamReader(activeDir + "bak.txt"))
            {
                string line;
                string result = null;
                while ((line = sr.ReadLine())!=null )
                {
                     
                    Console.WriteLine(line);
                    result = result + line + ":     ";
                    try { 
                        String content = File.ReadAllText(activeDir + line + fileName);
                        content = content.Replace("\"Visible\": false", "\"Visible\": true");
                        //content = content.Replace("[ \"", "[\"");
                        //content = content.Replace("\" ]", "\"]");
                        File.WriteAllText(activeDir + line + fileName, content);
                        Console.WriteLine(content);
                        result = result + "Done!" + Environment.NewLine;
                    }catch (Exception e)
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
