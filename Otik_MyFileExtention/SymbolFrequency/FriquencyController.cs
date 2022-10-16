using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Otik_MyFileExtention.SymbolFrequency
{
    internal class FriquencyController
    {
        public static Dictionary<char, int> FrequencyDict;
        public static byte[] FileInfo;
        private ISymbolTask _task;

        public FriquencyController(ISymbolTask task, byte[]? fileinfo)
        {
            _task = task;
            if (fileinfo == null)
            {
                FileStream fileStreamInput = File.OpenRead(Storage.NameFile);
                FileInfo = new byte[fileStreamInput.Length];

                fileStreamInput.Read(FileInfo);

                fileStreamInput.Close();
            }
            else
            {
                FileInfo = fileinfo;
            }
            FrequencyDict = new Dictionary<char, int>();

            _task.Task();
        }

        public Dictionary<char, int> SortKey()
        {
            FrequencyDict = FrequencyDict.OrderBy(x => x.Key).ToDictionary(x => x.Key, x => x.Value);

            return FrequencyDict;
        }

        public Dictionary<char, int> SortValue()
        {
            FrequencyDict = FrequencyDict.OrderByDescending(x => x.Value).ToDictionary(x => x.Key, x => x.Value);

            return FrequencyDict;
        }

        public void Print()
        {
            if (_task is ByteFrequency)
            {
                foreach (var element in FrequencyDict)
                {
                    Console.WriteLine($"key: {Convert.ToInt32(element.Key)}  value: {element.Value}");
                }
            }
            else
            {
                foreach (var element in FrequencyDict)
                {
                    Console.WriteLine($"key: {element.Key}  value: {element.Value}");
                }
            }
        }
    }
}
