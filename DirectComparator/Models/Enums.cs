using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DirectComparator.Models
{
    public enum StatusFile
    {
        FileInFirstDir,
        FileInSecondDir,
        FileInBothDirSameSize,
        FileInBothDirDiffSize
    }
}
