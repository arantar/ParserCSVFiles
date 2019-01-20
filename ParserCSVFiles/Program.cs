using System;
using System.Collections.Generic;

namespace ParserCSVFiles
{
    class Program
    {
        public static void Main(string[] args) {
            /* Вызываем метод для обработки аргументов командной строки.
               Гарантируем при этом, что введенные данные валидные: файл 
               для распарсивания существует и имеет расширение *.csv, а
               параметры сортировки - это натуральное число
            */
            CommandLineArgsParser.ParseArgs(args);
            if (CommandLineArgsParser.PathFile == null) {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Проверьте правильность введенных параметров. Нажмите Enter и попробуйте снова.");
                Console.ResetColor();
                Console.ReadKey();
                Environment.Exit(0);
            }
            // Создаем объект типа ParserCsv, в результате чего в нем будет содержаться распарсенный CSV файл в виде списка
            ParserCsv parserCsv = new ParserCsv(new ReadCsvFile(CommandLineArgsParser.PathFile));
            // Выводим на экран строки в CSV файле, которые могут содержать ошибку в оформлении
            parserCsv.ShowProblemLinesInCsvList(parserCsv.ProblemLinesCsvList);
            // Обращается к тому ли иному полю в зависимости от того, указан ли у нас параметр сортировки
            ShowParsedCsvList(CommandLineArgsParser.SortOption == null
                ? parserCsv.ParsedCsvList
                : parserCsv.SortedParsedCsvList);
        }

        // Метод для вывода на экран распарсенного (отсортированного) CSV файла
        public static void ShowParsedCsvList(List<List<string>> list) {
            int index = 0;
            foreach (var i in list) {
                Console.Write(++index + ".  ");
                foreach (var j in i) {
                    Console.Write(j);
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write(" | ");
                    Console.ResetColor();
                }
                Console.WriteLine();
            }
            Console.WriteLine("\r\nНажмите Enter для завершения программы.");
            Console.ReadKey();
        }
    }
}

