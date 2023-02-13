using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
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

        public readonly string DESTPATH = Path.Combine(Path.GetPathRoot(AppDomain.CurrentDomain.BaseDirectory), "/EasySaveLog/state.json");
        #endregion Properties

        #region Constructors

        public StateLog(string name, string sourcefilepath,string targetfilepath, string state, int totalfilestocopy , int totalfilesleft, int nbFilesToDo, int progression)
        {
            Name = name;
            SourceFilePath= sourcefilepath;
            TargetFilePath= targetfilepath;
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

        public void PrepareWriteLog(JArray DataForLog,string extension)
        {
            if (extension == EXTENSION.JSON.ToString())
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
            }else if (extension == EXTENSION.XML.ToString())
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

                File.WriteAllText(DESTPATH, DataForLog.ToString());

                StreamWriter file = File.CreateText(DESTPATH);
                XmlTextWriter writer = new XmlTextWriter(file);
                
                    //DataForLog.WriteTo(writer);
                
            }
        }

        public void GenerateLogEtatJSON()
        {

            JObject DataForLog = new JObject(new JProperty("Name", this.Name), new JProperty("SourceFilePath", this.SourceFilePath), new JProperty("TargetFilePath", this.TargetFilePath), new JProperty("State", this.LogState), new JProperty("TotalFilesToCopy", this.TotalFilesToCopy), new JProperty("TotalFileLeft", this.TotalFileLeft), new JProperty("NbFilesLeftToDo", this.NbFileTodo), new JProperty("Progression", this.Progression));
            JArray array = new JArray(DataForLog);
            PrepareWriteLog(array,EXTENSION.JSON.ToString());
        }

     /*   public void GenerateLogEtatXML()
        {

            List<Dictionary<string, object>> logData = new List<Dictionary<string, object>>();
            // Add data to the list of dictionaries representing each log entry

            // Create the root element
            XElement logElement = new XElement("Log");

            // Create a child element for each log entry
            foreach (Dictionary<string, object> entry in logData)
            {
                XElement copyJobElement = new XElement("CopyJob");
                copyJobElement.Add(new XElement("Name", entry["Name"]));
                copyJobElement.Add(new XElement("SourceFilePath", entry["SourceFilePath"]));
                copyJobElement.Add(new XElement("TargetFilePath", entry["TargetFilePath"]));
                copyJobElement.Add(new XElement("State", entry["State"]));
                copyJobElement.Add(new XElement("TotalFilesToCopy", entry["TotalFilesToCopy"]));
                copyJobElement.Add(new XElement("TotalFileLeft", entry["TotalFileLeft"]));
                copyJobElement.Add(new XElement("NbFilesLeftToDo", entry["NbFilesLeftToDo"]));
                copyJobElement.Add(new XElement("Progression", entry["Progression"]));
                logElement.Add(copyJobElement);
            }
        }
*/
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
