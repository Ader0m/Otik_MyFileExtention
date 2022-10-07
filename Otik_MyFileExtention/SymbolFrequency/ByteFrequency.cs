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
            foreach (byte b in SymbolDialog.ByteMass)
            {
                int count;

                if(SymbolDialog.FrequencyDict.TryGetValue((char)b, out count))
                {
                    SymbolDialog.FrequencyDict[(char)b] = 1;
                }
                SymbolDialog.FrequencyDict[(char)b] = count++;
            }
        }
    }
}
