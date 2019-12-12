using ProjectPux.DirectoryManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ProjectPux
{
       internal class FileComparer : IEqualityComparer<File>
       {
            public bool Equals(File file1, File file2)
            {
                if (string.Equals(file1.Name, file2.Name, StringComparison.OrdinalIgnoreCase))
                {
                    return true;
                }
                return false;
            }

            public int GetHashCode(File file)
            {
                return file.Name.GetHashCode();
            }
       }

    
}