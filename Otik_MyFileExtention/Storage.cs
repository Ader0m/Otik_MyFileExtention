using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Otik_MyFileExtention
{
    internal static class Storage
    {       
        private static string _nameFile = "Введите имя";
        private static readonly byte[] _signature = { 0x69, 0x76, 0x61, 0x65 };
        private static readonly int _version = 2;

        #region Get/Set

        public static string NameFile { get { return _nameFile; } set { _nameFile = value; } }

        public static byte[] Signature => _signature;

        public static int Version => _version;

        #endregion

        public struct IvaExtentionHeader
        {
            public byte[] Signature;
            public bool FileOrDirectory; // 0 - file 1 - dir
            public string Name;
            public int Version;
            public int Arhive;
            public int Protect;
            public int StartInfoByte;
            
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
            public int SolveStartInfoByte()
            {
                 StartInfoByte = (Name.Length * sizeof(char)) +
                                (sizeof(int) * 4) +
                                4 + // byte mass
                                1 + // bool
                                3 + // {
                                11; // \n

                return StartInfoByte;
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
                int archiveHeaderLength = StartInfoByte - SolveStartInfoByte();
                byte[] data = new byte[SolveStartInfoByte()];
                byte[] fixData;

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

                void AddString(string target)
                {
                    byte[] targetByteMass;


                    targetByteMass = Encoding.UTF8.GetBytes(target);
                    targetByteMass.CopyTo(data, offset);
                    offset += targetByteMass.Length;
                }
                #endregion

                data[0] = Signature[0];
                data[1] = Signature[1];
                data[2] = Signature[2];
                data[3] = Signature[3];
                offset = 4;

                AddString("\n");
                AddBool(FileOrDirectory); AddString("\n");
                AddString(Name); AddString("\n");
                AddInt(Version); AddString("\n");
                AddInt(Arhive); AddString("\n");
                AddInt(Protect); AddString("\n");
                AddInt(offset + 1 + 4 + archiveHeaderLength + endHeader.Length + startHeader.Length); AddString("\n");
                AddString(endHeader);

                fixData = new byte[offset];
                for (int i = 0; i < fixData.Length; i++)
                {
                    fixData[i] = data[i];
                }
                fixData = Encoding.ASCII.GetBytes(startHeader).Concat(fixData).ToArray();  


                return fixData;
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
