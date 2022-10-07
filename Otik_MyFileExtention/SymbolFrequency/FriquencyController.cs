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
        public static string FileInfo;
        private ISymbolTask _task;

        public FriquencyController(ISymbolTask task)
        {
            _task = task;
            FileInfo = File.ReadAllText(Storage.NameFile);
            Console.WriteLine(FileInfo);
            FrequencyDict = new Dictionary<char, int>();

            _task.Task();
        }

        public void sortKey()
        {
            FriquencyController.FrequencyDict = FriquencyController.FrequencyDict.OrderBy(x => x.Key).ToDictionary(x => x.Key, x => x.Value);
            if (_task is ByteFrequency)
            {
                foreach (var element in FriquencyController.FrequencyDict)
                {
                    Console.WriteLine($"key: {Convert.ToInt32(element.Key)}  value: {element.Value}");
                }
            }
            else
            {
                foreach (var element in FriquencyController.FrequencyDict)
                {
                    Console.WriteLine($"key: {element.Key}  value: {element.Value}");
                }
            }
        }

        public void sortValue()
        {
            if (_task is ByteFrequency)
            {
                FriquencyController.FrequencyDict = FriquencyController.FrequencyDict.OrderByDescending(x => x.Value).ToDictionary(x => x.Key, x => x.Value);
                foreach (var element in FriquencyController.FrequencyDict)
                {
                    Console.WriteLine($"key: {Convert.ToInt32(element.Key)}  value: {element.Value}");
                }
            }
            else
            {
                foreach (var element in FriquencyController.FrequencyDict)
                {
                    Console.WriteLine($"key: {element.Key}  value: {element.Value}");
                }
            }
        }
    }
}
