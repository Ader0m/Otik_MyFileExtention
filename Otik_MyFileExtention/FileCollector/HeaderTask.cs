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
            byte[] toWrite = FileCollectorController.Header.ToWrite();

            return toWrite.Concat(content).ToArray();
        }
    }
}
