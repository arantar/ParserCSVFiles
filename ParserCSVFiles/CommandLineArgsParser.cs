using System;
using System.IO;
using System.Text.RegularExpressions;

namespace ParserCSVFiles
{
    public static class CommandLineArgsParser
    {
        private static string _pathFile;

        public static string PathFile {
            get => _pathFile;
            set => _pathFile = ValidateArgPathFile(value, out Match match) ? match.Groups["path"].Value : null;
        }

        private static string _sortOption;

        public static string SortOption {
            get => _sortOption;
            set => _sortOption = ValidateArgSortOption(value, out Match match) ? match.Groups["sort_line"].Value : null;
        }

        // метод, который определяет количество аргументов переданный командной строке и присваивает их соответствующему полю
        public static void ParseArgs(string[] args) {
            switch (args.Length) {
                case 0:
                    ShowTypeError("Не найдено параметров для запуска парсера. Попробуйте снова.");
                    return;
                case 1:
                    PathFile = args[0];
                    break;
                case 2: {
                    PathFile = args[0];
                    if (PathFile != null) {
                        SortOption = args[1];
                    }
                    break;
                }
            }
        }

        // метод для валидации пути к файлу CSV, на основе регулярного выражения
        private static bool ValidateArgPathFile(string path, out Match match) {
            match = Regex.Match(path, @"(?<=\/(?i)in=)(?<path>.{3,})");
            // проверка на то, правильно ли был передан параметр пути к файлу
            if (!match.Success) {
                return ShowTypeError("Введено неверное имя параметра. Необходимо /in=\"[путь к файлу]\"");
            }
            // проверяем существует ли файл
            if (!File.Exists(match.Groups["path"].Value)) {
                return ShowTypeError("Файла не существует. Проверьте набранный путь к файлу.");
            }
            // проверяем совпадает ли расширение файла с форматом CSV
            if (!string.Equals(Path.GetExtension(match.Groups["path"].Value), ".csv")) {
                return ShowTypeError("Файл имеет неверный формат!");
            }
            return true;
        }
        // метод для валидации параметра сортировки на основе регулярного выражения
        private static bool ValidateArgSortOption(string sortOption, out Match match) {
            match = Regex.Match(sortOption, @"(?<=\/(?i)sort=)(?<sort_line>\d+$)");
            if (!match.Success) {
                return ShowTypeError("Введено неверное имя параметра. Необходимо /sort=[число]");
            }
            else {
                return true;
            }
        }

        private static bool ShowTypeError(string typeError) {
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine(typeError + Environment.NewLine);
            Console.ResetColor();
            return false;
        }
    }
}

