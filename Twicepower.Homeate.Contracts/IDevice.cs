using System;
using System.Collections.Generic;
using System.Text;

namespace Twicepower.Homeate.Contracts
{
    public interface IDevice<TDeviceStatus> where TDeviceStatus : IDeviceStatus
    {
    }
}
