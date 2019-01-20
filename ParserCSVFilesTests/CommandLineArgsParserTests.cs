using Microsoft.VisualStudio.TestTools.UnitTesting;
using ParserCSVFiles;

namespace ParserCSVFilesTests
{
    [TestClass]
    public class CommandLineArgsParserTests
    {
        [TestMethod]
        public void PathFileForInvalidityTest() {
            CommandLineArgsParser.PathFile = "";
            Assert.IsNull(CommandLineArgsParser.PathFile);
            CommandLineArgsParser.PathFile = "123:";
            Assert.IsNull(CommandLineArgsParser.PathFile);
            CommandLineArgsParser.PathFile = "/in=";
            Assert.IsNull(CommandLineArgsParser.PathFile);
            CommandLineArgsParser.PathFile = "/in=C:\\test";
            Assert.IsNull(CommandLineArgsParser.PathFile);
            CommandLineArgsParser.PathFile = "/in=D:\\test.txt";
            Assert.IsNull(CommandLineArgsParser.PathFile);
            CommandLineArgsParser.PathFile = "/sort=1";
            Assert.IsNull(CommandLineArgsParser.PathFile);
        }

        [TestMethod]
        public void SortOptionForInvalidityTest() {
            CommandLineArgsParser.SortOption = "";
            Assert.IsNull(CommandLineArgsParser.SortOption);
            CommandLineArgsParser.SortOption = "/so=1";
            Assert.IsNull(CommandLineArgsParser.SortOption);
            CommandLineArgsParser.SortOption = "/sort=";
            Assert.IsNull(CommandLineArgsParser.SortOption);
            CommandLineArgsParser.SortOption = "/sort=0";
            Assert.IsNull(CommandLineArgsParser.SortOption);
        }
    }
}
