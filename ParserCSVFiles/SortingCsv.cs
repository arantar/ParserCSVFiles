using System;
using System.Collections.Generic;
using System.Linq;

namespace ParserCSVFiles
{
    public static class SortingCsv
    {
        public static TSource SelectedOrDefault<TSource>(this IEnumerable<TSource> source, int sortingColumnIndex) {
            if (source == null)
                throw new ArgumentNullException(nameof(source));
            if (source is IList<TSource> sourceList) {
                if (sourceList.Count > sortingColumnIndex)
                    return sourceList[sortingColumnIndex];
            }
            else {
                using (IEnumerator<TSource> enumerator = source.GetEnumerator()) {
                    if (enumerator.MoveNext())
                        return enumerator.Current;
                }
            }

            return default(TSource);
        }

        public static List<List<string>> SortParsedCsvList(this List<List<string>> parsedCsvList, int index) {
            int max = parsedCsvList.Max(x => x.Count);
            return index <= max ? parsedCsvList.OrderBy(x => x.SelectedOrDefault(index)).ToList() : parsedCsvList;
        }
    }
}
