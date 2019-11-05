using DirectComparator.Commands;
using DirectComparator.Models;
using DirectComparator.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace DirectComparator.ViewModels
{
    class MainViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<CompareFileInfo> foundFiles;
        private BusinessCompareFileInfo businessCompareFileInfo;
        private bool isFileOnlyFirstDir;
        private bool isFileOnlySecondDir;
        private bool isFileBothDirSameSize;
        private bool isBothDirDiffSize;

        public ObservableCollection<CompareFileInfo> FoundFiles
        {
            get { return foundFiles; }
            set { foundFiles = value; OnPropertyChanged("FoundFiles"); }
        }
        public bool IsFileOnlyFirstDir
        {
            get { return isFileOnlyFirstDir; }
            set
            {
                isFileOnlyFirstDir = value;
                UpdateListFiles();
            }
        }
       
        public bool IsFileOnlySecondDir
        {
            get { return isFileOnlySecondDir; }
            set
            {
                isFileOnlySecondDir = value;
                UpdateListFiles();
            }
        }
        
        public bool IsFileBothDirSameSize
        {
            get { return isFileBothDirSameSize; }
            set
            {
                isFileBothDirSameSize = value;
                UpdateListFiles();
            }
        }
        public bool IsBothDirDiffSize
        {
            get { return isBothDirDiffSize; }
            set
            {
                isBothDirDiffSize = value;
                UpdateListFiles();
            }
        }

      
        public BusinessCompareFileInfo BusinessCompareFileInfo
        {
            get { return businessCompareFileInfo; }
            set
            {
                businessCompareFileInfo = value;
                UpdateListFiles();
            }
        }

        public MyCommand CompareDir
        {
            get
            {
                return new MyCommand(obj =>
                {
                    UpdateListFiles();
                }
                );
            }
        }


        public MainViewModel()
        {
            foundFiles = new ObservableCollection<CompareFileInfo>();
            isFileOnlyFirstDir = true;
            isBothDirDiffSize = true;
            isFileBothDirSameSize = true;
            isFileOnlySecondDir = true;
            businessCompareFileInfo = new BusinessCompareFileInfo();
        }

        private void UpdateListFiles()
        {
            try
            {
                FoundFiles = new ObservableCollection<CompareFileInfo>();
                if (IsFileOnlyFirstDir)
                    FoundFiles = new ObservableCollection<CompareFileInfo>(FoundFiles.Union(BusinessCompareFileInfo.GetFileOnlyInFirstDir()));
                if (IsFileOnlySecondDir)
                    FoundFiles = new ObservableCollection<CompareFileInfo>(FoundFiles.Union(BusinessCompareFileInfo.GetFileOnlyInSecondDir()));
                if (IsFileBothDirSameSize)
                    FoundFiles = new ObservableCollection<CompareFileInfo>(FoundFiles.Union(BusinessCompareFileInfo.GetFileInBothDirSameSize()));
                if (IsBothDirDiffSize)
                    FoundFiles = new ObservableCollection<CompareFileInfo>(FoundFiles.Union(BusinessCompareFileInfo.GetFileInBothDirDiffSize()));
            }
            catch (DirectoryNotFoundException e)
            {
                MessageBox.Show("Not found directory: " + e.Message, "Exception ", MessageBoxButton.OK, MessageBoxImage.Warning);

            }
            catch (ArgumentNullException e)
            {
                MessageBox.Show("Not found directory: " + e.ParamName, "Exception ", MessageBoxButton.OK, MessageBoxImage.Warning);

            }

        }
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
