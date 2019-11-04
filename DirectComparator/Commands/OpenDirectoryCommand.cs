using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;
using DirectComparator.ViewModels;

namespace DirectComparator.Commands
{
    class OpenDirectoryCommand : MyCommand
    {
        public OpenDirectoryCommand(Action<object> execute, Func<object, bool> canExecute = null) : base(execute, canExecute)
        {
        }

        //public OpenDirectoryCommand(DataManager _dm) : base(_dm)
        //{
        //}

        //public event EventHandler CanExecuteChanged;

        //public override bool CanExecute(object parameter)
        //{
        //    throw new NotImplementedException();
        //}


        //public override void Execute(object parameter)
        //{
        //    (parameter as System.Windows.Controls.TextBox).Text = "hello";
        //    //using (var dialog = new FolderBrowserDialog())
        //    //{
        //    //    if (dialog.ShowDialog() == DialogResult.OK)
        //    //    {
        //    //        (parameter as TextBox).Text= dialog.SelectedPath;


        //    //    }

        //    //}
        //}
    }
}
