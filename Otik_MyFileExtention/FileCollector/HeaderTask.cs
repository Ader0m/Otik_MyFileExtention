using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Otik_MyFileExtention.FileCollector
{
    internal class HeaderTask : IFileTask
    {
        public byte[] Task(byte[] content)
        {
            Console.WriteLine("Header Task " + content.Length);
            byte[] toWrite = FileCollectorController.Header.ToWrite(content.Length - FileCollectorController.Header.LengthInfo);

            return toWrite.Concat(content).ToArray();
        }
    }
}
