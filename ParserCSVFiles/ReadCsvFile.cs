using System;
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

        // Метод для чтения CSV файлов. Пустые строки из файла пропускаются
        public List<string> ReadCsv(string path) {
            List<string> rawInputList = new List<string>();
            try {
                using (StreamReader csvReader = new StreamReader(path)) {
                    string line;
                    while ((line = csvReader.ReadLine()) != null) {
                        if (!string.IsNullOrEmpty(line)) {
                            rawInputList.Add(line);
                        }
                    }
                }
            }
            catch (Exception e) {
                Console.WriteLine(e);
                Console.ReadKey();
                Environment.Exit(0);
            }
            return rawInputList;
        }
    }
}