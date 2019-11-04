using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DirectComparator.Services
{
    public class DialogService
    {
        public string FilePath { get; set; }

        public bool OpenDirectory()
        {
            using (var dialog = new FolderBrowserDialog())
            {
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    FilePath = dialog.SelectedPath;
                    return true;
                }
                return false;
            }
        
        }

    }
}
