using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Otik_MyFileExtention
{
    internal class ContentTask: IFileTask
    {
        public byte[] Task(byte[] content)
        {
            FileStream fileStreamInput = File.OpenRead(FileCollector.FilePath);
            content = new byte[fileStreamInput.Length];

            fileStreamInput.Read(content);           

            return content;
        }
    }
}
