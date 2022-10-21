using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Otik_MyFileExtention.FileCollector
{
    internal class WriteTask : IFileTask
    {
        const int WriteBlock = 4096;

        public byte[] Task(byte[] content)
        {
            FileStream fileStreamInput = File.OpenWrite(Encode.Instence.ArchivePath);
            fileStreamInput.Position = fileStreamInput.Length;
            byte[] buffer;
            int bytesRead = 0;
            int i = 0;

            Console.WriteLine("Write Task " + content.Length);
            //Записываем содержимое
            while (i < content.Length)
            {      
                buffer = content.Skip(bytesRead).Take(content.Length % WriteBlock).ToArray();
                fileStreamInput.Write(buffer, 0, buffer.Length);
                bytesRead += buffer.Length;

                i += buffer.Length;
            }

            //Записываем конец файла
            buffer = Encoding.UTF8.GetBytes("\n}");
            fileStreamInput.Write(buffer, 0, buffer.Length);


            fileStreamInput.Close();

            byte[] result = { 1 };
            return result ;
        }
    }
}
