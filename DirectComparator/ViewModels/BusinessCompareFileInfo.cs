using DirectComparator.Commands;
using DirectComparator.Models;
using DirectComparator.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace DirectComparator.ViewModels
{
    public class BusinessCompareFileInfo : INotifyPropertyChanged
    {
        private FileComparator fileComparator = new FileComparator();
        private string path1;
        private string path2;
        private DialogService dialogService;

        public BusinessCompareFileInfo()
        {
            this.dialogService = new DialogService();
        }

        public string Path1
        {
            get { return path1; }
            set { path1 = value; OnPropertyChanged("Path1"); }
        }
        public string Path2
        {
            get { return path2; }
            set { path2 = value; OnPropertyChanged("Path2"); MessageBox.Show("Srabotalo", "Exception ", MessageBoxButton.OK); }
        }
        public List<CompareFileInfo> GetFileOnlyInFirstDir()
        {
            
            var res = fileComparator.GetUniqueFiles(fileComparator.GetDirFiles(Path1), fileComparator.GetDirFiles(Path2)).Select(z => new CompareFileInfo()
            {
                FileName = z.Name,
                SizeInBytes = z.Length,
                LastChangeDate = z.LastWriteTime,
                StatusFile = StatusFile.FileInFirstDir
            }).ToList();

            return res;
        }
        public List<CompareFileInfo> GetFileOnlyInSecondDir()
        {
            var res = fileComparator.GetUniqueFiles(fileComparator.GetDirFiles(Path2), fileComparator.GetDirFiles(Path1)).Select(z => new CompareFileInfo()
            {
                FileName = z.Name,
                SizeInBytes = z.Length,
                LastChangeDate = z.LastWriteTime,
                StatusFile = StatusFile.FileInSecondDir
            }).ToList();
            return res;
        }
        public List<CompareFileInfo> GetFileInBothDirSameSize()
        {
            var res = fileComparator.GetFileDuplicatesSameSize(fileComparator.GetDirFiles(Path1), fileComparator.GetDirFiles(Path2))
                .Select(z => new CompareFileInfo()
                {
                    FileName = z.Name,
                    SizeInBytes = z.Length,
                    LastChangeDate = z.LastWriteTime,
                    StatusFile = StatusFile.FileInBothDirSameSize
                }).ToList();
            return res;
        }
        public List<CompareFileInfo> GetFileInBothDirDiffSize()
        {
            var res = fileComparator.GetFileDuplicatesDiffSize(fileComparator.GetDirFiles(Path1), fileComparator.GetDirFiles(Path2)).Select(z => new CompareFileInfo()
            {
                FileName = z.Name,
                SizeInBytes = z.Length,
                LastChangeDate = z.LastWriteTime,
                StatusFile = StatusFile.FileInBothDirDiffSize
            }).ToList();

            return res;
        }

        public MyCommand OpenDirectory1
        {
            get
            {
                return new MyCommand(obj =>
                {
                    
                    if (dialogService.OpenDirectory() == true)
                    {
                        Path1 = dialogService.FilePath;
                    }
                }
                );
            }
        }
        public MyCommand OpenDirectory2
        {
            get
            {
                return new MyCommand(obj =>
                {
                    if (dialogService.OpenDirectory() == true)
                    {
                        Path2 = dialogService.FilePath;
                    }
                }
                );
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
