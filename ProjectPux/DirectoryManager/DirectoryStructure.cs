using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectPux.DirectoryManager
{
    [Serializable]
    public class DirectoryStructure
    {
        public List<Directory> Directories = new List<Directory>();

        public Directory GetDirectory(string path)
        {
            return Directories.Where(dir => dir.RelativePath == path).FirstOrDefault();
        }

    }
}