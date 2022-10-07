using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Otik_MyFileExtention.SymbolFrequency
{
    internal class SymbolDialog
    {
        private ISymbolTask _task;

        public SymbolDialog(ISymbolTask task)
        {
            _task = task;
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
