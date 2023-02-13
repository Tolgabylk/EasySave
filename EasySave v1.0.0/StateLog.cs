using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Xml;
using static EasySave_v1._0._0.Enums;

namespace EasySave_v1._0._0
{/// <summary>
/// Class used to model a status log of a backup
/// </summary>
    internal class StateLog
    {
        #region Properties
        public string Name { get; set; }
        public string SourceFilePath { get; set; }
        public string TargetFilePath { get; set; }
        public string LogState { get; set; }
        public int TotalFilesToCopy { get; set; }
        public int TotalFileLeft { get; set; }
        public int NbFileTodo { get; set; }
        public int Progression { get; set; }
        public readonly string LOGDESTPATH = "C:/EasySaveLog/state.";

        public readonly string DESTPATH = Path.Combine(Path.GetPathRoot(AppDomain.CurrentDomain.BaseDirectory), "/EasySaveLog/state.json");
        #endregion Properties

        #region Constructors

        public StateLog(string name, string sourcefilepath, string targetfilepath, string state, int totalfilestocopy, int totalfilesleft, int nbFilesToDo, int progression)
        {
            Name = name;
            SourceFilePath = sourcefilepath;
            TargetFilePath = targetfilepath;
            LogState = state;
            TotalFilesToCopy = totalfilestocopy;
            TotalFileLeft = totalfilesleft;
            NbFileTodo = nbFilesToDo;
            Progression = progression;
        }

        public StateLog()
        {

        }
        #endregion Constructors

        #region Methods

        public void PrepareWriteLogJSON(JArray DataForLog)
        {

            if (!Directory.Exists(Path.Combine(Path.GetPathRoot(AppDomain.CurrentDomain.BaseDirectory), "/EasySaveLog/")))
            {
                Directory.CreateDirectory(Path.Combine(Path.GetPathRoot(AppDomain.CurrentDomain.BaseDirectory), "/EasySaveLog/"));
            }
            else
            {
                if (!File.Exists("C:/EasySaveLog/state.json"))
                {
                    File.Create("C:/EasySaveLog/state.json");
                }
            }

            File.WriteAllText(DESTPATH, DataForLog.ToString());

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
                if (!File.Exists("C:/EasySaveLog/state.XML"))
                {
                    File.Create("C:/EasySaveLog/state.XML");
                }
            }
            FileStream fileStream = File.Open(LOGDESTPATH + "xml", FileMode.Open, FileAccess.Write);

            // Crée un flux d'écriture pour écrire dans le fichier
            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Indent = true;
            XmlWriter xmlWriter = XmlWriter.Create(fileStream, settings);

            DataForLog.Save(xmlWriter);

            // Ferme le flux d'écriture et le fichier
            xmlWriter.Close();
            fileStream.Close();

        }

        public void GenerateLogEtatJSON()
        {

            JObject DataForLog = new JObject(new JProperty("Name", this.Name), new JProperty("SourceFilePath", this.SourceFilePath), new JProperty("TargetFilePath", this.TargetFilePath), new JProperty("State", this.LogState), new JProperty("TotalFilesToCopy", this.TotalFilesToCopy), new JProperty("TotalFileLeft", this.TotalFileLeft), new JProperty("NbFilesLeftToDo", this.NbFileTodo), new JProperty("Progression", this.Progression));
            JArray array = new JArray(DataForLog);
            PrepareWriteLogJSON(array);
        }

        public void GenerateLogEtatXML(XmlDocument DataforLog)
        {
            XmlDocument document = new XmlDocument();

            // Créer le noeud racine
            XmlElement root = document.CreateElement("DailyLog");
            document.AppendChild(root);

            // Créer le noeud DataForLog dans un document XML différent
            XmlDocument tempDoc = new XmlDocument();
            XmlElement DataForLog = tempDoc.CreateElement("DailyLog");
            DataForLog.AppendChild(tempDoc.CreateElement("Name")).InnerText = this.Name;
            DataForLog.AppendChild(tempDoc.CreateElement("SourceFilePath")).InnerText = this.SourceFilePath;
            DataForLog.AppendChild(tempDoc.CreateElement("TargetFilePath")).InnerText = this.TargetFilePath;
            DataForLog.AppendChild(tempDoc.CreateElement("LogState")).InnerText = this.LogState;
            DataForLog.AppendChild(tempDoc.CreateElement("TotalFilesToCopy")).InnerText = this.TotalFilesToCopy.ToString();
            DataForLog.AppendChild(tempDoc.CreateElement("TotalFileLeft")).InnerText = this.TotalFileLeft.ToString();
            DataForLog.AppendChild(tempDoc.CreateElement("NbFileTodo")).InnerText = this.NbFileTodo.ToString();
            DataForLog.AppendChild(tempDoc.CreateElement("Progression")).InnerText = this.Progression.ToString();

            // Importer le noeud DataForLog dans le document XML principal
            XmlNode importedNode = document.ImportNode(DataForLog, true);
            root.AppendChild(importedNode);

            PrepareWriteLogXML(document);

        }

        public int FileLeft(string destination)
        {
            int nbFichiers = Directory.GetFiles(destination, "*.*", SearchOption.AllDirectories).Length;
            TotalFilesToCopy = nbFichiers;
            TotalFileLeft = nbFichiers - 1;
            return nbFichiers;
        }

        #endregion Methods
    }

}
