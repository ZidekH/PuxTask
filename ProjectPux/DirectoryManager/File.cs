using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;

namespace ProjectPux.DirectoryManager
{
   [Serializable]
    public class File
    {
        public FileState CurrentFileState { get; set; }
        public string Name { get; set; }
        public int Version { get; set; }

        public DateTime LastUpdated { get; set; }
        public FileInfo FileInfo { get; set; }

      
        public enum FileState
        {
            Mapped,
            Added,
            Updated,
            Removed

        }
        public File(FileInfo info)
        {
            
            this.Name = info.Name;
            this.Version = 1;
            this.LastUpdated = info.LastWriteTime;
            this.FileInfo = info;
        }

      
    }
}