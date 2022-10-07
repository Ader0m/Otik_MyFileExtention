using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Otik_MyFileExtention.SymbolFrequency
{
    internal class ByteFrequency : ISymbolTask
    {
        public void Task()
        {
            foreach (char b in SymbolDialog.FileInfo)
            {
                int count;

                if(SymbolDialog.FrequencyDict.TryGetValue(b, out count))
                {
                    SymbolDialog.FrequencyDict[b] = 1;
                }
                SymbolDialog.FrequencyDict[b] = count++;
            }
        }
    }
}
