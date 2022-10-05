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
            File.AppendAllText(Encode.Instence.ArchivePath, "\n{\n{\n"); // потом добавить в ToWrite
            FileStream fs = File.OpenWrite(Encode.Instence.ArchivePath);

            fs.Position = fs.Length;
            fs.Write(FileCollector.Header.ToWrite(), 0, FileCollector.Header.ToWrite().Length);

            fs.Close();


            return none;
        }
    }
}
