using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using Otik_MyFileExtention.FileCollector;
using Otik_MyFileExtention.Haffman;

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
            Dictionary<char, int> freq = new Dictionary<char, int>();
            HaffmanLogiс haffmanLogic = new HaffmanLogiс();
            int dataLenght = 0;
            string inputstring = "";
            List<char> data;
            Storage.IvaExtentionHeader h = new Storage.IvaExtentionHeader();
            FileStream fileStreamInput = File.OpenRead(Storage.NameFile);
            file = new byte[fileStreamInput.Length];
            fileStreamInput.Read(file);
            fileStreamInput.Close();
            int offset = 0;
            for (int byt = 0; byt < file.Length; byt++)
            {
                Console.WriteLine("byt " + byt);
                byt += 3;
                for (int i = byt; i < (byt + 4); i++)
                    h.Signature[i - byt] = file[i];
                byt += 5;

                h.FileOrDirectory = BitConverter.ToBoolean(file, byt);
                byt += 2;

                int count = 0;
                while (file[byt + count] != 10)
                    count++;
                h.Name = Encoding.UTF8.GetString(file, byt, count);
                byt += count + 1;



                h.Version = BitConverter.ToInt32(file, byt);
                byt += 4 + 1;


                h.Arhive = BitConverter.ToInt32(file, byt);
                byt += 4 + 1;


                h.Protect = BitConverter.ToInt32(file, byt);
                byt += 4 + 1;

                h.StartInfoByte = BitConverter.ToInt32(file, byt);
                byt += 4 + 1;

                h.LengthInfo = BitConverter.ToInt32(file, byt);
                byt += 4 + 5;

                Console.WriteLine("Sygnature " + Encoding.UTF8.GetString(h.Signature));
                Console.WriteLine("bool " + h.FileOrDirectory);
                Console.WriteLine("Name " + h.Name);
                Console.WriteLine("Version " + h.Version);
                Console.WriteLine("Arhive " + h.Arhive);
                Console.WriteLine("Protect " + h.Protect);
                Console.WriteLine("StartInfoByte " + h.StartInfoByte);
                Console.WriteLine("LengthInfo " + h.LengthInfo);
                Console.WriteLine("INFO ");
                if (h.CheckSignature() && h.Version == Storage.Version)
                {
                    offset += h.StartInfoByte;
                    if (h.Arhive == 1)
                    {
                        dataLenght = BitConverter.ToInt32(file, byt);
                        Console.WriteLine("Data length " + dataLenght);
                        byt += 4;
                        for (int i = byt; i < offset;)
                        {
                            freq.Add(BitConverter.ToChar(file, i), BitConverter.ToInt32(file, i + 2));
                            i += 6;
                        }
                        haffmanLogic = new HaffmanLogiс(freq);

                        foreach (var f in freq)
                        {
                            Console.WriteLine(f.Key + " " + f.Value);
                        }
                    }
                    
                    byt = offset;
                    
                    ReadOnlySpan<byte> info = new ReadOnlySpan<byte>(file, byt, h.LengthInfo);
                    //Console.WriteLine("info lenght " + info.Length);


                    if (h.Arhive == 1)
                    {

                        data = haffmanLogic.Decompress(info, dataLenght);

                    }
                    else
                        data = new List<char>();

                    if (h.FileOrDirectory)
                    {
                        Directory.CreateDirectory(Directory.GetCurrentDirectory() + @"\" + h.Name);
                        Console.WriteLine("Создан каталог " + h.Name);
                    }
                    else
                    {
                        fileStreamInput = File.Create(Directory.GetCurrentDirectory() + @"\" + h.Name);
                        fileStreamInput.Close();
                        if (h.Arhive == 0)
                        {
                            fileStreamInput = File.OpenWrite(Directory.GetCurrentDirectory() + @"\" + h.Name);
                            fileStreamInput.Write(info);
                            fileStreamInput.Close();
                        }
                        else
                        {
                            FileStream fs = File.OpenWrite(Directory.GetCurrentDirectory() + @"\" + h.Name);
                            foreach (char c in data)
                            {
                                byte[] bm = BitConverter.GetBytes(c);
                                fs.Write(bm, 0, bm.Length);
                            }
                            fs.Close();
                        }
                        Console.WriteLine("Конец процесса " + h.Name);
                    }

                }
                else
                {
                    Console.WriteLine("Error: Не корректная сигнатура или версия");
                }

                byt += h.LengthInfo + 1;
                offset = byt + 1;
            }
        }
    }
}
