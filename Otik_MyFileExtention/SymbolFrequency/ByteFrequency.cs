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
            foreach (char b in FriquencyController.FileInfo)
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
