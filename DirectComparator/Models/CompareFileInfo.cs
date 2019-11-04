using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DirectComparator.Models
{
    public class CompareFileInfo
    {
        private long sizeInBytes;
        public String FileName { get; set; }


        public long SizeInBytes
        {
            get
            {
                return sizeInBytes;
            }
            set
            {
                sizeInBytes = value;
                SizeInKB = (int)value / 1024;
            }

        }
        public int SizeInKB { get; set; }
        public DateTime LastChangeDate { get; set; }
        public StatusFile StatusFile { get; set; }
    }
}
