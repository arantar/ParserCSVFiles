using System.Collections.Generic;

namespace ParserCSVFiles
{
    // Интерфейс для реализации DI паттерна (Constructor Injection)
    public interface IReadCsvFile
    {
        List<string> RawInputCsvList { get; set; }
        List<string> ReadCsv(string path);
    }
}