using System;
using System.Collections.Generic;
using System.Text;

namespace EasySave_v1._0._0
{
    internal class Log
    {
        public StateLog StateLog { get; set; }

        public DailyLog DailyLog { get; set; }

        public string ChoixLog { get; set; }

        public Log(string choixLog)
        {
            if(choixLog == "Daily")
            {
                DailyLog = new DailyLog();
            }
            else
            {
                StateLog = new StateLog();
            }
        }

    }
}
                