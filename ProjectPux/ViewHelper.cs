using ProjectPux.DirectoryManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ProjectPux
{
    public static class ViewHelper
    {
        public static void RenderTable(List<File> listOfFiles, Table table)
        {
            foreach (File file in listOfFiles)
            {
                TableRow row = new TableRow();
                TableCell cell1 = new TableCell
                {
                    Text = file.Name
                };

                TableCell cell2 = new TableCell
                {
                    Text = Convert.ToString(file.Version)
                };

                row.Cells.Add(cell1);
                row.Cells.Add(cell2);
                table.Rows.Add(row);
            }
        }
    }
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