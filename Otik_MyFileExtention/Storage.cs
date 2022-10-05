using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Otik_MyFileExtention
{
    internal static class Storage
    {       
        private static string _nameFile = "bla.iva";
        private static readonly byte[] _signature = { 0x69, 0x76, 0x61, 0x65 };
        public static readonly int Version = 1;

        #region Get/Set

        public static string NameFile { get { return _nameFile; } set { _nameFile = value; } }

        public static byte[] Signature { get => _signature; }

        #endregion

        public struct IvaExtentionHeader
        {
            public string Name;
            public int Version;
            public int Arhive;
            public int Protect;
            public int StartInfoByte;
            public byte[] Signature;
            public bool FileOrDirectory; // 0 - file 1 - dir

            public IvaExtentionHeader()
            {
                Name = "";
                Version = Storage.Version;
                Arhive = 0;
                Protect = 0;
                StartInfoByte = 0;
                Signature = new byte[4];
                FileOrDirectory = false;
            }

            /// <summary>
            /// Значение будет равно при считывании началу контента т.к отсчет данных начинаем с 0
            /// </summary>
            public void SolveStartInfoByte()
            {
                StartInfoByte = (Name.Length * sizeof(char)) +
                                (sizeof(int) * 4) +
                                4 + // byte mass
                                1 + // bool
                                3 + // {
                                12; // \n
            }

            public bool CheckSignature()
            {
                for (int i = 0; i < Signature.Length; i++)
                {
                    if (Signature[i] != Storage.Signature[i])
                    {
                        return false;
                    }
                }

                return true;
            }

            public byte[] ToWrite()
            {
                string startHeader = "\n{\n";
                byte[] signa = new byte[4];
                byte[] temp = new byte[1];
                byte[] data;

                signa[0] = Signature[0];
                signa[1] = Signature[1];
                signa[2] = Signature[2];
                signa[3] = Signature[3];

                

                data = Encoding.UTF8.GetBytes(startHeader).Concat(signa).ToArray();
                data = data.Concat(Encoding.UTF8.GetBytes("\n")).ToArray();

                temp[0] = Convert.ToByte(FileOrDirectory);
                Console.WriteLine("data l " + data.Length);
                data = data.Concat(temp).ToArray();
                Console.WriteLine("data l " + data.Length);


                data = data.Concat(Encoding.UTF8.GetBytes(ToStringToWrite())).ToArray();
                Console.WriteLine("data " + data[8]);


                return data;
            }

            public override string ToString()
            {
                return "\n{\n{\n" + // начало в HeaderTask
                    Signature[0].ToString() + " " + Signature[1].ToString() + " " + Signature[2].ToString() + " " + Signature[3].ToString() + "\n" + // 9
                    FileOrDirectory.ToString() + "\n" + // 1 + 1
                    Name + "\n" + // 7 + 1
                    Version + "\n" + // 4 + 1
                    Arhive + "\n" + // 4 + 1
                    Protect + "\n" + // 4 + 1
                    StartInfoByte.ToString() + "\n" + "}\n" + "{\n"; // 4 + 1 + 1 + 1
                // 46
            }

            private string ToStringToWrite()
            {
                return "\n" +              
                    Name + "\n" + 
                    Version + "\n" + 
                    Arhive + "\n" + 
                    Protect + "\n" + 
                    StartInfoByte.ToString() + "\n" + "}\n" + "{\n";
            }
        }
    }
}
