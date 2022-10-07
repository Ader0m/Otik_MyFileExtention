using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Otik_MyFileExtention.FileCollector
{
    internal class ArchiveTask : IFileTask
    {
        public byte[] Task(byte[] content)
        {
            return content;
        }
    }
}
