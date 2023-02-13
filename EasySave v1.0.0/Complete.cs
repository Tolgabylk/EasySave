using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Diagnostics;
using EasySave_v1._0._0;

namespace EasySave.Model
{
    class Complete : WorkSave
    {
        public Complete(string Name, string BaseDirectory, string TargetDirectory, bool Type) : base(Name, BaseDirectory, TargetDirectory, Type) { }

        public void completeSave(bool typeFichier)
        {
       
            //if type is false then it is file
            if (typeFichier == false)
            {

                Stopwatch stopwatch = new Stopwatch();
                int fileNumber = Directory.GetFiles(baseDirectory, "*.*", SearchOption.AllDirectories).Length;
                try
                {

                    foreach (string dir in Directory.GetDirectories(baseDirectory, "*", SearchOption.AllDirectories))
                    {
                        string dirToCreate = dir.Replace(baseDirectory, targetDirectory);
                        Directory.CreateDirectory(dirToCreate);
                    }

                    foreach (string newPath in Directory.GetFiles(baseDirectory, "*.*", SearchOption.AllDirectories))
                    {
                        File.Copy(newPath, newPath.Replace(baseDirectory, targetDirectory), true);
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
            else
            {
                try
                {
                    File.Copy(baseDirectory, targetDirectory + Path.GetFileName(baseDirectory));
                }
                catch (Exception i)
                {
                    Console.WriteLine(i.Message);
                }
            }

        }

        public long GetDirectorySize(string directoryPath)
        {
            long size = 0;

            DirectoryInfo directoryInfo = new DirectoryInfo(directoryPath);

            foreach (FileInfo file in directoryInfo.GetFiles("*.*,", SearchOption.AllDirectories))
            {
                size += file.Length;
            }
            return size;
        }

    }
}
