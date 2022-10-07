using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Otik_MyFileExtention.FileCollector
{
    internal class HeaderTask : IFileTask
    {
        public byte[] Task(byte[] none)
        {
            FileStream fs = File.OpenWrite(Encode.Instence.ArchivePath);
            byte[] toWrite = FileCollectorController.Header.ToWrite();


            fs.Position = fs.Length;
            fs.Write(toWrite, 0, toWrite.Length);

            fs.Close();


            return none;
        }
    }
}
