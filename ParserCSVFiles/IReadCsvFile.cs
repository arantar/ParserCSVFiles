using System.Collections.Generic;

namespace ParserCSVFiles
{
    public interface IReadCsvFile
    {
        List<string> RawInputCsvList { get; set; }
        List<string> ReadCsv(string path);
    }
}