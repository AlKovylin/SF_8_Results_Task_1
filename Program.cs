using System;
using System.IO;

namespace SF_8_Results_Task_1
{
    class Program
    {
        static int chD = 0;
        static int chF = 0;

        static void Main(string[] args)
        {
            Console.WriteLine("\tПРОГРАММА УДАЛЕНИЯ ФАЙЛОВ И ПАПОК В УКАЗАННОЙ ДИРЕКТОРИИ, \n\t\tКОТОРЫЕ НЕ ИСПОЛЬЗОВАЛИСЬ БОЛЕЕ 30 МИН.\n");
            Console.Write("Введите путь до папки: ");
            string pachFolder =  Console.ReadLine(); //"C://SF8FFD";

            if (Directory.Exists(pachFolder))
            {
                DeleteFolderAndFiles(pachFolder);
                Console.WriteLine($"Удалено: {chD} папок и {chF} файлов");
                Console.WriteLine("Программа завершена.");
            }
            else
            {
                Console.WriteLine("Папка не существует. Или неверно задан путь.");
            }

        }

        private static void DeleteFolderAndFiles(string folder)
        {
            try
            {
                DirectoryInfo dirInfo = new DirectoryInfo(folder);
                DirectoryInfo[] diDir = dirInfo.GetDirectories();
                FileInfo[] diFiles = dirInfo.GetFiles();
                foreach (FileInfo f in diFiles)
                {
                    if ((DateTime.Now - f.LastAccessTime) > TimeSpan.FromMinutes(1))
                    {
                        f.Delete();
                        chF++;
                    }
                }
                foreach (DirectoryInfo df in diDir)
                {
                    DeleteFolderAndFiles(df.FullName);
                }
                if (dirInfo.GetDirectories().Length == 0 && dirInfo.GetFiles().Length == 0)
                {
                    dirInfo.Delete();
                    chD++;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Произошла ошибка: " + ex.Message);
            }
        }
    }
}