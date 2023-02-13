using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace EasySave_v1._0._0
{/// <summary>
/// Abtract class for the complete and the differential Save
/// </summary>
    abstract class WorkSave
    {
        #region Properties
        public string name { get; set; }
        public string baseDirectory { get; set; }
        public string targetDirectory { get; set; }
        public bool type { get; set; }

        public Log log { get; set; }

        #endregion Properties 

        #region Constructor

        public WorkSave(string Name, string BaseDirectory, string TargetDirectory, bool Type)
        {
            this.name = Name;
            this.baseDirectory = BaseDirectory;
            this.targetDirectory = TargetDirectory;
            this.type = Type;
            
        }

        public override string ToString()
        {
            return this.name + " " + this.baseDirectory;
        }

      /*  public int FileLeft(string destination)
        {
            int nbFichiers = Directory.GetFiles(destination, "*.*", SearchOption.AllDirectories).Length;
            log.TotalFilesToCopy = nbFichiers;
            TotalFileLeft = nbFichiers - 1;
            return nbFichiers;
        }*/

        #endregion Constructor 
    }
}
