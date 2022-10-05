using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Otik_MyFileExtention
{
    internal interface IFileTask
    {
        public byte[] Task(byte[] content);
    }
}
