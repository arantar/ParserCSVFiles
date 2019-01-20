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
                Console.WriteLine("Нажмите Enter для завершения программы.");
                Console.ReadKey();
                Environment.Exit(0);
            }
            // создаем объект типа ParserCsv, в результате чего в нем уже будет содержаться распарсенный CSV файл в виде списка
            ParserCsv parserCsv = new ParserCsv(new ReadCsvFile(CommandLineArgsParser.PathFile));
            parserCsv.ShowProblemLinesInCsvList(parserCsv.ProblemLinesCsvList);
            // обращается к тому ли иному свойству в зависимости от того, указан ли у нас параметр сортировки
            ShowParsedCsvList(CommandLineArgsParser.SortOption == null
                ? parserCsv.ParsedCsvList
                : parserCsv.SortedParsedCsvList);
        }

        // метод для вывода на экран распарсенного (отсортированного) CSV файла
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

