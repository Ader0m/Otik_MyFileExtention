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
            FileStream fs = File.OpenWrite(Encode.Instence.ArchivePath);
            byte[] toWrite = FileCollector.Header.ToWrite();


            fs.Position = fs.Length;
            fs.Write(toWrite, 0, toWrite.Length);

            fs.Close();


            return none;
        }
    }
}
