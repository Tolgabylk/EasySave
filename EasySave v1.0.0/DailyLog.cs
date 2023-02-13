using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;
using System.Xml.Linq;
using static EasySave_v1._0._0.Enums;

namespace EasySave_v1._0._0
{
    internal class DailyLog
    {
        #region Properties 
        public string Name { get; set; }
        public string FileSource { get; set; }
        public string FileTarget { get; set; }
        public string DesthPath { get; set; }
        public int FileSize { get; set; }
        public int FileTransferTime { get; set; }

        //Readonly properties
        public readonly string TIME = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");

        public readonly string DESTPATH = Path.Combine(Path.GetPathRoot(AppDomain.CurrentDomain.BaseDirectory), "/EasySaveLog/state.json");

        public readonly string LOGDESTPATH = "C:/EasySaveLog/daily.";
        #endregion Properties 

        #region Constructors
        public DailyLog(string name, string fileSource, string fileTarget, string desthPath, int fileSize, int fileTransferTime)
        {
            Name = name;
            FileSource = fileSource;
            FileTarget = fileTarget;
            DesthPath = desthPath;
            FileSize = fileSize;
            FileTransferTime = fileTransferTime;
            
        }

        public DailyLog() { }

        #endregion Constructors

        #region Methods
        public void GenerateLogDailyJSON() 
        {
            JObject DataForLog = new JObject(new JProperty("Name", this.Name), new JProperty("FileSource", this.FileSource), new JProperty("FileTarget", this.FileTarget), new JProperty("Destpath", this.DesthPath), new JProperty("FileSize", this.FileSize), new JProperty("FileTransferTime", this.FileTransferTime), new JProperty("Time", this.TIME));
            JArray array = new JArray(DataForLog);
            PrepareWriteLog(array, EXTENSION.JSON.ToString());
        }
        public void GenerateLogDailyXML() 
        {

            XElement DataForLog = new XElement(new JProperty("Name", this.Name), new JProperty("FileSource", this.FileSource), new JProperty("FileTarget", this.FileTarget), new JProperty("Destpath", this.DesthPath), new JProperty("FileSize", this.FileSize), new JProperty("FileTransferTime", this.FileTransferTime), new JProperty("Time", this.TIME));
            JArray array = new JArray(DataForLog);
            PrepareWriteLog(array, EXTENSION.XML.ToString());
        }

        public void PrepareWriteLog(JArray DataForLog, string extension)
        {
            if (extension == EXTENSION.JSON.ToString())
            {
                if (!Directory.Exists(Path.Combine(Path.GetPathRoot(AppDomain.CurrentDomain.BaseDirectory), "/EasySaveLog/")))
                {
                    Directory.CreateDirectory(Path.Combine(Path.GetPathRoot(AppDomain.CurrentDomain.BaseDirectory), "/EasySaveLog/"));
                }
                else
                {
                    if (!File.Exists(LOGDESTPATH+"json"))
                    {
                        File.Create(LOGDESTPATH+"json");
                    }
                }

                File.WriteAllText(DESTPATH, DataForLog.ToString());

                StreamWriter file = File.CreateText(DESTPATH);
                using (JsonTextWriter writer = new JsonTextWriter(file))
                {
                    DataForLog.WriteTo(writer);
                }
            }
            else if (extension == EXTENSION.XML.ToString())
            {
                if (!Directory.Exists(Path.Combine(Path.GetPathRoot(AppDomain.CurrentDomain.BaseDirectory), "/EasySaveLog/")))
                {
                    Directory.CreateDirectory(Path.Combine(Path.GetPathRoot(AppDomain.CurrentDomain.BaseDirectory), "/EasySaveLog/"));
                }
                else
                {
                    if (!File.Exists(LOGDESTPATH+"xml"))
                    {
                        File.Create(LOGDESTPATH+"xml");
                    }
                }

                File.WriteAllText(DESTPATH, DataForLog.ToString());

                StreamWriter file = File.CreateText(DESTPATH);
                XmlTextWriter writer = new XmlTextWriter(file);

                //DataForLog.WriteTo(writer);

            }
        }
        #endregion Methods
    }
}
