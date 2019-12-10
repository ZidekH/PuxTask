using ProjectPux.DirectoryManager;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ProjectPux
{
    public partial class _Default : Page
    {
        DirectoryStructure directoryStructure;

        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                directoryStructure = new DirectoryStructure();
                ViewState["test"] = directoryStructure;
               
            }
            else
            directoryStructure = (DirectoryStructure)ViewState["test"];

            InfoLabel.Visible = false;
        }

        protected void GetChanges_Click(object sender, EventArgs e)
        {
            string absolutePath = HttpContext.Current.Server.MapPath(PathTextBox.Text);
            if (System.IO.Directory.Exists(absolutePath))
            {
                DirectoryManager.Directory currentDir = directoryStructure?.GetDirectory(PathTextBox.Text);
                if (currentDir != null)
                {
                    currentDir.SetCurrentFilesList();
                    List<DirectoryManager.File> fileActionList = currentDir.GetFilesWithAction();
                   
                    FilesGridView.DataSource = fileActionList;
                    FilesGridView.DataBind();
                                     
                }
                else
                {
                    currentDir = new DirectoryManager.Directory(PathTextBox.Text);
                    directoryStructure.Directories.Add(currentDir);
                    InfoLabel.Visible = true;
                    InfoLabel.Text = $"Byl namapován nový adresář: {absolutePath}";

                }
                currentDir.OldFiles = currentDir.CurrentFiles;
                ViewState["test"] = directoryStructure;
            }
            else
            {
                InfoLabel.Visible = true;
                InfoLabel.Text = $"Tento adresář nebyl nalezen: {absolutePath}";
            }
        }
    }
}