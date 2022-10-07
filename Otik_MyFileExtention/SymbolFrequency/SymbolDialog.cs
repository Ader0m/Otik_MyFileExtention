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
            Console.Clear();
            Console.WriteLine("*-------------------SymbolMenu-------------------*");
            Console.WriteLine("\tАктивный файл", Storage.NameFile);
            Console.WriteLine();
            Console.WriteLine("\t1. Введите имя файла");
            Console.WriteLine("\t2. Сортировать по алфавиту");
            Console.WriteLine("\t3. Сортировать частоты по убыванию");
            Console.WriteLine("\t0. Выход");
        }
        public void sortKey()
        {
            FrequencyDict = FrequencyDict.OrderBy(x => x.Key).ToDictionary(x => x.Key, x => x.Value);
            foreach (var element in FrequencyDict)
            {
                Console.WriteLine($"key: {element.Key}  value: {element.Value}");
            }
        }
        public void sortValue()
        {
            FrequencyDict = FrequencyDict.OrderBy(x => x.Value).ToDictionary(x => x.Key, x => x.Value);
            foreach (var element in FrequencyDict)
            {
                Console.WriteLine($"key: {element.Key}  value: {element.Value}");
            }
        }
    }
}
