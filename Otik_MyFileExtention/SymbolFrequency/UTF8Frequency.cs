using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Otik_MyFileExtention.SymbolFrequency
{
    internal class UTF8Frequency : ISymbolTask
    {
        public void Task()
        {
            char[] chars;
            chars = Encoding.UTF8.GetChars(Encoding.UTF8.GetBytes(FriquencyController.FileInfo));

            foreach (char ch in chars)
            {
                int count;

                if (FriquencyController.FrequencyDict.TryGetValue(ch, out count))
                {
                    FriquencyController.FrequencyDict[ch] = ++count;                    
                }
                else
                {
                    FriquencyController.FrequencyDict[ch] = 1;
                }                
            }
        }
    }
}
