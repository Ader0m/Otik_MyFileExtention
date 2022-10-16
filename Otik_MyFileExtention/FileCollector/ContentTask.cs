using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Otik_MyFileExtention.FileCollector
{
    internal class ContentTask: IFileTask
    {
        public byte[] Task(byte[] content)
        {
            switch (FileCollectorController.Header.Arhive)
            {
                case 1:
                    {
                        FileStream fileStreamInput = File.OpenRead(FileCollectorController.FilePath);
                        content = new byte[fileStreamInput.Length];

                        fileStreamInput.Read(content);

                        fileStreamInput.Close();

                        break;
                    }
            }

            return content;
        }
    }
}
