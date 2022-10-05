using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Otik_MyFileExtention
{
    internal class HeaderTask : IFileTask
    {
        public byte[] Task(byte[] none)
        {
            File.AppendAllText(Encode.Instence.ArchivePath, "\n");
            File.AppendAllText(Encode.Instence.ArchivePath, FileCollector.Header.ToString()); 
            return none;
        }
    }
}
