using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Web;

namespace ProjectPux.DirectoryManager
{
    [Serializable]
    public class Directory
    {
        public string RelativePath { get; set; }
        public string AbsolutePath { get; set; }
        public DirectoryInfo DirectoryInfo { get; set; }
        public List<File> OldFiles = new List<File>();
        public List<File> CurrentFiles = new List<File>();
   

        public Directory(string path)
        {
            this.RelativePath = path;
            SetCurrentFilesList();
        }

        public void SetCurrentFilesList()
        {
            CurrentFiles = new List<File>();
            var files = System.IO.Directory.EnumerateFiles(HttpContext.Current.Server.MapPath(RelativePath), "*.*", System.IO.SearchOption.AllDirectories);
         
            foreach (var file in files)
            {
                FileInfo info = new FileInfo(file);
                CurrentFiles.Add(new File(info));
            }
   
        }

        public List<File> GetFilesWithAction()
        {
            List<File> allFiles = new List<File>();
            GetAddedFiles(ref allFiles);
            GetRemovedFiles(ref allFiles);
            GetUpdatedFiles(ref allFiles);
            
            return allFiles;
        }

        private void GetAddedFiles(ref List<File> list)
        {
            list = list.Concat(CurrentFiles.Except(OldFiles, new FileComparer())).ToList();
            list.ForEach(file => file.CurrentFileState =File.FileState.Added);
        }
        private void GetRemovedFiles(ref List<File> list)
        {
           list = list.Concat(OldFiles.Except(CurrentFiles, new FileComparer())).ToList();
           list.ForEach(file => file.CurrentFileState = File.FileState.Removed);
        }
        private void GetUpdatedFiles(ref List<File> list)
        {
            File changedFile;
            foreach (File file in OldFiles)
            {
                if ((changedFile = CurrentFiles.Where(x => x.Name == file.Name).FirstOrDefault()) != null)
                {
                    if (changedFile.LastUpdated > file.LastUpdated)
                    {
                        changedFile.Version++;
                        changedFile.CurrentFileState = File.FileState.Updated;
                        list.Add(changedFile);
                    }
                }
            }
           
        }

    }
}