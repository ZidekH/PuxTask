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
                ViewState["Data"] = directoryStructure;
            }
            else
                directoryStructure = (DirectoryStructure)ViewState["Data"];

            InfoLabel.Visible = false;
            FilesGridView.Visible = true;
        }

        protected void GetChanges_Click(object sender, EventArgs e)
        {
            string absolutePath = HttpContext.Current.Server.MapPath(PathTextBox.Text);
            if (System.IO.Directory.Exists(absolutePath))
            {
                DirectoryManager.Directory currentDirectory = directoryStructure?.GetDirectory(PathTextBox.Text);
                if (currentDirectory != null)
                {
                    currentDirectory.SetCurrentFilesList();
                    List<DirectoryManager.File> filesAWithAction = currentDirectory.GetFilesWithAction();
                    FilesGridView.DataSource = filesAWithAction;
                    FilesGridView.DataBind();
                    FilesGridView.Visible = true;
                }
                else
                {
                    currentDirectory = new DirectoryManager.Directory(PathTextBox.Text);
                    directoryStructure.AddDirectory(currentDirectory);

                    InfoLabel.Visible = true;
                    InfoLabel.Text = $"Byl namapován nový adresář: {absolutePath}";
                    FilesGridView.Visible = false;

                }
                currentDirectory.SetFileHistory();
                ViewState["Data"] = directoryStructure;
            }
            else
            {
                InfoLabel.Visible = true;
                InfoLabel.Text = $"Tento adresář nebyl nalezen: {absolutePath}";
            }
        }
    }
}