using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TwicePower.Homeate.Binding.Knx;
using Xunit;

namespace TwicePower.Homeate.Binding.Knx.Testing
{
    public class KnxBindingTests
    {
        [Fact]
        public async Task A_Connection_Can_Be_Made_And_Disconnected()
        {
            var binding = new KnxBinding();
            await binding.Init();
            Assert.True(binding.State == Twicepower.Homeate.Contracts.BindingState.Disconnected);
            await binding.Connect();
            Assert.True(binding.State == Twicepower.Homeate.Contracts.BindingState.Connected);
            await binding.Disconnect();
            Assert.True(binding.State == Twicepower.Homeate.Contracts.BindingState.Disconnected);
        }

        [Fact]
        public async Task A_Status_Can_Be_Checked()
        {
            var result = Knx.Parsing.KnxprojReader.ReadKnxproject(System.IO.File.OpenRead("testknxproj.knxproj"));

            var binding = new KnxBinding();
            await binding.Init();
            await binding.Connect();

            await binding.Disconnect();
        }
    }
}
