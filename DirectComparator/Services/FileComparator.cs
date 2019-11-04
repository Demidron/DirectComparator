using DirectComparator.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DirectComparator.Services
{
    public static class FileComparator
    {
        public static FileInfo[] GetDirFiles(string PathDir)
        {
            var dir = new DirectoryInfo(PathDir);

            FileInfo[] files = dir.GetFiles();
            return files;
           
        }
        public static FileInfo[] GetFileOnlyInFirstDir(List<FileInfo> l1, List<FileInfo> l2)
        {
            
            var fs = l1.Where(x => !l2.Exists(y => y.Name == x.Name)).ToArray();
            return fs;
        }

        public static FileInfo[] GetFileInBothDirSameSize (List<FileInfo> l1, List<FileInfo> l2)
        {
            var fs = l1.Where(x => l2.Exists(y => y.Name == x.Name && y.Length == x.Length)).ToArray();
            return fs;
        }

        public static FileInfo[] GetFileInBothDirDiffSize(List<FileInfo> l1, List<FileInfo> l2)
        {
            var fs = l1.Where(x => l2.Exists(y => y.Name == x.Name && y.Length != x.Length)).ToArray();
            return fs;
        } 
        public static IEnumerable<CompareFileInfo> CompareFileMass(string PathDir1, string PathDir2)
        {
            var l1 = GetDirFiles(PathDir1).ToList();
            var l2 = GetDirFiles(PathDir2).ToList();
            var res = GetFileOnlyInFirstDir(l1, l2).Select(z => new CompareFileInfo()
            {
                FileName = z.Name,
                SizeInBytes = z.Length,
                LastChangeDate = z.LastWriteTime,
                StatusFile = StatusFile.FileInFirstDir
            }).ToList();

            res = res.Union(GetFileOnlyInFirstDir(l2, l1).Select(z => new CompareFileInfo()
            {
                FileName = z.Name,
                SizeInBytes = z.Length,
                LastChangeDate = z.LastWriteTime,
                StatusFile = StatusFile.FileInSecondDir
            })).ToList();

            res = res.Union(GetFileInBothDirSameSize(l1, l2).Select(z => new CompareFileInfo()
            {
                FileName = z.Name,
                SizeInBytes = z.Length,
                LastChangeDate = z.LastWriteTime,
                StatusFile = StatusFile.FileInBothDirSameSize
            })).ToList();

            res = res.Union(GetFileInBothDirDiffSize(l1, l2).Select(z => new CompareFileInfo()
            {
                FileName = z.Name,
                SizeInBytes = z.Length,
                LastChangeDate = z.LastWriteTime,
                StatusFile = StatusFile.FileInBothDirDiffSize
            })).ToList();

            return res;
        }
    }
}
