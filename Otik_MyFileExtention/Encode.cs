using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Otik_MyFileExtention.FileCollector;

namespace Otik_MyFileExtention
{
    internal class Encode
    {
        #region Singleton

        public static Encode? Instence { get { return _instance; } }
        private static Encode? _instance;

        #endregion

        private string _archivePath;
        private bool _fileOrDirectory; // 0 - file 1 - dir


        #region Get/Set

        public string ArchivePath { get { return _archivePath; }}

        #endregion

        public Encode() => _instance = this;

        public void Start()
        {
            _fileOrDirectory = UserDialog.FileOrDirectory;
            _archivePath = Directory.GetCurrentDirectory() + @"\" + Storage.NameFile.Split(".")[0] + ".iva";
            FileStream fs = File.Create(_archivePath);
            fs.Close();

            if (_fileOrDirectory)
            {
                ArchiverRecursion(Storage.NameFile);
            }
            else
            {
                ArchiveFile(Storage.NameFile);
                //EndWriteSegment();
            }            
        }     

        private void ArchiverRecursion(string filePath)
        {
            string[] dirs = Directory.GetDirectories(Directory.GetCurrentDirectory(), filePath + @"\*");
            string[] files = Directory.GetFiles(Directory.GetCurrentDirectory(), filePath + @"\*");


            #region LocalFunction
            void FileForeach()
            {
                foreach (string fil in files)
                {
                    ArchiveFile(fil);
                    //EndWriteSegment();
                }
            }

            void DirForeach()
            {
                foreach (string dir in dirs)
                {
                    ArchiverRecursion(dir.Replace(Directory.GetCurrentDirectory() + @"\", ""));
                }
            }
            #endregion

            ArchiveFolder(filePath);
            switch (dirs.Length > 0, files.Length > 0)
            {
                case (true, true):
                    {
                        Console.WriteLine("В {0} обнаружены и каталоги и файлы\n", filePath);

                        DirForeach();
                        FileForeach();

                        break;
                    }
                case (false, false):
                    {
                        Console.WriteLine("Папка {0} пуста\n", filePath);


                        break;
                    }
                case (true, false):
                    {
                        Console.WriteLine("В {0} Обнаружен катало(г/ги).", filePath);

                        DirForeach();

                        break;
                    }
                case (false, true):
                    {
                        Console.WriteLine("В {0} Обнаружен фай(л/лы).", filePath);

                        FileForeach(); 
                        
                        break;
                    }               
            }

            //EndWriteSegment();            
        }

        private void ArchiveFile(string filePath)
        {
            try
            {                
                FileCollectorController fileCollector = new FileCollectorController(0);

                FileCollectorController.FilePath = filePath;
                FileCollectorController.Header.Name = filePath.Replace(Directory.GetCurrentDirectory() + @"\", "");
                FileCollectorController.Header.FileOrDirectory = false;
                FileCollectorController.Header.Signature = Storage.Signature;
                FileCollectorController.Header.Arhive = 0;
                FileCollectorController.Header.SolveStartInfoByte();

                fileCollector.Task(new byte[1]);

                fileCollector.Clear();
            }
            catch(Exception e)
            {
                Console.WriteLine(e);
            }
        }

        private void ArchiveFolder(string filePath)
        {
            try
            {
                FileCollectorController fileCollector = new FileCollectorController(1);

                FileCollectorController.FilePath = filePath;
                FileCollectorController.Header.Name = filePath.Replace(Directory.GetCurrentDirectory(), "");
                FileCollectorController.Header.FileOrDirectory = true;
                FileCollectorController.Header.Signature = Storage.Signature;
                FileCollectorController.Header.SolveStartInfoByte();

                fileCollector.Task(new byte[1]);

                fileCollector.Clear();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        private void EndWriteSegment()
        {
            FileStream fileStreamInput = File.OpenWrite(_archivePath);

            fileStreamInput.Position = fileStreamInput.Length;
            fileStreamInput.Write(Encoding.UTF8.GetBytes("\n}"), 0, 2);

            fileStreamInput.Close();
        }
    }
}
