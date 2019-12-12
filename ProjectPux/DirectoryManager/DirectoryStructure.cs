using System;
using System.Collections.Generic;
using System.Linq;

namespace ProjectPux.DirectoryManager
{
    [Serializable]
    public class DirectoryStructure
    {
        private List<Directory> Directories = new List<Directory>();
        public Directory GetDirectory(string path)
        {
            return Directories.Where(dir => dir.RelativePath == path).FirstOrDefault();
        }

        public void AddDirectory(Directory directory)
        {
            Directories.Add(directory);
        }
    }
}