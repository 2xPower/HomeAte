using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace TwicePower.Homeate.Binding.Knx.CoreWrapper
{
    public static class DnsM 
    {
        internal static IEnumerable<IPAddress> GetHostAddresses()
        {            
            return Dns.GetHostAddressesAsync(System.Net.Dns.GetHostName()).Result.Where(i => i.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork);            
        }

        internal static IPAddress GetHostAddress(string host)
        {            
            return Dns.GetHostEntryAsync(host).Result.AddressList[0];            
        }
    }
}
