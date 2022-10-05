using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using static Otik_MyFileExtention.ContentTask;

namespace Otik_MyFileExtention
{
    internal class Decoder
    {
        #region Singleton

        public static Decoder? Instence { get { return _instance; } }
        private static Decoder? _instance;

        #endregion

        ContentTask contentTask = new ContentTask();

        public Decoder()
        {
            _instance = this;
        }

        public void Start()
        {
            byte[] file;
            Storage.IvaExtentionHeader h = new Storage.IvaExtentionHeader();
            FileStream fileStreamInput = File.OpenRead(Storage.NameFile);
            file = new byte[fileStreamInput.Length];

            fileStreamInput.Read(file);
            int i;
            for (i = 0; i < 4; i++)
                h.Signature[i] = file[i];
            Console.WriteLine(i);
            h.FileOrDirectory = Convert.ToBoolean(file[i]);
            h.Name = Encoding.UTF8.GetString(file,i+1,i+7);
            h.Version = BitConverter.ToInt32(file, 13);
            h.Arhive = BitConverter.ToInt32(file, 17);
            h.Protect = BitConverter.ToInt32(file, 17);


            Console.WriteLine("bool "+h.Name);
            string s = Encoding.UTF8.GetString(file);
            //s = "{\n{\na\nb\n}\n{\nsss\n}\n}";
            //Console.WriteLine(s);
            s = s.Substring(s.IndexOf("{") + 2, s.LastIndexOf('}') - (s.IndexOf("{") + 3));
            string header = s.Substring(s.IndexOf('{') + 2, s.IndexOf('}', s.IndexOf('{')) - (s.IndexOf('{') + 3));
            string[] heeeerd = header.Split("\n");

            h.Version = Convert.ToInt32(heeeerd[3]);
            h.Arhive = Convert.ToInt32(heeeerd[4]);
            h.Protect = Convert.ToInt32(heeeerd[5]);
            h.StartInfoByte = Convert.ToInt32(heeeerd[6]);

            s = s.Substring(s.IndexOf('}') + 2);
            string info = s.Substring(s.IndexOf('{') + 2, s.IndexOf('}', s.IndexOf('{')) - (s.IndexOf('{') + 3));
            Console.WriteLine("header\n" + header);
            Console.WriteLine("info\n" + info);


        }
    }
}
