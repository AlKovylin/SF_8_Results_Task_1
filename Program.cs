using System;
using System.IO;

namespace SF_8_Results_Task_1
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("\tПРОГРАММА УДАЛЕНИЯ ФАЙЛОВ И ПАПОК В УКАЗАННОЙ ДИРЕКТОРИИ, \n\t\tКОТОРЫЕ НЕ ИСПОЛЬЗОВАЛИСЬ БОЛЕЕ 30 МИН.\n");
            Console.Write("Введите путь до папки: ");
            string pachFolder = "D://SF8FFD"; // Console.ReadLine();

            try
            {
                if (Directory.Exists(pachFolder)) // Проверим, что директория существует
                {
                    DeleteFile(pachFolder);//проверяем файлы в папке назначения
                    DeleteDirectory(pachFolder);//проверяем файлы во вложенных файлах
                }
                else
                {
                    Console.WriteLine("Папка не существует или неверно указан путь");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Нет доступа." + ex.Message);
            }
        }

        /// <summary>
        /// Рекурсия перебора вложенных папок и удаления папок файлы из которых были удалены полностью.
        /// </summary>
        /// <param name="pachFolder">путь до исходного каталога</param>
        /// <returns></returns>
        private static int DeleteDirectory(string pachFolder)
        {
            int ch = 0;

            string[] dirs = Directory.GetDirectories(pachFolder);//получаем список вложенных папок
            try
            {
                for (int i = 0; i < dirs.Length; i++)
                {
                    DeleteDirectory(dirs[i]);//идём вглубь
                    if (DeleteFile(dirs[i]))//проверяем файлы в текущей папке
                        Directory.Delete(dirs[i]);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return ch++;
        }

        /// <summary>
        /// Удаляет файлы которые не использовались более 30 мин.
        /// </summary>
        /// <param name="pachFolder">путь к папке с файлами</param>
        /// <returns>bool - если были удалены все файлы, false - если не все.</returns>
        private static bool DeleteFile(string pachFolder)
        {
            string[] files = Directory.GetFiles(pachFolder);//получаем список вложенных файлов
            int ch = 0;

            for (int i = 0; i < files.Length; i++)
            {
                try
                {
                    if ((DateTime.Now - File.GetLastAccessTime(files[i])) > TimeSpan.FromMinutes(1))
                    {
                        File.Delete(files[i]);
                        ch++;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Нет доступа." + ex.Message);
                }
            }
            if (ch == files.Length) { return true; }
            else { return false; }
        }
    }
}