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
            DailyLog d = new DailyLog("test", "test", "test", "test", 333, 10101901);
            DailyLog g = new DailyLog("test", "test", "test", "test", 10000, 10101901);

            c.GenerateLogEtatJSON();

            Console.Read();
        }
    }
}
