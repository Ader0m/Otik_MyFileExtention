using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Otik_MyFileExtention.FileCollector
{
    internal class FileCollectorController : IFileTask
    {
        private static List<IFileTask> _tasks;
        public static Storage.IvaExtentionHeader Header;
        public static string FilePath;

        //0 - file; 1 - folder
        public FileCollectorController(int mod)
        {
            _tasks = new List<IFileTask>();
            Header = new Storage.IvaExtentionHeader();

            switch (mod) {
                case 0:
                    {
                        _tasks.Add(new HeaderTask()); //0
                        _tasks.Add(new ContentTask()); //1
                        _tasks.Add(new ArchiveTask()); //2
                        _tasks.Add(new NoiseProtectTask()); //3
                        _tasks.Add(new WriteTask()); //4
                        break;
                    }
                case 1:
                    {
                        _tasks.Add(new HeaderTask());                       
                        _tasks.Add(new WriteTask());
                        break;
                    }         
            }     
        }

        public byte[] Task(byte[] none)
        {
            byte[] content = new byte[1];


            foreach (var task in _tasks)
            {
                content = task.Task(content);
            }

            Console.WriteLine("Конец процесса " + FilePath);


            return content;           
        }

        public void Clear()
        {
            _tasks.Clear();
            Header = new Storage.IvaExtentionHeader();
        }
    }
}
