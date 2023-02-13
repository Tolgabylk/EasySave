using System;
using System.Collections.Generic;
using System.Text;

namespace EasySave_v1._0._0
{
    internal class Program
    {
        static void Main(string[] args)
        { 
            StateLog c = new StateLog("Test", @"C:\\EasySaveLog", @"C:\\","test");
            /*c.GenerateLogEtatJSON();
            Console.WriteLine("test");*/
            DailyLog d = new DailyLog("test", @"C:\\EasySaveLog", @"C:\\", "test");

            d.GenerateLogDailyXML();
            c.GenerateLogEtatJSON();

            Console.Read();
        }
    }
}
