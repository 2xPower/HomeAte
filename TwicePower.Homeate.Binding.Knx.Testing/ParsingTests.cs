using System;
using Xunit;

namespace TwicePower.Homeate.Binding.Knx.Testing
{
    public class ParsingTests
    {
        [Fact]
        public void Test1()
        {
            var result = Knx.Parsing.KnxprojReader.ReadKnxproject(System.IO.File.OpenRead("testknxproj.knxproj"));

            Assert.NotNull(result);
        }
    }
}
