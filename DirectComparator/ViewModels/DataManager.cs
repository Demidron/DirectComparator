using DirectComparator.Commands;
using DirectComparator.Models;
using DirectComparator.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace DirectComparator.ViewModels
{
    class DataManager : INotifyPropertyChanged
    {
        private string path1;
        private string path2;
       
        DialogService dialogService;
        private ObservableCollection<CompareFileInfo> foundFiles;
        public ObservableCollection <CompareFileInfo> FoundFiles {
            get { return foundFiles; }
            set { foundFiles = value; OnPropertyChanged("FoundFiles"); }
        }
        public string Path1
        {
            get { return path1; }
            set {path1 = value; OnPropertyChanged("Path1"); }
        }
        public string Path2
        {
            get { return path2; }
            set { path2 = value; OnPropertyChanged("Path2"); }
        }
       


        public MyCommand OpenDirectory1
        {

            get
            {
                return new MyCommand(obj =>
                  {
                      if(dialogService.OpenDirectory()==true)
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
        public MyCommand CompareDir
        {
            get
            {
                return new MyCommand(obj =>
                {
                    FoundFiles=new ObservableCollection<CompareFileInfo>(
                        FileComparator.CompareFileMass(path1, path2));
                }
                );
            }
        }
        public DataManager()
        {
            dialogService = new DialogService();
            FoundFiles = new ObservableCollection<CompareFileInfo>() {
                new CompareFileInfo()
                {
                    FileName="Test",
                    SizeInBytes=125855,
                    LastChangeDate = DateTime.Now,
                    StatusFile = StatusFile.FileInFirstDir

                }
            };
        }
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
