using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Otik_MyFileExtention.SymbolFrequency
{
    internal class SymbolDialog
    {
        public static Dictionary<char, int> FrequencyDict;
        public static string FileInfo;
        private ISymbolTask _task;
        

        public SymbolDialog(ISymbolTask task, string fileInfo)
        {
            _task = task;
            FileInfo = fileInfo;
            FrequencyDict = new Dictionary<char, int>();

            _task.Task();
        }

        public void SymbolDialogStart()
        {
            SymbolDialogMenu();
        }

        private void SymbolDialogMenu()
        {

        }
    }
}
