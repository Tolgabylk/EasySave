using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;

namespace EasySave_v1._0._0
{
    internal class StateLog
    {
        #region Properties
        public string Name { get; set; }
        public string SourceFilePath { get; set; }
        public string TargetFilePath { get; set; }        
        public State LogState { get; set; }
        public int TotalFilesToCopy { get; set; }
        public int TotalFileLeft { get; set; }
        public int NbFileTodo { get; set; }
        public int Progression { get; set; }

        public readonly string DESTPATH = Path.Combine(Path.GetPathRoot(AppDomain.CurrentDomain.BaseDirectory), "/EasySaveLog/state.json");
        #endregion Properties

        #region Constructors

        public StateLog(string name, string sourcefilepath,string targetfilepath, State state, int totalfilestocopy , int totalfilesleft, int nbFilesToDo, int progression)
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

        public void PrepareWriteLog(JArray DataForLog,EXTENSION extension)
        {
            if (extension == EXTENSION.JSON)
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

                using StreamWriter file = File.CreateText(DESTPATH);
                using (JsonTextWriter writer = new JsonTextWriter(file))
                {
                    DataForLog.WriteTo(writer);
                }
            }else if (extension == EXTENSION.JSON)
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

                using StreamWriter file = File.CreateText(DESTPATH);
                using (XmlTextWriter writer = new XmlTextWriter(file))
                {
                    DataForLog.WriteTo(writer);
                }
            }
        }

        public void GenerateLogEtat()
        {

            JObject DataForLog = new JObject(new JProperty("Name", this.Name), new JProperty("SourceFilePath", this.SourceFilePath), new JProperty("TargetFilePath", this.TargetFilePath), new JProperty("State", this.State), new JProperty("TotalFilesToCopy", this.TotalFilesToCopy), new JProperty("TotalFilesSize", this.TotalFilesSize), new JProperty("NbFilesLeftToDo", this.NbFilesLeftToDo), new JProperty("Progression", this.Progression));
            JArray array = new JArray(DataForLog);
            PrepareWriteLog(array);
        }

        #endregion Methods
    }

    public enum State { ACTIVE, END, NOTSTARTED }
    public enum EXTENSION { JSON, XML}
}
