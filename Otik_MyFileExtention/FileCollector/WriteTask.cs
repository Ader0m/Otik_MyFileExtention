using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Otik_MyFileExtention.FileCollector
{
    internal class WriteTask : IFileTask
    {
        public byte[] Task(byte[] content)
        {
            FileStream fileStreamInput = File.OpenWrite(Encode.Instence.ArchivePath);
            fileStreamInput.Position = fileStreamInput.Length;
            byte[] buffer;
            int bytesRead = 0;
            int i = 0;           


            //Записываем содержимое
            while (i < (content.Length / 1024) + 1)
            {           
                buffer = content.Skip(bytesRead).Take(content.Length % 1024).ToArray();

                foreach (byte b in buffer)
                    Console.WriteLine(b);

                fileStreamInput.Write(buffer, 0, buffer.Length);
                bytesRead += buffer.Length;
                i++;
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
