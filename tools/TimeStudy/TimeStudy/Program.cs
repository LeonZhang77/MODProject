using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeStudy
{
    class Program
    {
        static void Main(string[] args)
        {
            System.DateTime timeNow = System.DateTime.Now;
            Console.WriteLine(timeNow.ToString("yyyy-MM-dd"));
            Console.ReadLine();
        }
    }
}
