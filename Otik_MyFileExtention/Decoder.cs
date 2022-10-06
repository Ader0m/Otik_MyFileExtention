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
            fileStreamInput.Close();
            for (int byt = 0; byt < file.Length; byt++)
            {
                byt += 3;
                for (int i = byt; i < (byt + 4); i++)
                    h.Signature[i - byt] = file[i];
                byt += 5;

                h.FileOrDirectory = Convert.ToBoolean(file[byt]);
                byt += 2;

                int count = 0;
                while (file[byt + count] != 10)
                    count++;
                h.Name = Encoding.UTF8.GetString(file, byt, count);
                byt += count + 1;

                count = 0;
                while (file[byt + count] != 10)
                    count++;
                h.Version = Convert.ToInt32(Encoding.UTF8.GetString(file, byt, count));
                byt += count + 1;

                count = 0;
                while (file[byt + count] != 10)
                    count++;
                h.Arhive = Convert.ToInt32(Encoding.UTF8.GetString(file, byt, count));
                byt += count + 1;

                count = 0;
                while (file[byt + count] != 10)
                    count++;
                h.Protect = Convert.ToInt32(Encoding.UTF8.GetString(file, byt, count));
                byt += count + 1;

                count = 0;
                while (file[byt + count] != 10)
                    count++;
                h.StartInfoByte = Convert.ToInt32(Encoding.UTF8.GetString(file, byt, count));

                Console.WriteLine("Sygnature " + Encoding.UTF8.GetString(h.Signature));
                Console.WriteLine("bool " + h.FileOrDirectory);
                Console.WriteLine("Name " + h.Name);
                Console.WriteLine("Version " + h.Version);
                Console.WriteLine("Arhive " + h.Arhive);
                Console.WriteLine("Protect " + h.Protect);
                Console.WriteLine("StartInfoByte " + h.StartInfoByte);
                Console.WriteLine("INFO ");
                while (Encoding.UTF8.GetString(file, byt, 1) != "{")
                {
                    byt++;
                }
                byt += 2;
                count = 0;
                while (file[byt + count] != 10)
                    count++;
                Console.WriteLine(Encoding.UTF8.GetString(file, byt, count));
                ReadOnlySpan<byte> info = new ReadOnlySpan<byte>(file, byt, count);
                if (h.FileOrDirectory)
                    Directory.CreateDirectory(Directory.GetCurrentDirectory() + @"\" + h.Name);
                else
                {
                    fileStreamInput = File.Create(Directory.GetCurrentDirectory() + @"\" + h.Name);
                    fileStreamInput.Close();
                    fileStreamInput = File.OpenWrite(Directory.GetCurrentDirectory() + @"\" + h.Name);
                    fileStreamInput.Write(info);
                    fileStreamInput.Close();
                }
                byt += count + 1;
            }
        }
    }
}
