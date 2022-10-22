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
            if (content.Length < 1)
            {
                FileCollectorController.Header.Arhive = 0;
                return content;
            }

            switch (FileCollectorController.Header.Arhive)
            {
                case 1:
                    {
                        byte[] header;
                        byte[] data;
                        byte[] candidat;



                        Haffman.HaffmanLogiс haffman = new Haffman.HaffmanLogiс(content);
                        
                        data = haffman.GetCompressData(content);                       
                        header = haffman.GetHaffmanHeader();

                        FileCollectorController.Header.StartInfoByte += header.Length;
                        FileCollectorController.Header.LengthInfo += header.Length;

                        candidat = header.Concat(data).ToArray();

                        if (candidat.Length < content.Length)
                        {
                            content = candidat;
                        }
                        else
                        {
                            FileCollectorController.Header.Arhive = 0;
                        }
                        break;
                    }
            }


            return content;
        }
    }
}
