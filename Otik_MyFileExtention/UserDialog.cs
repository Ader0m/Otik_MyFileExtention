namespace Otik_MyFileExtention
{
    static class UserDialog
    {
        #region Image

        public static string Image = @"
                                                                           
              █████████   ███          ███     ████████████                
                 ███      ███          ███    ███        ███               
                 ███       ███        ███    ███          ███              
                 ███       ███        ███    ███          ███              
                 ███        ███      ███     ███          ███              
                 ███        ███      ███     ████████████████              
                 ███         ███    ███      ███          ███              
                 ███         ███    ███      ███          ███              
                 ███          ███  ███       ███          ███              
              █████████        ██████        ███          ███              
                                                                           
";

        #endregion

        public static bool FileOrDirectory = false; // 0 - file 1 - dir


        static async Task Main(string[] args)
        {
            Console.SetWindowSize(75, 20);
            for (int i = 0; i < Image.Length; i++)
            {
                Console.Write(Image[i]);
                if (i % 77 == 0)
                    await Task.Delay(50);
            }
            await Task.Delay(650);
            Console.SetWindowSize(46, 20);

            Encode encode = new Encode();
            Decoder decoder = new Decoder();
            StartDialog();
        }

        static public void StartDialog()
        {
            Console.Clear();
            LoadMenu();
            ListenUserInput();


        }

        static private void LoadMenu()
        {
            Console.WriteLine("*--------------------Menu--------------------*");
            Console.WriteLine("\tАктивный {0}: {1}", FileOrDirectory ? "Каталог" : "Файл", Storage.NameFile);
            Console.WriteLine();
            Console.WriteLine("\t1. Введите имя файла");
            Console.WriteLine("\t2. Архивировать");
            Console.WriteLine("\t3. Разархивировать");
            Console.WriteLine("\t0. Выход");
        }

        static private void ListenUserInput()
        {
            try
            {
                while (true)
                {
                    int input = Convert.ToInt32(Console.ReadLine());
                    switch (input)
                    {
                        case 1:
                            {
                                CorrectFileName();
                                StartDialog();
                                break;
                            }
                        case 2:
                            {
                                if (!Storage.NameFile.Equals("Введите имя"))
                                    if (FileOrDirectory || !Storage.NameFile.Split(".")[1].Equals("iva"))
                                    {
                                        Encode.Instence.Start();
                                    }
                                    else
                                    {
                                        Console.WriteLine("Error: Нельзя архивировать архив .iva");
                                    }
                                break;
                            }
                        case 3:
                            {
                                if (!Storage.NameFile.Equals("Введите имя"))
                                    if (Storage.NameFile.Split(".")[1].Equals("iva"))
                                    {
                                        Decoder.Instence.Start();
                                    }
                                    else
                                    {
                                        Console.WriteLine("Error: Можно разархивировать файл только формата .iva");
                                    }                                
                                break;
                            }
                        case 0:
                            {
                                return;
                            }
                        default:
                            {
                                Console.WriteLine("Error: Номер команды введен не корректно");
                                break;
                            }
                    }
                }
            }
            catch (FormatException e)
            {
                Console.WriteLine("Введите !номер! команды\n");
                Console.WriteLine(e);
                ListenUserInput();
            }
        }

        private static void CorrectFileName()
        {
            while (true)
            {
                Console.WriteLine("Введите имя каталога или файла с расширением.");
                string candidat = Console.ReadLine();
                if (!string.IsNullOrEmpty(candidat))
                {
                    if (FindTargetFile(candidat))
                    {
                        Storage.NameFile = candidat;
                        break;
                    }
                }
                else
                {
                    Console.WriteLine("Error: Имя не может быть пустым");
                }
            }
        }

        private static bool FindTargetFile(string candidat)
        {
            string[] dirs = Directory.GetDirectories(Directory.GetCurrentDirectory(), candidat);
            string[] files = Directory.GetFiles(Directory.GetCurrentDirectory(), candidat);


            switch (dirs.Length > 0, files.Length > 0)
            {
                case (true, true):
                    {
                        Console.WriteLine("Error: В целевом каталоге обнаружен и файл и каталог. Исправьте это.\n");


                        return false;
                    }
                case (false, false):
                    {
                        Console.WriteLine("Error: В целевом каталоге совпадений не найдено.\n");


                        return false;
                    }
                case (true, false):
                    {
                        Console.WriteLine("Обнаружен каталог.\n");
                        FileOrDirectory = true;

                        return true;
                    }
                case (false, true):
                    {
                        Console.WriteLine("Обнаружен файл.\n");
                        FileOrDirectory = false;


                        return true;
                    }
            }
        }
    }
}