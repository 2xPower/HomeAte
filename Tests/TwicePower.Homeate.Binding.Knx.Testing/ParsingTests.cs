using System;
using Xunit;

namespace TwicePower.Homeate.Binding.Knx.Testing
{
    public class ParsingTests
    {
        [Fact]
        public void Can_Parse_ETS_V13_Project_Export()
        {
            var result = Knx.Parsing.KnxprojReader.ReadKnxproject(System.IO.File.OpenRead("testknxproj.knxproj"));

            Assert.NotNull(result);
            Assert.NotNull(result.MasterData);
            Assert.NotNull(result.Project);
            Assert.True(result.Project.Length > 0);
            Assert.NotNull(result.Project[0].Installations);
            Assert.True(result.Project[0].Installations.Length > 0);
            Assert.NotNull(result.Project[0].Installations[0].Buildings);
            Assert.True(result.Project[0].Installations[0].Buildings.Length > 0);
            Assert.True(result.Project[0].Installations[0].Buildings[0].BuildingPart.Length > 0);
        }
    }
}
