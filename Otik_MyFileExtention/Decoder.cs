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
            int byt = 3;
            for (int i = byt; i < (byt+4); i++)
                h.Signature[i-byt] = file[i];
            byt += 5;
            Console.WriteLine(byt);
            h.FileOrDirectory = Convert.ToBoolean(file[byt]);
            byt += 2;
            h.Name = Encoding.UTF8.GetString(file,byt,byt+7);
            byt += 8;
            h.Version = BitConverter.ToInt32(file, byt);
            byt += 5;
            h.Arhive = BitConverter.ToInt32(file, byt);
            byt += 5;
            h.Protect = BitConverter.ToInt32(file, byt);

            Console.WriteLine("Sygnature " + Encoding.UTF8.GetString(h.Signature));
            Console.WriteLine("bool "+h.FileOrDirectory);
            Console.WriteLine("Name " + h.Name);
            Console.WriteLine("Version " + h.Version);
            Console.WriteLine("Arhive " + h.Arhive);
            Console.WriteLine("Protect " + h.Protect);
            //string s = Encoding.UTF8.GetString(file);
            //s = "{\n{\na\nb\n}\n{\nsss\n}\n}";
            //Console.WriteLine(s);
            //s = s.Substring(s.IndexOf("{") + 2, s.LastIndexOf('}') - (s.IndexOf("{") + 3));
            //string header = s.Substring(s.IndexOf('{') + 2, s.IndexOf('}', s.IndexOf('{')) - (s.IndexOf('{') + 3));
            //string[] heeeerd = header.Split("\n");

            //h.Version = Convert.ToInt32(heeeerd[3]);
            //h.Arhive = Convert.ToInt32(heeeerd[4]);
            //h.Protect = Convert.ToInt32(heeeerd[5]);
            //h.StartInfoByte = Convert.ToInt32(heeeerd[6]);

            //s = s.Substring(s.IndexOf('}') + 2);
            //string info = s.Substring(s.IndexOf('{') + 2, s.IndexOf('}', s.IndexOf('{')) - (s.IndexOf('{') + 3));
            //Console.WriteLine("header\n" + header);
            //Console.WriteLine("info\n" + info);


        }
    }
}
