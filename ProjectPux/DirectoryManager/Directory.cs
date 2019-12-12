using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace ProjectPux.DirectoryManager
{
    [Serializable]
    public class Directory
    {
        private List<File> _oldFiles = new List<File>();
        private List<File> _currentFiles = new List<File>();
        public string RelativePath { get; set; }

        public Directory(string path)
        {
            this.RelativePath = path;
            SetCurrentFilesList();
        }

        public void SetCurrentFilesList()
        {
            _currentFiles = new List<File>();
            var files = System.IO.Directory.EnumerateFiles(HttpContext.Current.Server.MapPath(RelativePath), "*.*", System.IO.SearchOption.AllDirectories);

            foreach (var file in files)
            {
                FileInfo info = new FileInfo(file);
                File existingFile = GetExistingFile(info);
                File newFile = new File(info);


                if (existingFile != null)
                    newFile.Version = existingFile.Version;

                _currentFiles.Add(newFile);
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
        public void SetFileHistory()
        {
            _oldFiles = _currentFiles;
            _currentFiles = new List<File>();
        }
        private File GetExistingFile(FileInfo fileInfo)
        {
            return _oldFiles.Where(oldfile => oldfile.Name == fileInfo.Name && oldfile.CreationTime == fileInfo.CreationTime).FirstOrDefault();
         
        }
        private void GetAddedFiles(ref List<File> list)
        {
            List<File> addedFiles = _currentFiles.Except(_oldFiles, new FileComparer()).ToList();
            addedFiles.ForEach(file => file.CurrentFileState = File.FileState.Added);

            list = list.Concat(addedFiles).ToList();
        }
        private void GetRemovedFiles(ref List<File> list)
        {
            List<File> removedFiles = _oldFiles.Except(_currentFiles, new FileComparer()).ToList();
            removedFiles.ForEach(file => file.CurrentFileState = File.FileState.Removed);

            list = list.Concat(removedFiles).ToList();
     
        }
        private void GetUpdatedFiles(ref List<File> list)
        {
            File changedFile;
            foreach (File file in _oldFiles)
            {
                if ((changedFile = _currentFiles.Where(x => x.Name == file.Name && x.CreationTime == file.CreationTime).FirstOrDefault()) != null)
                {
                    if (changedFile.LastUpdatedTime > file.LastUpdatedTime)
                    {
                        _currentFiles.Where(x => x.Name == file.Name && x.CreationTime == file.CreationTime).First().Version = file.Version++;
                        changedFile.Version = file.Version++;
                        changedFile.CurrentFileState = File.FileState.Updated;
                        list.Add(changedFile);
                    }
                }
            }

        }

    }
}