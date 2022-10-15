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
            switch (FileCollectorController.Header.Arhive)
            {
                case 1:
                    {
                        byte[] header;
                        byte[] data;
                        

                        Haffman.HaffmanLogiс haffman = new Haffman.HaffmanLogiс(FileCollectorController.FilePath);
                        
                        data = haffman.GetCompressData(content);
                        header = haffman.GetHaffmanHeader();

                        FileCollectorController.Header.StartInfoByte += header.Length;

                        content = header.Concat(data).ToArray();

                        break;
                    }
            }


            return content;
        }
    }
}
