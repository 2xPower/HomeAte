using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Twicepower.Homeate.Contracts;
using TwicePower.Homeate.Binding.Knx;
using Xunit;

namespace TwicePower.Homeate.Binding.Knx.Testing
{
    public class KnxBindingTests
    {
        [Fact]
        public async Task A_Connection_Can_Be_Made_And_Disconnected_With_Correct_State_Events()
        {
            var binding = new KnxBinding();
            var stateList = new List<BindingState>();
            Assert.Equal(BindingState.Uninitialized, binding.State);
            binding.BindingStateChanged += (sender, state) =>
            {
                stateList.Add(state);
            };
            await binding.Init();
            Assert.True(binding.State == Twicepower.Homeate.Contracts.BindingState.Disconnected);
            Assert.Equal(BindingState.Initializing, stateList[0]);
            Assert.Equal(BindingState.Disconnected, stateList[1]);            
            await binding.Connect();
            Assert.True(binding.State == Twicepower.Homeate.Contracts.BindingState.Connected);
            Assert.Equal(BindingState.Connecting, stateList[2]);
            Assert.Equal(BindingState.Connected, stateList[3]);
            await binding.Disconnect();
            Assert.True(binding.State == Twicepower.Homeate.Contracts.BindingState.Disconnected);
            Assert.Equal(BindingState.Disconnecting, stateList[4]);
            Assert.Equal(BindingState.Disconnected, stateList[5]);
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
