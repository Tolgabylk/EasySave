using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

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
            PrepareWriteLogJSON(array);
        }
        public void GenerateLogDailyXML()
        {
            XmlDocument document = new XmlDocument();

            // Créer le noeud racine
            XmlElement root = document.CreateElement("DailyLog");
            document.AppendChild(root);

            // Créer le noeud DataForLog dans un document XML différent
            XmlDocument tempDoc = new XmlDocument();
            XmlElement DataForLog = tempDoc.CreateElement("DailyLog");
            DataForLog.AppendChild(tempDoc.CreateElement("Name")).InnerText = this.Name;
            DataForLog.AppendChild(tempDoc.CreateElement("FileSource")).InnerText = this.FileSource;
            DataForLog.AppendChild(tempDoc.CreateElement("FileTarget")).InnerText = this.FileTarget;
            DataForLog.AppendChild(tempDoc.CreateElement("Destpath")).InnerText = this.DesthPath;
            DataForLog.AppendChild(tempDoc.CreateElement("FileSize")).InnerText = this.FileSize.ToString();
            DataForLog.AppendChild(tempDoc.CreateElement("FileTransferTime")).InnerText = this.FileTransferTime.ToString();
            DataForLog.AppendChild(tempDoc.CreateElement("Time")).InnerText = this.TIME;

            // Importer le noeud DataForLog dans le document XML principal
            XmlNode importedNode = document.ImportNode(DataForLog, true);
            root.AppendChild(importedNode);

            PrepareWriteLogXML(document);
        }

        public void PrepareWriteLogJSON(JArray DataForLog)
        {

            if (!Directory.Exists(Path.Combine(Path.GetPathRoot(AppDomain.CurrentDomain.BaseDirectory), "/EasySaveLog/")))
            {
                Directory.CreateDirectory(Path.Combine(Path.GetPathRoot(AppDomain.CurrentDomain.BaseDirectory), "/EasySaveLog/"));
            }
            else
            {
                if (!File.Exists(LOGDESTPATH + "json"))
                {
                    File.Create(LOGDESTPATH + "json");
                }
            }

            File.WriteAllText(LOGDESTPATH + "json", DataForLog.ToString());

            StreamWriter file = File.CreateText(DESTPATH);
            using (JsonTextWriter writer = new JsonTextWriter(file))
            {
                DataForLog.WriteTo(writer);
            }
        }
        public void PrepareWriteLogXML(XmlDocument DataForLog)
        {

            if (!Directory.Exists(Path.Combine(Path.GetPathRoot(AppDomain.CurrentDomain.BaseDirectory), "/EasySaveLog/")))
            {
                Directory.CreateDirectory(Path.Combine(Path.GetPathRoot(AppDomain.CurrentDomain.BaseDirectory), "/EasySaveLog/"));
            }
            else
            {
                if (!File.Exists(LOGDESTPATH + "xml"))
                {
                    File.Create(LOGDESTPATH + "xml");
                }
            }

            File.WriteAllText(LOGDESTPATH+"xml", DataForLog.InnerXml);

            StreamWriter file = File.CreateText(DESTPATH);
            XmlTextWriter writer = new XmlTextWriter(file);

            DataForLog.WriteTo(writer);

        }

        #endregion Methods
    }
}
