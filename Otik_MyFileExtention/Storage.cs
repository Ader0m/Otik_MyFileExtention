using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Otik_MyFileExtention
{
    internal static class Storage
    {       
        private static string _nameFile = "bls.iva";
        private static readonly byte[] _signature = { 0x69, 0x76, 0x61, 0x65 };
        private static readonly int _version = 1;

        #region Get/Set

        public static string NameFile { get { return _nameFile; } set { _nameFile = value; } }

        public static byte[] Signature => _signature;

        public static int Version => _version;

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
                                8; // \n
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
                string endHeader = "}\n" + "{\n";
                int offset;
                byte[] data = new byte[StartInfoByte];
                byte[] nameByteMass;


                data[0] = Signature[0];
                data[1] = Signature[1];
                data[2] = Signature[2];
                data[3] = Signature[3];
                offset = 4;

                AddString("\n");
                AddBool(FileOrDirectory); AddString("\n");
                AddString(Name);

                StartInfoByte = StartInfoByte - ((Name.Length * sizeof(char)) - nameByteMass.Length);
                byte[] fixData = new byte[StartInfoByte];
                for (int i = 0; i < fixData.Length; i++)
                {
                    fixData[i] = data[i];
                }
                data = fixData;

                AddString("\n");
                AddInt(Version); AddString("\n");
                AddInt(Arhive); AddString("\n");
                AddInt(Protect); AddString("\n");
                AddInt(StartInfoByte); AddString("\n");
                AddString(endHeader);

                data = Encoding.UTF8.GetBytes(startHeader).Concat(data).ToArray();                

                return data;

                #region LocalMethod

                void AddInt(int target)
                {
                    BitConverter.GetBytes(target).CopyTo(data, offset);
                    offset += sizeof(int);

                }

                void AddBool(bool target)
                {
                    BitConverter.GetBytes(target).CopyTo(data, offset);
                    offset += sizeof(bool);
                }

                void AddN(string target)
                {
                    nameByteMass = Encoding.UTF8.GetBytes(target);
                    nameByteMass.CopyTo(data, offset);
                    offset += nameByteMass.Length;
                }

                int AddString(string target)
                {
                    nameByteMass = Encoding.UTF8.GetBytes(target);
                    nameByteMass.CopyTo(data, offset);
                    offset += nameByteMass.Length;

                    return nameByteMass.Length;
                }
                #endregion
            }

            public override string ToString()
            {
                return "\n{\n{\n" +
                    Signature[0].ToString() + " " + Signature[1].ToString() + " " + Signature[2].ToString() + " " + Signature[3].ToString() + "\n" +
                    FileOrDirectory.ToString() + "\n" +
                    Name + "\n" +
                    Version + "\n" +
                    Arhive + "\n" +
                    Protect + "\n" +
                    StartInfoByte.ToString() + "\n" + "}\n" + "{\n";
            }
        }
    }
}
