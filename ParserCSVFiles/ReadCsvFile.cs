using System.Collections.Generic;
using System.IO;

namespace ParserCSVFiles
{
    public class ReadCsvFile : IReadCsvFile
    {
        public ReadCsvFile(string path) {
            RawInputCsvList = ReadCsv(path);
        }

        public List<string> RawInputCsvList { get; set; }

        // метод для чтения CSV файлов. Пустые строки из файла пропускаются
        public List<string> ReadCsv(string path) {
            List<string> rawInputList = new List<string>();
            using (StreamReader csvReader = new StreamReader(path)) {
                string line;
                while ((line = csvReader.ReadLine()) != null) {
                    if (!string.IsNullOrEmpty(line)) {
                        rawInputList.Add(line);
                    }
                }
            }
            return rawInputList;
        }
    }
}