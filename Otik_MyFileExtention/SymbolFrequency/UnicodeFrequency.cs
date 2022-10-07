using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Otik_MyFileExtention.SymbolFrequency
{
    internal class UnicodeFrequency : ISymbolTask
    {
        public void Task()
        {
            char[] chars;
            chars = Encoding.UTF8.GetChars(Encoding.UTF8.GetBytes(SymbolDialog.FileInfo));

            foreach (char ch in chars)
            {
                int count;

                if (SymbolDialog.FrequencyDict.TryGetValue(ch, out count))
                {
                    SymbolDialog.FrequencyDict[ch] = 1;
                }
                SymbolDialog.FrequencyDict[ch] = count++;
            }
        }
    }
}
