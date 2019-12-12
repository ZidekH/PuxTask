using System;
using System.IO;

namespace ProjectPux.DirectoryManager
{
   [Serializable]
    public class File
    {
        public FileState CurrentFileState { get; set; }
        public string Name { get; set; }
        public int Version { get; set; }
        public DateTime CreationTime { get; set; }
        public DateTime LastUpdatedTime { get; set; }
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
            this.CurrentFileState = FileState.Mapped;
            this.Name = info.Name;
            this.Version = 1;
            this.CreationTime = info.CreationTime;
            this.LastUpdatedTime = info.LastWriteTime;
            this.FileInfo = info;
        }
      
    }
}