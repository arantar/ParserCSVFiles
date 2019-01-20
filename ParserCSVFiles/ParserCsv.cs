using System;
using System.Collections.Generic;

namespace ParserCSVFiles
{
    public class ParserCsv
    {
        // конструктор, где производятся все манипуляции при создании объекта ParseCsv
        public ParserCsv(IReadCsvFile readFile) {
            _readFile = readFile;
            _rawInputList = _readFile.RawInputCsvList;
            ParsedCsvList = ParseCsvList(',');
            if (CommandLineArgsParser.SortOption != null) {
                SortedParsedCsvList = ParsedCsvList.SortParsedCsvList(int.Parse(CommandLineArgsParser.SortOption));
            }
        }

        private readonly IReadCsvFile _readFile;
        // список, содержащий нераспарсенные строки из CSV файла
        private readonly List<string> _rawInputList;
        public List<int> ProblemLinesCsvList { get; private set; }
        // список, содержащий распарсенные строки CSV файла
        public List<List<string>> ParsedCsvList { get; private set; }
        // список, содержащий распарсенный и отсортированный строки CSV файла
        public List<List<string>> SortedParsedCsvList { get; private set; }

        /* основной метод для парсинга. Его работа основывается на подсчете количества кавычек в строке, а также он
           выявляет возможные проблемы в оформлении CSV строки
        */
        public List<List<string>> ParseCsvList(char separator) {
            List<List<string>> parsedCsvList = new List<List<string>>();
            List<int> problemLinesList = new List<int>();
            foreach (string line in _rawInputList) {
                int startIndex = 0;
                int endIndex = 0;
                int quoteCount = 0;
                var columns = new List<string>();
                foreach (char c in line) {
                    if (c == '"') {
                        quoteCount += 1;
                    }
                    else {
                        if (c == separator && quoteCount % 2 == 0) {
                            columns.Add(TrimQuotes(line.Substring(startIndex, endIndex - startIndex)));
                            startIndex = endIndex + 1;
                        }
                    }
                    endIndex++;
                }
                if (quoteCount % 2 != 0) {
                    problemLinesList.Add(_rawInputList.IndexOf(line) + 1);
                }
                columns.Add(TrimQuotes(line.Substring(startIndex, endIndex - startIndex)));
                parsedCsvList.Add(columns);
            }
            ProblemLinesCsvList = problemLinesList;
            return parsedCsvList;
        }

        // вспомогательный метод для метода ParseCsvList, который удаляет лишние кавычки в зависимости от того, где они находятся
        private string TrimQuotes(string line) {
            if (string.IsNullOrEmpty(line)) {
                return line;
            }
            if (line[0] == '"' && line.Length > 2) {
                return line.Substring(1, line.Length - 2);
            }
            else {
                return line.Trim(new[] { ' ', '"' });
            }
        }

        // метод для вывода на экран списка строк из файла CSV, которые могут содержать ошибки оформления, например лишние или недостающие кавычки
        public void ShowProblemLinesInCsvList(List<int> problemLinesList) {
            if (problemLinesList.Count == 0) {
                return;
            }
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine("Проверьте следующие строки в самом файле CSV на правильность оформления. Возможно не хватает кавычек.");
            foreach (var item in problemLinesList) {
                Console.Write(item + "  ");
            }
            Console.WriteLine("\r\n");
            Console.ResetColor();
        }
    }
}
