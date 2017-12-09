using System;
using System.Collections.Generic;
using System.Text;

namespace Twicepower.Homeate.Contracts
{
    public enum BindingState
    {
        Uninitialized,
        Initializing,
        Disconnected,
        Disconnecting,
        Connecting,
        Connected,
    }
}
