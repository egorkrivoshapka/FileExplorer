using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;

namespace FileExplorer.Models
{

    public class FileModel
    {
        public string Name { get; set; }
        public string FullName { get; set; }
        public long Lenght { get; set; }

        public FileModel(FileInfo info)
        {
            Name = info.Name;
            FullName = info.FullName;
            Lenght = info.Length;
        }
    }

    public class DirectoryModel
    {
        public string Root { get; set; }
        public string Current { get; set; }
        public List<string> Sub { get; set; }
        public List<FileModel> Files { get; set; }

        public DirectoryModel()
        {
            Sub = new List<string>();
            Files = new List<FileModel>();
        }
    }
}