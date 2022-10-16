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
            char[] content = Encoding.UTF8.GetChars(FriquencyController.FileInfo);

            foreach (char b in content)
            {
                int count;
                
                if (FriquencyController.FrequencyDict.TryGetValue(b, out count))
                { 
                    FriquencyController.FrequencyDict[b] = ++count;                   
                }
                else
                {
                    FriquencyController.FrequencyDict[b] = 1;
                }
            }
        }
    }
}
