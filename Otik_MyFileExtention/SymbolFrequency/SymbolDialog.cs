using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Otik_MyFileExtention.SymbolFrequency
{
    internal class SymbolDialog
    {
        private FriquencyController _controller;

        public SymbolDialog(ISymbolTask task)
        {
            _controller = new FriquencyController(task);
        }

        public void StartSymbolDialog()
        {
            LoadSymbolDialogMenu();
            ListenSymbolDialogUserInput();
        }

        private void LoadSymbolDialogMenu()
        {
            Console.Clear();
            Console.WriteLine("*-------------------SymbolMenu-------------------*");
            Console.WriteLine("\tАктивный файл {0}", Storage.NameFile);
            Console.WriteLine();
            Console.WriteLine("\t1. Введите имя файла");
            Console.WriteLine("\t2. Сортировать по алфавиту");
            Console.WriteLine("\t3. Сортировать частоты по убыванию");
            Console.WriteLine("\t0. Выход");
        }

        private void ListenSymbolDialogUserInput()
        {
            while (true)
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
                                    UserDialog.CorrectFileName();                                   
                                    _controller = new FriquencyController(new ByteFrequency());
                                    StartSymbolDialog();
                                    break;
                                }
                            case 2:
                                {
                                    _controller.sortKey();
                                    break;
                                }
                            case 3:
                                {
                                    _controller.sortValue();
                                    break;
                                }
                            case 0:
                                {
                                    UserDialog.LoadSecondMenu();
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
                    ListenSymbolDialogUserInput();
                }
            }
        }     
    }
}
