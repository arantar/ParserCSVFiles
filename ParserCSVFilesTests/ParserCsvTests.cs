using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ParserCSVFiles;
using ParserCSVFiles.Fakes;

namespace ParserCSVFilesTests
{
    [TestClass]
    public class ParserCsvTests
    {
        private void AreEqual(List<List<string>> expected, List<List<string>> actual) {
            Assert.AreEqual(expected.Count, actual.Count);
            for (int i = 0; i < expected.Count; i++) {
                CollectionAssert.AreEqual(expected[i], actual[i]);
            }
        }

        [TestMethod]
        public void ParseCsvList_EmptyTest() {
            var testData = new List<string>() { "" };
            var expectedData = new List<List<string>>()
            {
                new List<string>() { "" },
            };
            var readerCsv = new StubIReadCsvFile() {
                RawInputCsvListGet = () => testData
            };
            var parserCsv = new ParserCsv(readerCsv);
            AreEqual(expectedData, parserCsv.ParsedCsvList);
        }

        [TestMethod]
        public void ParseCsvList_EmptyLineTest() {
            var testData = new List<string>() { "\r\n" };
            var expectedData = new List<List<string>>()
            {
                new List<string>() { "\r\n" },
            };
            var readerCsv = new StubIReadCsvFile() {
                RawInputCsvListGet = () => testData
            };
            var parserCsv = new ParserCsv(readerCsv);
            AreEqual(expectedData, parserCsv.ParsedCsvList);
        }

        [TestMethod]
        public void ParseCsvList_SingleCommaTest() {
            var testData = new List<string>() { "," };
            var expectedData = new List<List<string>>()
            {
                new List<string>() { "", "" },
            };
            var readerCsv = new StubIReadCsvFile() {
                RawInputCsvListGet = () => testData
            };
            var parserCsv = new ParserCsv(readerCsv);
            AreEqual(expectedData, parserCsv.ParsedCsvList);
        }

        [TestMethod]
        public void ParseCsvList_QuoteCharTest1() {
            var testData = new List<string>() { "a\"b" };
            var expectedData = new List<List<string>>()
            {
                new List<string>() { "a\"b" },
            };
            var readerCsv = new StubIReadCsvFile() {
                RawInputCsvListGet = () => testData
            };
            var parserCsv = new ParserCsv(readerCsv);
            AreEqual(expectedData, parserCsv.ParsedCsvList);
        }

        [TestMethod]
        public void ParseCsvList_QuoteCharTest2() {
            var testData = new List<string>() { "a\"" };
            var expectedData = new List<List<string>>()
            {
                new List<string>() { "a" },
            };
            var readerCsv = new StubIReadCsvFile() {
                RawInputCsvListGet = () => testData
            };
            var parserCsv = new ParserCsv(readerCsv);
            AreEqual(expectedData, parserCsv.ParsedCsvList);
        }

        [TestMethod]
        public void ParseCsvList_QuoteCharTest3() {
            var testData = new List<string>() { "a\",b" };
            var expectedData = new List<List<string>>()
            {
                new List<string>() { "a\",b" },
            };
            var readerCsv = new StubIReadCsvFile() {
                RawInputCsvListGet = () => testData
            };
            var parserCsv = new ParserCsv(readerCsv);
            AreEqual(expectedData, parserCsv.ParsedCsvList);
        }

        [TestMethod]
        public void ParseCsvList_SingleLineTest() {
            var testData = new List<string>() { "a, b, c" };
            var expectedData = new List<List<string>>()
            {
                new List<string>() { "a", "b", "c" },
            };
            var readerCsv = new StubIReadCsvFile() {
                RawInputCsvListGet = () => testData
            };
            var parserCsv = new ParserCsv(readerCsv);
            AreEqual(expectedData, parserCsv.ParsedCsvList);
        }

        [TestMethod]
        public void ParseCsvList_TwoLinesTest() {
            var testData = new List<string>() { "a,b,c", "c,b,a" };
            var expectedData = new List<List<string>>()
            {
                new List<string>() { "a", "b", "c" },
                new List<string>() { "c", "b", "a" },
            };
            var readerCsv = new StubIReadCsvFile() {
                RawInputCsvListGet = () => testData
            };
            var parserCsv = new ParserCsv(readerCsv);
            AreEqual(expectedData, parserCsv.ParsedCsvList);
        }

        [TestMethod]
        public void ParseCsvList_QuotedValueTest() {
            var testData = new List<string>() { "a, \"b\", c" };
            var expectedData = new List<List<string>>()
            {
                new List<string>() { "a", "b", "c" },
            };
            var readerCsv = new StubIReadCsvFile() {
                RawInputCsvListGet = () => testData
            };
            var parserCsv = new ParserCsv(readerCsv);
            AreEqual(expectedData, parserCsv.ParsedCsvList);
        }

        [TestMethod]
        public void ParseCsvList_SingleQuotedValueTest() {
            var testData = new List<string>() { "\"a\"" };
            var expectedData = new List<List<string>>()
            {
                new List<string>() { "a" },
            };
            var readerCsv = new StubIReadCsvFile() {
                RawInputCsvListGet = () => testData
            };
            var parserCsv = new ParserCsv(readerCsv);
            AreEqual(expectedData, parserCsv.ParsedCsvList);
        }

        [TestMethod]
        public void ParseCsvList_EmptyQuotedValueTest() {
            var testData = new List<string>() { "a,\"\",c" };
            var expectedData = new List<List<string>>()
            {
                new List<string>() { "a", "", "c" },
            };
            var readerCsv = new StubIReadCsvFile() {
                RawInputCsvListGet = () => testData
            };
            var parserCsv = new ParserCsv(readerCsv);
            AreEqual(expectedData, parserCsv.ParsedCsvList);
        }

        [TestMethod]
        public void ParseCsvList_EmptyValueTest() {
            var testData = new List<string>() { "a,,c" };
            var expectedData = new List<List<string>>()
            {
                new List<string>() { "a", "", "c" },
            };
            var readerCsv = new StubIReadCsvFile() {
                RawInputCsvListGet = () => testData
            };
            var parserCsv = new ParserCsv(readerCsv);
            AreEqual(expectedData, parserCsv.ParsedCsvList);
        }

        [TestMethod]
        public void ParseCsvList_QuotedValueWithCommaTest() {
            var testData = new List<string>() { "a,\"b,c\",d" };
            var expectedData = new List<List<string>>()
            {
                new List<string>() { "a", "b,c", "d" },
            };
            var readerCsv = new StubIReadCsvFile() {
                RawInputCsvListGet = () => testData
            };
            var parserCsv = new ParserCsv(readerCsv);
            AreEqual(expectedData, parserCsv.ParsedCsvList);
        }

        [TestMethod]
        public void ParseCsvList_NestedQuotedValueTest() {
            var testData = new List<string>() { "\"\"a\"\"" };
            var expectedData = new List<List<string>>()
            {
                new List<string>() { "\"a\"" },
            };
            var readerCsv = new StubIReadCsvFile() {
                RawInputCsvListGet = () => testData
            };
            var parserCsv = new ParserCsv(readerCsv);
            AreEqual(expectedData, parserCsv.ParsedCsvList);
        }
    }
} 