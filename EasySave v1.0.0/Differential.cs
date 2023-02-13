using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace EasySave_v1._0._0
{
    internal class Differential : WorkSave
    {
        public Differential(string Name, string BaseDirectory, string TargetDirectory, bool Type) : base(Name, BaseDirectory, TargetDirectory, Type)
        {
        }

        public void DifferentialSave(string baseDirectory, string targetDirectory)
        {
            CopyDifferential(new DirectoryInfo(baseDirectory), new DirectoryInfo(targetDirectory));
        }

        public void CopyDifferential(DirectoryInfo sourcePath, DirectoryInfo destinationPath)
        {
            Directory.CreateDirectory(destinationPath.FullName);
            foreach (FileInfo dirFile in sourcePath.GetFiles())
            {
                if (dirFile.LastWriteTime != File.GetLastWriteTime(Path.Combine(destinationPath.FullName, dirFile.Name)) || !File.Exists(Path.Combine(destinationPath.FullName, dirFile.Name)))
                {
                    dirFile.CopyTo(Path.Combine(destinationPath.FullName, dirFile.Name), true);
                }
            }
            foreach (DirectoryInfo srcSubDirs in sourcePath.GetDirectories())
            {
                if (Directory.Exists(Path.Combine(destinationPath.FullName, srcSubDirs.Name)))
                {
                    DirectoryInfo nextDestSubDir = new DirectoryInfo(Path.Combine(destinationPath.FullName, srcSubDirs.Name));
                    CopyDifferential(srcSubDirs, nextDestSubDir);
                }
                else
                {
                    DirectoryInfo nextDestSubDir = destinationPath.CreateSubdirectory(srcSubDirs.Name);
                    CopyDifferential(srcSubDirs, nextDestSubDir);
                }
            }
        }

    }

}