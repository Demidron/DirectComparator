using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DirectComparator.Services
{
    public class FileComparator
    {
        public  List<FileInfo> GetDirFiles(string PathDir)
        {
            var dir = new DirectoryInfo(PathDir);

            var files = dir.GetFiles().ToList();
            return files;
        }
        public  IEnumerable<FileInfo> GetUniqueFiles(List<FileInfo> fileList, List<FileInfo> compareWith)
        {
            return fileList.Where(x => !compareWith.Exists(y => y.Name == x.Name));
        }

        public  IEnumerable<FileInfo> GetFileDuplicatesSameSize(List<FileInfo> l1, List<FileInfo> l2)
        {
            return l1.Where(x => l2.Exists(y => y.Name == x.Name && y.Length == x.Length)); 
        }

        public  IEnumerable<FileInfo> GetFileDuplicatesDiffSize(List<FileInfo> l1, List<FileInfo> l2)
        {
            return l1.Where(x => l2.Exists(y => y.Name == x.Name && y.Length != x.Length));   
        } 
    }
}
